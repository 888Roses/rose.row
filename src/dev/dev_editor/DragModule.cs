using rose.row.easy_package.ui.factory.elements;
using rose.row.util;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.dev.dev_editor
{
    public class DragModule : AbstractWindowModule
    {
        /// <summary>
        /// Detects when the user starts dragging the rect transform this behaviour is attached to.
        /// </summary>
        public class DragModuleDragger : MonoBehaviour, IPointerDownHandler
        {
            public Action onStartDragging;
            public Action onStopDragging;

            private bool _isDragging = false;
            public bool isDragging => _isDragging;

            public void OnPointerDown(PointerEventData eventData)
            {
                if (eventData.button != 0)
                    return;

                _isDragging = true;
                onStartDragging?.Invoke();
            }

            private void Update()
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _isDragging = false;
                    onStopDragging?.Invoke();
                }
            }
        }

        private RectTransform _target;
        public RectTransform dragSurface => _target;

        private DragModuleDragger _dragger;
        private Vector3 _startDraggingOffset;

        public bool restrictToScreenEdges;

        public DragModule(UiElement dragSurface, bool restrictToScreenEdges = true)
            : this(dragSurface.rectTransform, restrictToScreenEdges) { }

        public DragModule(RectTransform dragSurface, bool restrictToScreenEdges = true)
        {
            _target = dragSurface;

            _dragger = dragSurface.use<DragModuleDragger>();
            _dragger.onStartDragging += onStartDragging;
            this.restrictToScreenEdges = restrictToScreenEdges;
        }

        private void onStartDragging()
        {
            _startDraggingOffset = parent.transform.position - Input.mousePosition;
        }

        public override void update()
        {
            if (_dragger.isDragging)
            {
                var pos = _startDraggingOffset + Input.mousePosition;

                if (restrictToScreenEdges)
                {
                    pos.x = Mathf.Clamp(pos.x, parent.getWidth() / 2f, Screen.width - parent.getWidth() / 2f);
                    pos.y = Mathf.Clamp(pos.y, parent.getHeight() / 2f, Screen.height - parent.getHeight() / 2f);
                }

                parent.transform.position = pos;
            }
        }
    }
}
