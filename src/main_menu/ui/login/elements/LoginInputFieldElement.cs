using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.main_menu.ui.abstraction.elements;
using TMPro;
using UnityEngine;

namespace rose.row.main_menu.ui.login.elements
{
    /// <summary>
    /// Reponsible for allowing the user to input a username, password and such.
    /// </summary>
    public class LoginInputFieldElement : UiElement
    {
        public string placeholder;

        #region components

        private BorderedUiElement _background;
        private InputFieldElement _inputField;

        public InputFieldElement inputField => _inputField;

        #endregion components

        protected override void Awake()
        { }

        public override void build()
        {
            _background = UiFactory.createUiElement<BorderedUiElement>("Background", this);
            _background.borderColor = new Color32(190, 190, 190, 255);
            _background.thickness = 3;
            _background.borderCorners = true;
            _background.build();
            _background.background.image().texture = ImageRegistry.fieldShadow.get();

            _inputField = UiFactory.createUiElement<InputFieldElement>("Input Field", this);
            _inputField.font = Fonts.defaultFont;
            _inputField.fontSize = 24f;
            _inputField.textColor = Color.white;
            _inputField.placeholderColor = new Color32(235, 235, 235, 255);
            _inputField.padding = Vector2.zero;

            _inputField.build();

            _inputField.placeholder.setText(placeholder);
            _inputField.placeholder.text.fontStyle = FontStyles.Normal;
            setAlignment(_inputField.placeholder);
            setAlignment(_inputField.text);
            _inputField.setAnchors(Anchors.FillParent);
            _background.setAnchors(Anchors.FillParent);
            _background.setOffset(-1, -1, 1, 1);
        }

        private void setAlignment(TextElement element)
        {
            element.setTextAlign(HorizontalAlignmentOptions.Geometry);
            element.setTextAlign(VerticalAlignmentOptions.Geometry);
        }

        public string text => _inputField.inputField.text;
    }
}