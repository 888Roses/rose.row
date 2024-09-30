using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using UnityEngine;

namespace rose.row.dev.dev_info_screen
{
    public abstract class AbstractDeveloperMenu : UiElement
    {
        public abstract Shortcut activateKey { get; }

        public static AbstractDeveloperMenu create(Type type)
        {
            var gameObject = new GameObject(type.Name);

            var rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.SetParent(DeveloperInfoMainElement.instance.wrapper.transform);
            rectTransform.anchoredPosition = Vector2.zero;

            var menu = rectTransform.gameObject.AddComponent(type) as AbstractDeveloperMenu;
            return menu;
        }

        public override void build()
        {
            base.build();
            setAnchors(Anchors.FillParent);
        }

        public TextElement createStandardText(string name, string text, UiElement parent)
        {
            var element = UiFactory.createUiElement<TextElement>(name, parent);
            element.build();

            element.setFont(Fonts.fancyFont);
            element.setFontSize(16f);
            element.setColor(Color.white);

            element.setText(text);

            return element;
        }
    }
}