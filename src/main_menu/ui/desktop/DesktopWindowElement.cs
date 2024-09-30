using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using rose.row.main_menu.ui.abstraction.elements;
using rose.row.main_menu.ui.desktop.elements;
using rose.row.main_menu.ui.desktop.war;
using UnityEngine;

namespace rose.row.main_menu.ui.desktop
{
    public class DesktopWindowElement : AbstractWindowElement
    {
        public const float k_HeaderHeight = 50f;

        // Makes it so this element is fullscreen.
        public override float anchorX => 0;

        public override float anchorY => 0;

        public override void build()
        {
            base.build();
            createBackground();
            createHeader();

            var warWindow = UiFactory.createUiElement<WarWindowElement>("War Window", this);
            warWindow.setAnchors(Anchors.FillParent);
            warWindow.setOffset(0, 0, 0, -k_HeaderHeight);
            warWindow.build();
        }

        #region header

        private UiElement _header;

        private UiElement _headerCentreButtonsContainer;
        private HeaderButtonElement _headerHeroesButton;
        private HeaderButtonElement _headerGeneralsButton;
        private HeaderButtonElement _combatButton;

        private void createHeader()
        {
            #region header

            _header = UiFactory.createGenericUiElement("Header", _wrapper);
            _header.setAnchors(Anchors.StretchTop);
            _header.setPivot(0, 1);
            _header.setAnchoredPosition(0, 0);
            _header.setHeight(k_HeaderHeight);
            _header.image().color = Color.black;

            #endregion header

            #region centre button container

            _headerCentreButtonsContainer = UiFactory.createGenericUiElement(
                name: "Centre Buttons Container",
                element: _header
            );
            _headerCentreButtonsContainer.setAnchors(Anchors.StretchCenter);
            _headerCentreButtonsContainer.setWidth(438f);

            #endregion centre button container

            #region heroes button

            _headerHeroesButton = createHeaderButton(
                name: "Heroes Button",
                text: "HEROES",
                selected: false
            );
            _headerHeroesButton.setPivot(0, 0.5f);
            _headerHeroesButton.setAnchors(Anchors.MiddleLeft, updateOffsets: false);
            _headerHeroesButton.setAnchoredPosition(0, 0);

            #endregion heroes button

            #region generals button

            _headerGeneralsButton = createHeaderButton(
                name: "Generals Button",
                text: "GENERALS",
                selected: true
            );
            _headerGeneralsButton.setPivot(1, 0.5f);
            _headerGeneralsButton.setAnchors(Anchors.MiddleRight, updateOffsets: false);
            _headerGeneralsButton.setAnchoredPosition(0, 0);

            #endregion generals button

            #region combat button

            _combatButton = UiFactory.createUiElement<HeaderButtonElement>(
                name: "Combat Button",
                element: _wrapper
            );

            _combatButton.selected = true;
            _combatButton.selectedBackgroundColor = new Color32(100, 30, 34, 255);
            _combatButton.selectedColor = Color.white;
            _combatButton.fontSize = 36f;
            _combatButton.setSize(202, 48);
            _combatButton.text = "ENTER COMBAT!";
            _combatButton.build();
            _combatButton.setAnchors(Anchors.TopCenter, false);
            _combatButton.setPivot(0.5f, 1);
            _combatButton.setAnchoredPosition(0, -90);

            var underline = UiFactory.createGenericUiElement("Underline", _combatButton);
            underline.image().color = Color.white;
            underline.setAnchors(Anchors.BottomCenter, false);
            underline.setPivot(0.5f, 0);
            underline.setHeight(2);
            underline.setAnchoredPosition(0, 7);
            underline.setWidth(_combatButton.textElement.text.preferredWidth);

            #endregion combat button
        }

        private HeaderButtonElement createHeaderButton(
            string name,
            string text,
            bool selected
        )
        {
            var button = UiFactory.createUiElement<HeaderButtonElement>(
                name: name,
                element: _headerCentreButtonsContainer
            );

            button.selected = selected;

            button.backgroundColor = Color.white;
            // button.backgroundColor = Color.clear;
            button.color = Color.white;

            button.selectedBackgroundColor = new Color32(247, 178, 51, 255);
            button.selectedColor = Color.black;

            button.text = text;
            button.fontSize = 36;
            button.setSize(213, 42);
            button.build();

            return button;
        }

        #endregion header

        #region background

        private UiElement _backgroundContainer;
        private UiElement _backgroundImage;
        private UiElement _backgroundCover;

        private void createBackground()
        {
            _backgroundContainer = UiFactory.createGenericUiElement("Background Image", _wrapper);
            _backgroundContainer.setAnchors(Anchors.FillParent);

            _backgroundImage = UiFactory.createGenericUiElement(
                name: "Background Image",
                element: _backgroundContainer
            );

            _backgroundCover = UiFactory.createGenericUiElement(
                name: "Background Image",
                element: _backgroundContainer
            );

            _backgroundImage.setAnchors(Anchors.FillParent);
            _backgroundCover.setAnchors(Anchors.FillParent);

            _backgroundImage.image().texture = ImageRegistry.desktopBackground.get();
            _backgroundCover.image().texture = ImageRegistry.desktopBackgroundCover.get();
            _backgroundCover.image().color = new Color(1, 1, 1, 0.56f);
        }

        #endregion background
    }
}