using rose.row.data;
using rose.row.default_package;
using rose.row.dev.vehicle_selector;
using rose.row.easy_events;
using rose.row.util;
using rose.row.vehicles;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace rose.row.rendering.game_preview
{
    public class VehiclePreviewManager : Singleton<VehiclePreviewManager>
    {
        public static void create()
        {
            var gameObject = new GameObject("Vehicle Preview Manager");
            gameObject.AddComponent<VehiclePreviewManager>();
            DontDestroyOnLoad(gameObject);
        }

        #region declarations

        private IEnumerable<VehicleInfo> _vehicles;
        private List<VehicleInfo> _scheduledRenderedVehicles;
        private Coroutine _renderCoroutine;

        private readonly Dictionary<VehicleInfo, Texture2D> _vehicleTextures = new Dictionary<VehicleInfo, Texture2D>();

        #endregion

        #region public utility

        public Texture2D this[VehicleInfo info] => _vehicleTextures[info];

        public Texture2D this[GameObject prefab]
        {
            get
            {
                foreach (var pair in _vehicleTextures)
                    if (pair.Key.prefab == prefab)
                        return pair.Value;

                return null;
            }
        }

        public VehicleInfoPair[] pairs
        {
            get
            {
                var pairs = _vehicleTextures.ToArray();
                var result = new VehicleInfoPair[pairs.Length];

                for (int i = 0; i < pairs.Length; i++)
                    result[i] = new VehicleInfoPair(pairs[i].Key, pairs[i].Value);

                return result;
            }
        }

        #endregion

        #region generating

        private void Awake()
        {
            populateVehiclesList();
            generateVehiclePreviews();
        }

        private Texture2D getStandardTexture2D()
        {
            return new Texture2D(512, 512, TextureFormat.ARGB32, false, false);
        }

        #region files

        public string vehicleEntryCachedDirectory => $"{Constants.basePath}/Textures/previews/vehicles";

        private string vehicleEntryFilePath(VehicleInfo entry)
        {
            var fileName = entry.prefab.transform.name.toConventionalFileName();
            var filePath = $"{vehicleEntryCachedDirectory}/{fileName}.png";
            return filePath;
        }

        private bool isVehicleEntryRegistered(VehicleInfo entry)
        {
            if (!Directory.Exists(vehicleEntryCachedDirectory))
            {
                Directory.CreateDirectory(vehicleEntryCachedDirectory);
                return false;
            }

            return File.Exists(vehicleEntryFilePath(entry));
        }

        #endregion

        #region rendering

        private void populateVehiclesList()
        {
            _vehicles = ModManager.instance.getVehicles();

            foreach (var vehicle in _vehicles)
                _vehicleTextures.Add(vehicle, getStandardTexture2D());
        }

        private void generateVehiclePreviews()
        {
            Debug.Log("Generating or pulling vehicle previews from disk.");

            if (GamePreview.instance != null)
            {
                if (_renderCoroutine != null)
                {
                    GamePreview.instance.StopCoroutine(_renderCoroutine);
                }

                _scheduledRenderedVehicles = _vehicles.ToList();
                _renderCoroutine = GamePreview.instance.StartCoroutine(renderPreviews());
            }
        }

        private IEnumerator renderPreviews()
        {
            while (_scheduledRenderedVehicles.Count() > 0)
            {
                while (!gameObject.activeInHierarchy)
                {
                    yield return 0;
                }

                var entry = _scheduledRenderedVehicles.First();
                _scheduledRenderedVehicles.RemoveAt(0);

                if (entry.prefab == null)
                {
                    Debug.LogWarning("Cannot generate vehicle preview for null prefab.");
                }
                else
                {
                    var path = vehicleEntryFilePath(entry);
                    if (isVehicleEntryRegistered(entry))
                    {
                        var texture = getStandardTexture2D();
                        texture.LoadImage(File.ReadAllBytes(path));
                        texture.Apply();
                        _vehicleTextures[entry] = texture;
                        Debug.Log($"Pulled vehicle texture '{entry.prefab.transform.name}' from disk.");
                    }
                    else
                    {
                        yield return renderEntry(entry);
                    }
                }

                if (_scheduledRenderedVehicles.Count <= 0)
                {
                    Debug.Log($"Finished rendering or pulling vehicle previews from disk.");
                    Events.onFinishedRenderingVehiclePreviews.before?.Invoke();
                    Events.onFinishedRenderingVehiclePreviews.after?.Invoke();
                }
            }

            yield break;
        }

        private IEnumerator renderEntry(VehicleInfo entry)
        {
            var parent = GamePreview.instance.vehiclePreviewParent;
            GamePreview.UpdateVehiclePrefab(entry.prefab, parent, -1);

            var camera = GamePreview.instance.vehiclePreviewCamera;
            var instantiated = parent.GetChild(parent.childCount - 1);
            instantiated.localEulerAngles = Vector3.zero;
            var bounds = instantiated.getMaxBounds();
            var offset = bounds.size * -2;
            offset.y *= -1;
            camera.transform.position = instantiated.transform.position + offset;
            camera.transform.LookAt(bounds.center);
            camera.transform.RotateAround(instantiated.gameObject.transform.position, new Vector3(0, 1, 0), 32.5f);

            yield return new WaitForEndOfFrame();

            var currentRT = RenderTexture.active;
            RenderTexture.active = camera.targetTexture;

            camera.Render();

            Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
            image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
            image.Apply();
            _vehicleTextures[entry] = image;

            RenderTexture.active = currentRT;
            var path = vehicleEntryFilePath(entry);
            File.WriteAllBytes(path, image.EncodeToPNG());

            Debug.Log($"Renderered vehicle entry '{entry.prefab.transform.name}'.");

            yield return 0;
            yield break;
        }

        #endregion

        #endregion generating
    }
}