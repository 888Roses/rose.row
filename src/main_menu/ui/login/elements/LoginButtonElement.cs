using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.main_menu.ui.login.elements
{
    /// <summary>
    /// Represents the big gray button to login on the <see cref="LoginWindowElement"/>.
    /// </summary>
    public class LoginButtonElement : UiElement, IPointerClickHandler
    {
        public Color32 backgroundColor = new Color32(31, 27, 24, 255);
        public string text = "LOG IN";

        #region components

        private UiElement _background;
        private TextElement _text;

        #endregion components

        public override void build()
        {
            base.build();

            _background = UiFactory.createGenericUiElement("Background", this);
            applyStandardStyling(_background);

            _text = UiFactory.createUiElement<TextElement>("Text", this);
            applyStandardStyling(_text);

            _text.setTextAlign(HorizontalAlignmentOptions.Geometry);
            _text.setTextAlign(VerticalAlignmentOptions.Geometry);
            _text.setColor(LoginScreen.textColor);
            _text.setFont(Fonts.defaultFont);
            _text.setFontSize(32f);
            _text.setFontWeight(FontWeight.Bold);
        }

        /// <summary>
        /// Applies a common styling for a given component.
        /// </summary>
        /// <param name="component">
        /// A style that will be applied to every component in this <see cref="UiElement"/>.
        /// </param>
        private void applyStandardStyling(UiElement component)
        {
            component.setAnchors(Anchors.FillParent);
        }

        /// <summary>
        /// Updates the information and style shown on the different elements of that UiElement.
        /// </summary>
        public void updateInformation()
        {
            _background.image().color = backgroundColor;
            _text.setText(text);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            MainMenuUiManager.instance.loginScreen.login();
        }
    }
}