using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.ui.ingame.screens.map.icon_displayers
{
    public abstract class AbstractMapIconDisplayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        #region ui

        public abstract void buildUi();

        public virtual RectTransform rectTransform => (RectTransform) transform;

        #endregion ui

        #region mouse events

        public Action<PointerEventData> onMouseDown;
        public Action<PointerEventData> onMouseEnter;
        public Action<PointerEventData> onMouseExit;

        protected virtual void mouseDown(PointerEventData eventData)
        { }

        protected virtual void mouseEnter(PointerEventData eventData)
        { }

        protected virtual void mouseExit(PointerEventData eventData)
        { }

        public void OnPointerDown(PointerEventData eventData)
        {
            mouseDown(eventData);
            onMouseDown?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseEnter(eventData);
            onMouseEnter?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseExit(eventData);
            onMouseExit?.Invoke(eventData);
        }

        #endregion mouse events
    }
}