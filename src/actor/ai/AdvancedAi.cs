using UnityEngine;

namespace rose.row.actor.ai
{
    public class AdvancedAi : MonoBehaviour
    {
        public AiActorController controller;
        public AiWhistle whistle;

        private void Awake()
        {
            whistle = use<AiWhistle>();
        }

        public T use<T>() where T : AiBehaviour
        {
            var component = gameObject.AddComponent<T>();
            component.ai = this;
            return component;
        }
    }
}
