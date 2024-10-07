using rose.row.default_package;
using rose.row.easy_events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace rose.row.actor.player.camera
{
    public class OldDeathCamera : Singleton<OldDeathCamera>
    {
        public GameObject cameraContainer;
        public Camera camera;

        public static void create()
        {
            var gameObject = new GameObject("Death Camera");
            gameObject.AddComponent<OldDeathCamera>();
        }

        private void Awake()
        {
            subscribeEvents();

            #region object

            cameraContainer = new GameObject("Camera");
            cameraContainer.transform.SetParent(transform);
            cameraContainer.transform.localPosition = Vector3.zero;

            #endregion object

            #region camera

            camera = cameraContainer.AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.depth = 3;
            camera.farClipPlane = 40000;

            #endregion camera
        }

        private void Start()
        {
            // TODO: Implement the hiding of the scenery camera better.
            SceneryCamera.instance.camera.enabled = false;

            enable();
        }

        #region events

        private void OnDestroy() => unsubscribeEvents();

        private void subscribeEvents()
        {
            Events.onPlayerSpawn.after += onPlayerSpawn;
            Events.onPlayerDie.after += onPlayerDie;
        }

        private void unsubscribeEvents()
        {
            Events.onPlayerSpawn.after -= onPlayerSpawn;
            Events.onPlayerDie.after -= onPlayerDie;
        }

        private void onPlayerSpawn(FpsActorController controller) => disable();

        private void onPlayerDie(FpsActorController controller) => enable();

        #endregion events

        private AveragePoint getAveragePoint(IEnumerable<Vector3> points)
        {
            var averagePoint = new Vector3();
            var largestGap = 0f;

            foreach (var point in points)
            {
                averagePoint += point;
                foreach (var point2 in points)
                {
                    var dst = Vector3.Distance(point, point2);
                    if (dst > largestGap)
                        largestGap = dst;
                }
            }

            averagePoint /= points.Count();

            return new AveragePoint(averagePoint, largestGap);
        }

        private void updateCameraPosition()
        {
            var points = FindObjectsOfType<SpawnPoint>();
            var avg = getAveragePoint(points.Select(x => x.transform.position));
            var cameraPosition = avg.point - Vector3.forward * avg.largestGap / 2;
            cameraPosition.y = 0f;

            #region increase camera height until all points are visible

            cameraPosition.y = avg.largestGap * 2;

            #endregion increase camera height until all points are visible

            camera.transform.position = cameraPosition;
            cameraContainer.transform.LookAt(avg.point);
        }

        public void enable()
        {
            camera.enabled = true;
            updateCameraPosition();

            RenderSettings.fog = false;
        }

        public void disable()
        {
            camera.enabled = false;
            RenderSettings.fog = true;
        }

        public readonly struct AveragePoint
        {
            public readonly Vector3 point;
            public readonly float largestGap;

            public AveragePoint(Vector3 point, float largestGap)
            {
                this.point = point;
                this.largestGap = largestGap;
            }
        }
    }
}