using rose.row.data;
using rose.row.default_package;
using rose.row.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace rose.row.rendering.game_preview
{
    public class WeaponPreviewManager : Singleton<WeaponPreviewManager>
    {
        #region static

        public static void create()
        {
            var gameObject = new GameObject("Weapon Preview Manager");
            gameObject.AddComponent<WeaponPreviewManager>();
            DontDestroyOnLoad(gameObject);
        }

        #region preview files

        public static string weaponPreviewsDirectory => $"{Constants.basePath}/Textures/previews/weapons";

        public static string getCachedWeaponPreviewPath(WeaponManager.WeaponEntry weapon)
        {
            var filePath = $"{weaponPreviewsDirectory}/{weapon.name.toConventionalFileName()}.png";
            return filePath;
        }

        public static bool isWeaponPreviewCached(WeaponManager.WeaponEntry weapon)
        {
            if (!Directory.Exists(weaponPreviewsDirectory))
            {
                Directory.CreateDirectory(weaponPreviewsDirectory);
                return false;
            }

            return File.Exists(getCachedWeaponPreviewPath(weapon));
        }

        #endregion

        #endregion

        #region unity

        private void Awake()
        {
            populateScheduledRenderedWeaponQueue();
            generatePreviews();
        }

        #endregion

        #region behaviour

        public static Dictionary<WeaponManager.WeaponEntry, Texture2D> textures
            = new Dictionary<WeaponManager.WeaponEntry, Texture2D>();

        private static Queue<WeaponManager.WeaponEntry> _scheduledRenderedWeapons
            = new Queue<WeaponManager.WeaponEntry>();

        private Coroutine _renderCoroutine;

        private void populateScheduledRenderedWeaponQueue()
        {
            _scheduledRenderedWeapons.Clear();
            foreach (var weapon in WeaponManager.instance.allWeapons)
            {
                // Enqueues the weapon to be renderer later on.
                _scheduledRenderedWeapons.Enqueue(weapon);
            }
        }

        private Texture2D getStandardTexture()
        {
            return new Texture2D(512, 512, TextureFormat.ARGB32, false, false);
        }

        private void generatePreviews()
        {
            Debug.Log($"Generating {_scheduledRenderedWeapons.Count} weapon previews.");

            if (GamePreview.instance != null)
            {
                if (_renderCoroutine != null)
                {
                    GamePreview.instance.StopCoroutine(_renderCoroutine);
                }

                _renderCoroutine = GamePreview.instance.StartCoroutine(renderPreviews());
            }
        }

        private IEnumerator renderPreviews()
        {
            while (!_scheduledRenderedWeapons.isEmpty())
            {
                // Safety measure so it doesn't generate any preview if the object is disabled.
                // We can suppose that we don't want to generate or do anything if the object is.
                while (!gameObject.activeInHierarchy)
                {
                    yield return 0;
                }

                var weaponEntry = _scheduledRenderedWeapons.Dequeue();

                if (weaponEntry.prefab == null)
                {
                    Debug.LogWarning($"Could not generate weapon entry '{weaponEntry.name}' because its prefab is null.");
                }
                else
                {
                    // Adds the weapon entry to the texture dictionary, but only if it makes
                    // sense (i.e. if the prefab is not null).
                    textures.Add(weaponEntry, getStandardTexture());
                    var pulledWeaponTexture = false;
                    var tryFailed = false;

                    var weaponEntryFilePath = getCachedWeaponPreviewPath(weaponEntry);
                    try
                    {
                        // If the weapon preview exists we don't want to generate it. Instead we want
                        // to pull it from disk and load it that way.
                        if (File.Exists(weaponEntryFilePath))
                        {
                            var bytes = File.ReadAllBytes(weaponEntryFilePath);

                            var profileTexture = getStandardTexture();
                            profileTexture.LoadImage(bytes);
                            profileTexture.Apply();

                            textures[weaponEntry] = profileTexture;
                            Debug.Log($"Pulled weapon texture '{weaponEntry.name}' from disk.");
                            pulledWeaponTexture = true;
                        }
                    }
                    catch (Exception exception)
                    {
                        tryFailed = true;
                        Debug.LogWarning(exception.Message);
                    }

                    // This is when we didn't generate the weapon texture yet, so it's not cached
                    // on the disk.
                    if (!pulledWeaponTexture && !tryFailed)
                        yield return generateEntryPreview(weaponEntry);
                }
            }
        }

        private void updateWeaponPrefab(GameObject prefab, Transform parent, int team)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }

            var gameObject = Instantiate(prefab, parent);

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            GameManager.SetupRecursiveLayer(gameObject.transform, 30);

            var weapon = gameObject.GetComponent<Weapon>();
            weapon.enabled = false;
            weapon.arms.gameObject.SetActive(false);
            Destroy(weapon.arms.gameObject);
            Destroy(weapon.animator);
        }

        private IEnumerator generateEntryPreview(WeaponManager.WeaponEntry weaponEntry)
        {
            var parent = GamePreview.instance.vehiclePreviewParent;
            updateWeaponPrefab(weaponEntry.prefab, parent, -1);

            var camera = GamePreview.instance.vehiclePreviewCamera;
            var instantiated = parent.GetChild(parent.childCount - 1);
            instantiated.localEulerAngles = Vector3.zero;

            var renderers = instantiated.GetComponentsInChildren<MeshRenderer>();
            var bounds = renderers[0].bounds;
            for (int j = 1; j < renderers.Length; j++)
                if (renderers[j].transform.parent = instantiated.transform)
                    if (renderers[j].bounds.extents.magnitude >= bounds.extents.magnitude / 2)
                        bounds.Encapsulate(renderers[j].bounds);

            camera.aspect = 1f;
            camera.transform.position = bounds.center;
            camera.transform.LookAt(bounds.center);

            var allCornersVisible = false;
            var tokens = 100;
            while (!allCornersVisible && tokens > 0)
            {
                tokens--;
                camera.transform.position -= camera.transform.forward * 0.5f;
                allCornersVisible = true;

                foreach (var corner in bounds.getCorners())
                {
                    var screenPosition = camera.WorldToViewportPoint(corner);
                    if (screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1)
                        allCornersVisible = false;
                }
            }

            camera.nearClipPlane = 0.001f;

            yield return new WaitForEndOfFrame();

            var currentRT = RenderTexture.active;
            RenderTexture.active = camera.targetTexture;

            camera.Render();

            Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
            image.Apply();
            textures[weaponEntry] = image;

            RenderTexture.active = currentRT;

            var path = getCachedWeaponPreviewPath(weaponEntry);
            Directory.CreateDirectory(weaponPreviewsDirectory);
            var bytes = image.EncodeToPNG();
            File.WriteAllBytes(path, bytes);

            var bytesSize = bytes.Length / 1000;
            Debug.Log($"Renderered weapon '{weaponEntry.name}' and saved it on disk ({bytesSize}KB).");

            camera.ResetAspect();

            yield return 0;
            yield break;
        }

        #endregion
    }
}
