using UnityEngine;

namespace rose.row.dev.dev_editor
{
    public static class FlyMode
    {
        public static void toggleFlying()
        {
            if (DevMainInfo.isFlying)
                stopFlying();
            else
                startFlying();
        }

        public static void startFlying()
        {
            DevMainInfo.isFlying = true;

            var controller = FpsActorController.instance;
            var movement = controller.controller;
            movement.DisableCharacterController();

            if (!movement.TryGetComponent(out FlyMovement fly))
                movement.gameObject.AddComponent<FlyMovement>();
        }

        public static void stopFlying()
        {
            DevMainInfo.isFlying = false;

            var controller = FpsActorController.instance;
            var movement = controller.controller;
            movement.EnableCharacterController();
        }
    }

    public class FlyMovement : MonoBehaviour
    {
        public static float getUpInput()
        {
            var upward = 0f;
            var jumpDown = SteelInput.GetButton(SteelInput.KeyBinds.Jump);
            var crouchDown = SteelInput.GetButton(SteelInput.KeyBinds.Crouch);

            if (jumpDown)
            {
                upward = 1f;
            }

            if (crouchDown)
            {
                upward = -1f;
            }

            if ((jumpDown && crouchDown) || (!jumpDown && !crouchDown))
            {
                upward = 0f;
            }

            return upward;
        }

        private void Update()
        {
            if (DevMainInfo.isFlying)
            {
                var ver = SteelInput.GetAxis(SteelInput.KeyBinds.Vertical);
                var hor = SteelInput.GetAxis(SteelInput.KeyBinds.Horizontal);

                var camera = PlayerFpParent.instance.fpCamera.transform;
                var dir = camera.forward * ver + Vector3.up * getUpInput() + camera.right * -hor;
                dir = Vector3.ClampMagnitude(dir, 1f);

                var speed = SteelInput.GetButton(SteelInput.KeyBinds.Sprint) ? 20f : 5f;

                var controller = FpsActorController.instance;
                controller.transform.position += dir * speed * Time.deltaTime;
            }
        }
    }
}
