using rose.row.util;
using System;
using UnityEngine;

namespace rose.row.ui.ingame.ingame_displayables
{
    public abstract class AbstractDisplayable : MonoBehaviour
    {
        public virtual Vector3 offset { get => Vector3.up * 0.5f; }
        public virtual float iconSize { get => 48f; }
        public virtual float maxVisibleDistance { get => 10f; }

        public abstract Texture2D icon { get; }
        public virtual Type displayableUiWidgetClass => typeof(DisplayableUiWidget);

        public virtual Vector3 position => transform.position + offset;

        public virtual float getAlpha(Camera playerCamera)
        {
            var viewportPos = playerCamera.WorldToViewportPoint(position).with(z: 0);
            var distanceX = 1 - Mathf.Abs(0.5f - viewportPos.x);
            var distanceY = 1 - Mathf.Abs(0.5f - viewportPos.y);
            var baseAlpha = Mathf.Min(distanceX, distanceY);

            var dst = Vector3.Distance(position, playerCamera.transform.position);
            var distanceMultiplier = Mathf.Floor(Mathf.Min(1, maxVisibleDistance / dst) * 10) / 10f;
            var dir = 1 - Vector3.Dot(
                lhs: (playerCamera.transform.position - position),
                rhs: playerCamera.transform.forward
            );
            var directionMultiplier = Mathf.Max(0, dir);

            return Mathf.Min(baseAlpha * distanceMultiplier, directionMultiplier);
        }
    }
}