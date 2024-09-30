using rose.row.default_package;
using rose.row.easy_events;
using UnityEngine;

namespace rose.row.actor.player.camera
{
    public class PlayerCameraManager : Singleton<PlayerCameraManager>
    {
        public static bool hasSetupEvents;

        public PlayerFpParent fpParent;
        public Transform additionalCamera;

        public Camera fpCamera => fpParent.fpCamera;
        public Transform fpCameraTransform => fpParent.fpCamera.transform;

        public void setup(PlayerFpParent fpParent)
        {
            this.fpParent = fpParent;

            additionalCamera = new GameObject("Camera Additional Parent").transform;
            additionalCamera.SetParent(fpCameraTransform.parent);
            additionalCamera.localPosition = fpCameraTransform.localPosition;
            additionalCamera.localRotation = fpCameraTransform.localRotation;

            fpCameraTransform.SetParent(additionalCamera);
            fpCameraTransform.localPosition = Vector3.zero;
            fpCameraTransform.localRotation = Quaternion.identity;
        }

        private void Awake()
        {
            if (!hasSetupEvents)
            {
                Events.onPlayerDie.after += (fpsActorController) =>
                {
                    fpsActorController.tpCamera.enabled = false;
                };

                Events.onPlayerSpawn.before += (fpsActorController) =>
                {
                    fpsActorController.tpCamera.enabled = true;
                };
                hasSetupEvents = true;
            }
        }
    }
}