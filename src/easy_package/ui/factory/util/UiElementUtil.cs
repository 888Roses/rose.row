using rose.row.easy_package.ui.factory.elements;
using UnityEngine;

namespace rose.row.easy_package.ui.factory.util
{
    public static class UiElementUtil
    {
        public static UiElement.LiteralAnchors toLiteralAnchors(this UiElement.Anchors anchors)
        {
            switch (anchors)
            {
                case UiElement.Anchors.TopLeft:
                    return new UiElement.LiteralAnchors(0, 1, 0, 1);

                case UiElement.Anchors.TopCenter:
                    return new UiElement.LiteralAnchors(0.5f, 1, 0.5f, 1);

                case UiElement.Anchors.TopRight:
                    return new UiElement.LiteralAnchors(1, 1, 1, 1);

                case UiElement.Anchors.MiddleLeft:
                    return new UiElement.LiteralAnchors(0, 0.5f, 0, 0.5f);

                case UiElement.Anchors.MiddleCenter:
                    return new UiElement.LiteralAnchors(0.5f, 0.5f, 0.5f, 0.5f);

                case UiElement.Anchors.MiddleRight:
                    return new UiElement.LiteralAnchors(1, 0.5f, 1, 0.5f);

                case UiElement.Anchors.BottomLeft:
                    return new UiElement.LiteralAnchors(0, 0, 0, 0);

                case UiElement.Anchors.BottomCenter:
                    return new UiElement.LiteralAnchors(0.5f, 0, 0.5f, 0);

                case UiElement.Anchors.BottomRight:
                    return new UiElement.LiteralAnchors(1, 0, 1, 0);

                case UiElement.Anchors.StretchTop:
                    return new UiElement.LiteralAnchors(0, 1, 1, 1);

                case UiElement.Anchors.StretchMiddle:
                    return new UiElement.LiteralAnchors(0, 0.5f, 1, 0.5f);

                case UiElement.Anchors.StretchBottom:
                    return new UiElement.LiteralAnchors(0, 0, 1, 0);

                case UiElement.Anchors.StretchLeft:
                    return new UiElement.LiteralAnchors(0, 0, 0, 1);

                case UiElement.Anchors.StretchCenter:
                    return new UiElement.LiteralAnchors(0.5f, 0, 0.5f, 1);

                case UiElement.Anchors.StretchRight:
                    return new UiElement.LiteralAnchors(1, 0, 1, 1);

                case UiElement.Anchors.FillParent:
                    return new UiElement.LiteralAnchors(0, 0, 1, 1);

                default:
                    return new UiElement.LiteralAnchors(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }

        public static UiElement.Anchors toAnchors(this UiElement.LiteralAnchors anchors)
        {
            if (anchors == new UiElement.LiteralAnchors(0, 1, 0, 1))
                return UiElement.Anchors.TopLeft;
            if (anchors == new UiElement.LiteralAnchors(0.5f, 1, 0.5f, 0))
                return UiElement.Anchors.TopCenter;
            if (anchors == new UiElement.LiteralAnchors(1, 1, 1, 1))
                return UiElement.Anchors.TopRight;
            if (anchors == new UiElement.LiteralAnchors(0, 0.5f, 0, 0.5f))
                return UiElement.Anchors.MiddleLeft;
            if (anchors == new UiElement.LiteralAnchors(0.5f, 0.5f, 0.5f, 0.5f))
                return UiElement.Anchors.MiddleCenter;
            if (anchors == new UiElement.LiteralAnchors(1, 0.5f, 1, 0.5f))
                return UiElement.Anchors.MiddleRight;
            if (anchors == new UiElement.LiteralAnchors(0, 0, 0, 0))
                return UiElement.Anchors.BottomLeft;
            if (anchors == new UiElement.LiteralAnchors(0.5f, 0, 0.5f, 0))
                return UiElement.Anchors.BottomCenter;
            if (anchors == new UiElement.LiteralAnchors(1, 0, 1, 0))
                return UiElement.Anchors.BottomRight;
            if (anchors == new UiElement.LiteralAnchors(0, 1, 1, 1))
                return UiElement.Anchors.StretchTop;
            if (anchors == new UiElement.LiteralAnchors(0, 0.5f, 1, 0.5f))
                return UiElement.Anchors.StretchMiddle;
            if (anchors == new UiElement.LiteralAnchors(0, 0, 1, 0))
                return UiElement.Anchors.StretchBottom;
            if (anchors == new UiElement.LiteralAnchors(0, 0, 0, 1))
                return UiElement.Anchors.StretchLeft;
            if (anchors == new UiElement.LiteralAnchors(0.5f, 0, 0.5f, 1))
                return UiElement.Anchors.StretchCenter;
            if (anchors == new UiElement.LiteralAnchors(1, 0, 1, 1))
                return UiElement.Anchors.StretchRight;
            if (anchors == new UiElement.LiteralAnchors(0, 0, 1, 1))
                return UiElement.Anchors.FillParent;
            return UiElement.Anchors.MiddleCenter;
        }

        public static UiElement.Pivot toPivot(this UiElement.Anchors anchors)
        {
            switch (anchors)
            {
                case UiElement.Anchors.TopLeft:
                    return UiElement.Pivot.TopLeft;

                case UiElement.Anchors.TopCenter:
                    return UiElement.Pivot.TopCenter;

                case UiElement.Anchors.TopRight:
                    return UiElement.Pivot.TopRight;

                case UiElement.Anchors.MiddleLeft:
                    return UiElement.Pivot.MiddleLeft;

                case UiElement.Anchors.MiddleCenter:
                    return UiElement.Pivot.MiddleCenter;

                case UiElement.Anchors.MiddleRight:
                    return UiElement.Pivot.MiddleRight;

                case UiElement.Anchors.BottomLeft:
                    return UiElement.Pivot.BottomLeft;

                case UiElement.Anchors.BottomCenter:
                    return UiElement.Pivot.BottomCenter;

                case UiElement.Anchors.BottomRight:
                    return UiElement.Pivot.BottomRight;

                default:
                    return UiElement.Pivot.MiddleCenter;
            };
        }

        public static Vector2 toVector2(this UiElement.Pivot pivot)
        {
            switch (pivot)
            {
                case UiElement.Pivot.TopLeft:
                    return new Vector2(0, 1);

                case UiElement.Pivot.TopCenter:
                    return new Vector2(0.5f, 1);

                case UiElement.Pivot.TopRight:
                    return new Vector2(1, 1);

                case UiElement.Pivot.MiddleLeft:
                    return new Vector2(0, 0.5f);

                case UiElement.Pivot.MiddleCenter:
                    return new Vector2(0.5f, 0.5f);

                case UiElement.Pivot.MiddleRight:
                    return new Vector2(1, 0.5f);

                case UiElement.Pivot.BottomLeft:
                    return new Vector2(0, 0);

                case UiElement.Pivot.BottomCenter:
                    return new Vector2(0.5f, 0);

                case UiElement.Pivot.BottomRight:
                    return new Vector2(1, 0);

                default:
                    return new Vector2(0.5f, 0.5f);
            }
        }
    }
}