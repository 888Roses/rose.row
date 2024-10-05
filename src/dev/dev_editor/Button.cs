using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using TMPro;
using UnityEngine.EventSystems;

namespace rose.row.dev.dev_editor
{
    public class Button : UiElement, IPointerClickHandler
    {
        public Action onClick;

        private TextElement _text;
        public TextElement text => _text;

        public void setText(string text)
        {
            this.text.setText(text);
        }

        public override void build()
        {
            _text = UiFactory.createUiElement<TextElement>("Text", this);
            _text.build();
            _text.setAnchors(Anchors.FillParent);
            _text.setTextAlign(HorizontalAlignmentOptions.Geometry, VerticalAlignmentOptions.Geometry);
            _text.setColor("#d6d6d6");
            _text.setFontSize(18f);
            _text.setFont(Fonts.fancyFont);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}
