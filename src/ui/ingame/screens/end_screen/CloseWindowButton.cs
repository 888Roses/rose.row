using rose.row.data;
using rose.row.data.localisation;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.ui.ingame.screens.end_screen
{
    public class CloseWindowButton : UiElement, IPointerClickHandler
    {
        public const string k_TextContent = "Menus/GameOverScreen/Close";

        public TextElement text;

        public override void build()
        {
            setSize(101, 30); // HNG size
            image().texture = ImageRegistry.endScreenQuitButton.get();

            text = UiFactory.createUiElement<TextElement>("Text", this);
            text.setAnchors(Anchors.FillParent);
            text.build();

            text.setTextAlign(HorizontalAlignmentOptions.Geometry);
            text.setTextAlign(VerticalAlignmentOptions.Geometry);
            text.setFont(Fonts.defaultFont);
            text.setColor(Color.white);
            text.setText(Local.get(k_TextContent));
            text.setFontSize(20f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.ReturnToMenu();
        }
    }
}
