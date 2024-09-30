using UnityEngine;

namespace rose.row.actor.player
{
    public class Player : MonoBehaviour
    {
        public FpsActorController controller;

        public PlayerWhistle whistle;

        public static void create(FpsActorController controller)
        {
            var player = controller.gameObject.AddComponent<Player>();
            player.controller = controller;
        }

        private void Awake()
        {
            whistle = use<PlayerWhistle>();
        }

        public T use<T>() where T : PlayerBehaviour
        {
            var component = gameObject.AddComponent<T>();
            component.player = this;
            return component;
        }
    }
}
