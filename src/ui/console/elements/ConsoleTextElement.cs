using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using TMPro;
using UnityEngine.UI;

namespace rose.row.ui.console.elements
{
    public class ConsoleTextElement : UiElement
    {
        public TextElement text;

        private string _message;

        public string message
        {
            get => _message;
            set
            {
                _message = value;

                if (_message.Contains("\n"))
                {
                    text.use<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }

                text.setText(value);
            }
        }

        public override void build()
        {
            text = UiFactory.createUiElement<TextElement>("Text", this);
            text.setAnchors(Anchors.FillParent);
            text.setTextAlign(HorizontalAlignmentOptions.Left);
            text.setTextAlign(VerticalAlignmentOptions.Middle);
            text.setColor(ConsoleColors.log);
            text.setFontSize(16);
            text.setFont(Fonts.consoleFont);
            text.setAllowRichText(true);
        }
    }
}