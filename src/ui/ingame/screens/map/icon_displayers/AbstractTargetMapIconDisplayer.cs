using UnityEngine;

namespace rose.row.ui.ingame.screens.map.icon_displayers
{
    public abstract class AbstractTargetMapIconDisplayer : AbstractMovingMapIconDisplayer
    {
        public abstract Transform target { get; }

        public override Vector3 worldPosition => target.position;
    }
}