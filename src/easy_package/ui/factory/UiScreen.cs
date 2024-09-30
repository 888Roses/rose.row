using UnityEngine;
using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory
{
    public struct UiScreen
    {
        public readonly GameObject gameObject;
        public readonly Canvas canvas;
        public readonly CanvasRenderer renderer;
        public readonly GraphicRaycaster raycaster;
        public readonly CanvasScaler scaler;
        public CanvasGroup canvasGroup;

        public Transform transform => gameObject.transform;

        public UiScreen(GameObject gameObject,
                        Canvas canvas,
                        CanvasRenderer renderer,
                        GraphicRaycaster raycaster,
                        CanvasScaler scaler)
        {
            this.gameObject = gameObject;
            this.canvas = canvas;
            this.renderer = renderer;
            this.raycaster = raycaster;
            this.scaler = scaler;
            canvasGroup = null;
        }

        public UiScreen withCanvasGroup()
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            return this;
        }
    }
}