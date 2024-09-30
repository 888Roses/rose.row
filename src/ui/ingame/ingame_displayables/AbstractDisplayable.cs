using UnityEngine;

namespace rose.row.ui.ingame.ingame_displayables
{
    public abstract class AbstractDisplayable : MonoBehaviour
    {
        public virtual Vector3 offset { get => Vector3.up * 0.5f; }
        public virtual float iconSize { get => 48f; }
        public virtual float maxVisibleDistance { get => 10f; }

        public abstract Texture2D icon { get; }
    }
}