using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;
using UnityEngine;

namespace rose.row.main_menu.ui.desktop.elements
{
    public class HeaderButtonElement : UiElement
    {
        public float fontSize = 32f;
        public string text = "BUTTON";

        public Color32 backgroundColor = new Color32(0, 0, 0, 0);
        public Color32 color = new Color32(255, 255, 255, 255);

        public Color32 selectedBackgroundColor = new Color32(31, 27, 24, 255);
        public Color32 selectedColor = new Color32(0, 0, 0, 255);

        public bool selected;

        #region components

        private UiElement _background;
        private TextElement _text;

        public TextElement textElement => _text;

        #endregion components

        protected override void Awake()
        { }

        public override void build()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);

            _text = UiFactory.createUiElement<TextElement>("Text", this);
            _text.setAnchors(Anchors.FillParent);

            _text.setTextAlign(HorizontalAlignmentOptions.Geometry);
            _text.setTextAlign(VerticalAlignmentOptions.Geometry);
            _text.setFont(Fonts.defaultFont);
            _text.setFontSize(fontSize);
            _text.setFontWeight(FontWeight.Bold);

            updateDisplayedInfo();
        }

        public void updateDisplayedInfo()
        {
            _background.image().color = selected
                ? selectedBackgroundColor
                : backgroundColor;

            _background.image().texture = selected
                ? null
                : ImageRegistry.desktopMenuButton.get();

            _text.setColor(selected ? selectedColor : color);
            _text.setText(text);
        }
    }
}