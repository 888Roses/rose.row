using rose.row.default_package;
using UnityEngine;

namespace rose.row.actor.player
{
    public class Player : Singleton<Player>
    {
        public FpsActorController controller;

        public PlayerWhistle whistle;
        public PlayerPickupWeapons pickup;
        // TODO: Change this whenever I'll implement vehicles!
        public PlayerVehicleHornTemp horn;
        public PlayerUiUpdater uiUpdater;

        public static void create(FpsActorController controller)
        {
            var player = controller.gameObject.AddComponent<Player>();
            player.controller = controller;
        }

        private void Awake()
        {
            whistle = use<PlayerWhistle>();
            pickup = use<PlayerPickupWeapons>();
            horn = use<PlayerVehicleHornTemp>();
            use<PlayerAntiTankerFuckTanksOmfg>();
            uiUpdater = use<PlayerUiUpdater>();
            use<PlayerFallDamage>();
        }

        public T use<T>() where T : PlayerBehaviour
        {
            var component = gameObject.AddComponent<T>();
            component.player = this;
            return component;
        }

        /// <summary>
        /// A ray coming from the camera's position and going in the player's look direction.
        /// </summary>
        public Ray cameraForward()
        {
            var camera = PlayerFpParent.instance.fpCamera;
            var direction = camera.transform.forward;
            var origin = camera.transform.position;
            return new Ray(origin, direction);
        }
    }
}
