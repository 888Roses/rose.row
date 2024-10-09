using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.ui.console.elements
{
    public class ConsoleTextElement : UiElement
    {
        public TextElement text;

        private string _message;
        public string message => _message;

        private bool _wasBuilt = false;

        public void setMessage(string message)
        {
            if (!_wasBuilt)
            {
                build();
            }

            _message = message;
            updateText();
        }

        public void updateText()
        {
            text.setText(_message + " " + text.text.preferredHeight);
            LayoutRebuilder.MarkLayoutForRebuild(text.rectTransform);
        }

        private void Update()
        {
            setHeight(text.text.rectTransform.sizeDelta.y);
        }

        public override void build()
        {
            text = UiFactory.createUiElement<TextElement>("Text", this);
            text.setAnchors(Anchors.StretchTop);
            text.setPivot(0.5f, 1);
            text.setColor(Console.getColorForLogType(LogType.Log));
            text.setFontSize(16);
            text.setFont(Fonts.consoleFont);
            text.setAllowRichText(true);

            _wasBuilt = true;
        }
    }
}