using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;

namespace rose.row.ui.spawn_menu
{
    public class StandardTextElement : UiElement
    {
        public TextElement text;
        public string content;

        protected override void Awake()
        {
        }

        public override void build()
        {
            text = UiFactory.createUiElement<TextElement>(
                name: "Text",
                element: this);
            text.build();
            text.setAnchors(Anchors.FillParent);

            text.setFont(Fonts.defaultFont);
            text.setFontSize(36.5f);
            text.setText(content);
            text.setColor("#ECE7D5");

            text.setTextAlign(
                HorizontalAlignmentOptions.Geometry,
                VerticalAlignmentOptions.Geometry);
        }
    }
}
