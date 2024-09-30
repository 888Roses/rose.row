using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using UnityEngine;

namespace rose.row.main_menu.ui.abstraction.elements
{
    /// <summary>
    /// An UiElement that has a border and a background.
    /// </summary>
    public class BorderedUiElement : UiElement
    {
        public const int k_BorderTop = 0;
        public const int k_BorderBottom = 1;
        public const int k_BorderRight = 2;
        public const int k_BorderLeft = 3;

        public Color32 backgroundColor = new Color32(0, 0, 0, 255);
        public Color32 borderColor = new Color32(255, 255, 255, 127);
        public float thickness = 1;
        public bool borderCorners = true;

        #region components

        private UiElement _background;
        private UiElement[] _borders;

        #endregion components

        #region accessors

        public UiElement background => _background;
        public UiElement[] borders => _borders;
        public UiElement borderTop => _borders[k_BorderTop];
        public UiElement borderBottom => _borders[k_BorderBottom];
        public UiElement borderRight => _borders[k_BorderRight];
        public UiElement borderLeft => _borders[k_BorderLeft];

        #endregion accessors

        // Overriding awake so it doesn't get built as soon as it's being instantiated.
        protected override void Awake()
        { }

        public override void build()
        {
            _background = UiFactory.createGenericUiElement(
                name: "Background",
                element: this
            );
            _background.setAnchors(Anchors.FillParent);
            _background.setOffset(thickness, thickness, -thickness, -thickness);
            _background.image().color = backgroundColor;

            _borders = new UiElement[4];

            _borders[k_BorderTop] = createBorder(
                anchors: Anchors.StretchTop,
                height: thickness);
            _borders[k_BorderTop].setPivot(new Vector2(0.5f, 1));
            if (!borderCorners)
                _borders[k_BorderTop].setOffset(thickness, 0, -thickness, 0);

            _borders[k_BorderBottom] = createBorder(
                anchors: Anchors.StretchBottom,
                height: thickness);
            _borders[k_BorderBottom].setPivot(new Vector2(0.5f, 0));
            if (!borderCorners)
                _borders[k_BorderBottom].setOffset(thickness, 0, -thickness, 0);

            _borders[k_BorderRight] = createBorder(
                anchors: Anchors.StretchRight,
                width: thickness);
            _borders[k_BorderRight].setPivot(new Vector2(1, 0.5f));
            if (!borderCorners)
                _borders[k_BorderRight].setOffset(0, thickness, 0, -thickness);

            _borders[k_BorderLeft] = createBorder(
                anchors: Anchors.StretchLeft,
                width: thickness);
            _borders[k_BorderLeft].setPivot(new Vector2(0, 0.5f));
            if (!borderCorners)
                _borders[k_BorderLeft].setOffset(0, thickness, 0, -thickness);

            foreach (var border in _borders)
                border.setAnchoredPosition(0, 0);

            _background.moveToFront();
        }

        private UiElement createBorder(
            Anchors anchors,
            float? width = null,
            float? height = null
        )
        {
            var element = UiFactory.createGenericUiElement("Border", this);
            element.setAnchors(anchors);
            element.setAnchoredPosition(0, 0);
            element.image().color = borderColor;

            if (width.HasValue)
                element.setWidth(width.Value);

            if (height.HasValue)
                element.setHeight(height.Value);

            return element;
        }
    }
}