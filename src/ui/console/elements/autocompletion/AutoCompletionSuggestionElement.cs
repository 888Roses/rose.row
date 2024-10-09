using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using TMPro;
using UnityEngine.EventSystems;

namespace rose.row.ui.console.elements.autocompletion
{
    public class AutoCompletionSuggestionElement : UiElement, IPointerEnterHandler, IPointerDownHandler
    {
        public TextElement text;

        private bool _isSelected;
        public bool isSelected => _isSelected;

        public Action onHovered;
        public Action onClicked;

        //public AbstractConsoleCommand command;
        public string suggestion => text.text.text;

        public string completedText;

        public override void build()
        {
            setHeight(24f);
            image();

            text = UiFactory.createUiElement<TextElement>("Text", this);
            text.setAnchors(Anchors.FillParent);
            text.setFont(Fonts.consoleFont);
            text.setColor("#D7D7D7");
            text.setFontSize(16f);
            text.setTextAlign(VerticalAlignmentOptions.Middle);
            text.setAllowRichText(true);
            setSelected(false);
        }

        public void Update()
        {
            text.setFontSize(relativeHeight(16f));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onHovered?.Invoke();
        }

        public void setSelected(bool selected)
        {
            setBackgroundColor(selected ? "#28282850" : "#0A0A0A50");
        }

        public void setSuggestion(string text, string completedText)
        {
            //var message = new TextComponent(command.root);
            //var style = TextStyle.empty.withColor(ConsoleColors.autoCompletionSuggestionDescription);
            //message.append(new TextComponent(" " + command.description).withStyle(style));
            //text.setContent(message.getString());

            //this.command = command;

            this.completedText = completedText;
            this.text.setText(text);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onClicked?.Invoke();
        }
    }
}