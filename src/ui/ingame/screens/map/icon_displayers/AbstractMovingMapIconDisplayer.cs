using rose.row.actor.player.camera;
using UnityEngine;

namespace rose.row.ui.ingame.screens.map.icon_displayers
{
    public abstract class AbstractMovingMapIconDisplayer : AbstractMapIconDisplayer
    {
        public abstract Vector3 worldPosition { get; }

        public virtual Vector3 getScreenPosition(bool nullZPos = true)
        {
            Camera camera;
            if (OldDeathCamera.instance.camera.enabled)
                camera = OldDeathCamera.instance.camera;
            else
                camera = Camera.main;

            var screenPosition = camera.WorldToScreenPoint(worldPosition);
            if (nullZPos)
                screenPosition.z = 0;

            return screenPosition;
        }
    }
}