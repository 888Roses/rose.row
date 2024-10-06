using MapEditor;
using rose.row.easy_package.ui.factory.util;
using rose.row.util;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory.elements
{
    public class UiElement : MonoBehaviour
    {
        public RectTransform rectTransform => transform as RectTransform;
        private RawImage _rawImage;

        private Dictionary<Type, Behaviour> _usedComponents
            = new Dictionary<Type, Behaviour>();

        public virtual void build()
        { }

        protected virtual void Awake()
        {
            build();
        }

        public void setBackgroundColor(string color)
        {
            if (ColorUtility.TryParseHtmlString(color, out Color parsedColor))
                setBackgroundColor(parsedColor);
        }

        public void setBackgroundColor(Color color)
        {
            image().color = color;
        }

        public void setBackground(Texture2D texture) => image().texture = texture;
        public void setBackground(Texture texture) => setBackground(texture as Texture2D);
        public void setBackground(Sprite sprite) => setBackground(sprite.texture);

        public virtual float relativeWidth(float width, float baseWidth = 1920f)
        {
            return width / baseWidth * Screen.width;
        }

        public virtual float relativeHeight(float width, float baseHeight = 1080f)
        {
            return width / baseHeight * Screen.height;
        }

        #region style

        public enum Pivot
        {
            TopLeft, TopCenter, TopRight,
            MiddleLeft, MiddleCenter, MiddleRight,
            BottomLeft, BottomCenter, BottomRight,
        }

        public enum Anchors
        {
            TopLeft, TopCenter, TopRight,
            MiddleLeft, MiddleCenter, MiddleRight,
            BottomLeft, BottomCenter, BottomRight,

            StretchTop, StretchMiddle, StretchBottom,
            StretchLeft, StretchCenter, StretchRight,

            FillParent
        }

        public struct LiteralAnchors
        {
            public float minX;
            public float minY;
            public float maxX;
            public float maxY;

            public LiteralAnchors(float minX, float minY, float maxX, float maxY)
            {
                this.minX = minX;
                this.minY = minY;
                this.maxX = maxX;
                this.maxY = maxY;
            }

            public override bool Equals(object obj)
            {
                if (obj is LiteralAnchors otherAnchors)
                    return this == otherAnchors;

                return false;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator ==(LiteralAnchors a, LiteralAnchors b)
                => a.minX == b.minX && a.minY == b.minY && a.maxX == b.maxX && a.maxY == b.maxY;

            public static bool operator !=(LiteralAnchors a, LiteralAnchors b)
                => a.minX != b.minX && a.minY != b.minY && a.maxX != b.maxX && a.maxY != b.maxY;
        }

        public void setAnchors(Anchors anchors, bool updateOffsets = true, bool updatePivot = false)
        {
            setAnchors(anchors.toLiteralAnchors(), updateOffsets, updatePivot);
        }

        public void setAnchors(LiteralAnchors anchors, bool updateOffsets = true, bool updatePivot = false)
        {
            rectTransform.anchorMin = new Vector2(anchors.minX, anchors.minY);
            rectTransform.anchorMax = new Vector2(anchors.maxX, anchors.maxY);

            if (updateOffsets)
            {
                setOffset(Vector4.zero);
            }

            if (updatePivot)
            {
                setPivot(anchors.toAnchors().toPivot());
            }
        }

        public void setPivot(Pivot pivot) => setPivot(pivot.toVector2());

        public void setPivot(float x, float y) => setPivot(new Vector2(x, y));

        public void setPivot(Vector2 pivot)
        {
            rectTransform.pivot = pivot;
        }

        public void setOffset(Vector4 offset)
        {
            rectTransform.offsetMin = new Vector2(offset.x, offset.y);
            rectTransform.offsetMax = new Vector2(offset.z, offset.w);
        }

        public void setOffset(Vector2 min, Vector2 max) => setOffset(new Vector4(min.x, min.y, max.x, max.y));

        public void setOffset(float minX, float minY, float maxX, float maxY) => setOffset(new Vector4(minX, minY, maxX, maxY));

        public virtual void setWidth(float width) => rectTransform.sizeDelta = rectTransform.sizeDelta.with(x: width);

        public virtual void setHeight(float height) => rectTransform.sizeDelta = rectTransform.sizeDelta.with(y: height);

        public void setSize(float width, float height)
        {
            setSize(new Vector2(width, height));
        }

        public void setSize(Vector2 size)
        {
            setWidth(size.x);
            setHeight(size.y);
        }

        public void setSize(float size)
        {
            setSize(Vector2.one * size);
        }

        public virtual float getWidth() => rectTransform.rect.width;

        public virtual float getHeight() => rectTransform.rect.height;

        public virtual Vector2 size => rectTransform.sizeDelta;

        public virtual void setAnchoredPosition(Vector2 anchoredPosition) => rectTransform.anchoredPosition = anchoredPosition;

        public virtual void setAnchoredPosition(float x, float y) => setAnchoredPosition(new Vector2(x, y));

        public Vector2 anchoredPosition => rectTransform.anchoredPosition;

        public void moveToFront() => rectTransform.SetAsLastSibling();

        public void moveToBack() => rectTransform.SetAsFirstSibling();

        public void flipVertical() => rectTransform.localEulerAngles += Vector3.forward * 180f;

        public void setRotation(float rotation) => rectTransform.localEulerAngles = Vector3.forward * rotation;

        public void setParent(UiElement parent) => transform.SetParent(parent.transform);

        public RawImage image()
        {
            if (_rawImage == null)
                _rawImage = rectTransform.gameObject.GetOrCreateComponent<RawImage>();

            return _rawImage;
        }

        public void setImage(RawImage image)
        {
            _rawImage = image;
        }

        public T use<T>() where T : Behaviour
        {
            if (!_usedComponents.ContainsKey(typeof(T)))
                _usedComponents.Add(typeof(T), rectTransform.addComponent<T>());

            return _usedComponents[typeof(T)] as T;
        }

        public void disableBackground() => setBackgroundEnabled(false);

        public void enableBackground() => setBackgroundEnabled(true);

        public void setBackgroundEnabled(bool enabled)
        { if (_rawImage != null) _rawImage.enabled = enabled; }

        #endregion style
    }
}