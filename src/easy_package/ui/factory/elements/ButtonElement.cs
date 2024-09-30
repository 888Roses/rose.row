using rose.row.data;
using rose.row.easy_package.ui.factory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.easy_package.ui.factory.elements
{
    public class ButtonElement : UiElement, IPointerDownHandler
    {
        public TextElement text;

        public Action<PointerEventData> onClick;

        public override void build()
        {
            text = UiFactory.createUiElement<TextElement>("Text", rectTransform);
            text.setAnchors(Anchors.FillParent);

            image().texture = ImageRegistry.menuButton.get();

            text.setColor(Color.black);
            text.setText(transform.name);
            text.setTextAlign(HorizontalAlignmentOptions.Geometry);
            text.setTextAlign(VerticalAlignmentOptions.Geometry);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onClick?.Invoke(eventData);
        }
    }
}