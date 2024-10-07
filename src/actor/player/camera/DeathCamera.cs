using rose.row.default_package;
using rose.row.easy_events;
using rose.row.util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace rose.row.actor.player.camera
{
    public class DeathCamera : Singleton<DeathCamera>
    {
        #region events

        public static void subscribeToInitializationEvents()
        {
            Events.onPlayerSpawn.after += onPlayerSpawn;
            Events.onPlayerDie.after += onPlayerDie;
        }

        private static void onPlayerSpawn(FpsActorController controller)
        {
            if (instance != null)
                instance.setEnabled(false);
        }

        private static void onPlayerDie(FpsActorController controller)
        {
            if (instance != null)
                instance.setEnabled(true);
        }

        #endregion

        #region enabled state

        private bool _isEnabled;
        public bool isEnabled => _isEnabled;

        public void setEnabled(bool enabled)
        {
            _isEnabled = enabled;
            updateEnabledState();
        }

        private void updateEnabledState()
        {
            _deathCamera.enabled = _isEnabled;

            if (isEnabled && SceneryCamera.instance.camera.enabled)
                SceneryCamera.instance.camera.enabled = false;
        }

        #endregion

        #region base

        public static void create()
        {
            var gameObject = new GameObject("Death Camera");
            gameObject.AddComponent<DeathCamera>();
        }

        private Camera _deathCamera;

        private void Awake()
        {
            createDeathCamera();
        }

        private void Start()
        {
            updateCameraInitialPositionAndRotation();
            setEnabled(true);
        }

        private void Update()
        {
            updateCameraCurrentPositionAndRotation();
        }

        private void createDeathCamera()
        {
            var gameObject = new GameObject("Camera");
            gameObject.transform.SetParent(transform);
            gameObject.transform.resetLocalTransform();

            _deathCamera = gameObject.AddComponent<Camera>();
            _deathCamera.tag = "MainCamera";
            // So that it has priority over other in-game cameras.
            _deathCamera.depth = 3f;
            // Actually, unlike i first thought, the camera far clip plane being suuper high doesn't impact the performances at all!
            _deathCamera.farClipPlane = 40000;
        }

        #endregion

        #region point calculation

        private AveragePoint getAveragePoint(IEnumerable<Vector3> pointWeb)
        {
            var averagePoint = new Vector3();
            var largestGap = 0f;

            foreach (var point in pointWeb)
            {
                averagePoint += point;
                foreach (var point2 in pointWeb)
                {
                    var dst = Vector3.Distance(point, point2);
                    if (dst > largestGap)
                        largestGap = dst;
                }
            }

            averagePoint /= pointWeb.Count();

            return new AveragePoint(averagePoint, largestGap);
        }

        public readonly struct AveragePoint
        {
            public readonly Vector3 point;
            public readonly float maxDistance;

            public AveragePoint(Vector3 point, float maxDistance)
            {
                this.point = point;
                this.maxDistance = maxDistance;
            }
        }

        #endregion

        #region positionning

        private Vector3 _initialPosition;
        private Vector3 _cameraLookAtPoint;

        private float _cameraLookAtPoint_ChangeTimeStamp;
        private float _cameraLookAtPoint_ChangeDuration = 1.5f;
        private float _cameraLookAtPoint_FinishTimeStamp => _cameraLookAtPoint_ChangeTimeStamp + _cameraLookAtPoint_ChangeDuration;
        private float _cameraLookAtPoint_FinishedPercentage => (Time.time - _cameraLookAtPoint_FinishTimeStamp) / _cameraLookAtPoint_ChangeDuration;

        private float _initialPosition_ChangeTimeStamp;
        private float _initialPosition_ChangeDuration = 1.5f;
        private float _initialPosition_FinishTimeStamp => _initialPosition_ChangeTimeStamp + _initialPosition_ChangeDuration;
        private float _initialPosition_FinishedPercentage => (Time.time - _initialPosition_FinishTimeStamp) / _initialPosition_ChangeDuration;

        private Vector3 getCameraInitialPosition(AveragePoint averagePoint)
        {
            var cameraPosition = averagePoint.point - Vector3.forward * averagePoint.maxDistance / 2;
            cameraPosition.y = averagePoint.maxDistance * 2;

            return cameraPosition;
        }

        public void updateCameraInitialPositionAndRotation()
        {
            var averagePoint = getAveragePoint(ActorManager.instance.spawnPoints.Select(x => x.transform.position));
            var position = getCameraInitialPosition(averagePoint);
            updateCameraInitialLookAtPoint(averagePoint);
            _initialPosition = position;
        }

        private void updateCameraInitialLookAtPoint(AveragePoint averagePoint)
        {
            setCameraLookAtPoint(averagePoint.point);
        }

        private void updateCameraCurrentPositionAndRotation()
        {
            updateCameraCurrentRotation();
        }

        private void updateCameraCurrentRotation()
        {
            var direction = _deathCamera.transform.position - _cameraLookAtPoint;
            direction.Normalize();

            var quaternion = Quaternion.LookRotation(direction);
            var rotation = Quaternion.Slerp(_deathCamera.transform.rotation, quaternion, Mathf.Max(_cameraLookAtPoint_FinishedPercentage, 1));

            _deathCamera.transform.rotation = rotation;
        }

        public void setCameraLookAtPoint(Vector3 lookAtPoint, float duration = 1.5f)
        {
            _cameraLookAtPoint_ChangeDuration = duration;
            _cameraLookAtPoint_ChangeTimeStamp = Time.time;
            _cameraLookAtPoint = lookAtPoint;
        }

        #endregion
    }
}
