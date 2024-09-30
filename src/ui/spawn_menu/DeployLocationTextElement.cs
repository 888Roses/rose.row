using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;
using UnityEngine;

namespace rose.row.ui.spawn_menu
{
    public class DeployLocationTextElement : UiElement
    {
        public TextElement text;

        public override void build()
        {
            text = UiFactory.createUiElement<TextElement>(
                name: "Text",
                element: this);
            text.build();
            text.setAnchors(Anchors.FillParent);

            text.setFont(Fonts.defaultFont);
            text.setFontSize(36.5f);
            text.setText("Select Deploy Location");
            text.setColor("#ECE7D5");

            text.setTextAlign(
                HorizontalAlignmentOptions.Geometry,
                VerticalAlignmentOptions.Geometry);

            text.setShadow(
                offset: new Vector2(-1, 1),
                color: Color.black);
        }
    }
}
