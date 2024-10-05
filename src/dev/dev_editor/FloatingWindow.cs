using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using TMPro;
using UnityEngine;

namespace rose.row.dev.dev_editor
{
    public abstract class FloatingWindow : Window
    {
        public abstract float headerHeight { get; }
        public abstract float containerPadding { get; }

        protected UiElement _header;
        protected TextElement _headerText;

        private UiElement _container;
        public UiElement container => _container;

        public override void build()
        {
            base.build();

            _header = UiFactory.createGenericUiElement("Header", this);
            _header.setAnchors(Anchors.StretchTop);
            _header.setPivot(0.5f, 1);
            _header.setAnchoredPosition(0, 0);
            _header.setHeight(headerHeight);
            _header.setBackgroundColor("#282828");
            registerModule(new DragModule(_header));

            _headerText = UiFactory.createUiElement<TextElement>("Text", _header);
            _headerText.build();
            _headerText.setAnchors(Anchors.FillParent);
            _headerText.setOffset(8f, 0, 0, 0);
            _headerText.setTextAlign(VerticalAlignmentOptions.Middle);
            _headerText.setText(name);
            _headerText.setFontSize(18f);
            _headerText.setColor("#d6d6d6");
            _headerText.setFont(Fonts.fancyFont);

            _container = UiFactory.createGenericUiElement("Container", this);
            _container.setAnchors(Anchors.FillParent);
            _container.setOffset(containerPadding, containerPadding, -containerPadding, -containerPadding - 32f);

            setBackgroundColor("#1C1C1C");
        }

        protected virtual void updateHeight(RectTransform container, float gapBetweenContainerItems)
        {
            // Start with the height of the header.
            var height = headerHeight;
            // Add the height of the padding.
            height += 2 * containerPadding;
            // Then we can add the height of every element contained in the container.
            for (int i = 0; i < container.childCount; i++)
            {
                var child = container.GetChild(i) as RectTransform;
                height += child.sizeDelta.y;

                if (i < container.childCount - 1)
                {
                    height += gapBetweenContainerItems;
                }
            }

            setHeight(height);
        }

        #region utility

        public abstract UiElement itemContainer { get; }

        protected Button button(string name, string text, bool highlight = false, Action onClick = null)
        {
            return button(name, itemContainer, text, highlight, onClick);
        }

        protected Button button(string text, bool highlight = false, Action onClick = null)
        {
            return button(text, text, highlight, onClick);
        }

        protected TextElement header(string text)
        {
            var header = UiFactory.createUiElement<TextElement>("Text", itemContainer);
            header.build();
            header.setAnchors(Anchors.FillParent);
            header.setTextAlign(HorizontalAlignmentOptions.Left, VerticalAlignmentOptions.Bottom);
            header.setColor("#838383");
            header.setFontSize(24f);
            header.setFont(Fonts.fancyFont);
            header.setText(text);
            header.setHeight(40f);

            return header;
        }

        protected TextElement text(string text)
        {
            var textElement = UiFactory.createUiElement<TextElement>("Text", itemContainer);
            textElement.build();
            textElement.setAnchors(Anchors.FillParent);
            textElement.setTextAlign(HorizontalAlignmentOptions.Left, VerticalAlignmentOptions.Geometry);
            textElement.setColor("#d6d6d6");
            textElement.setFontSize(18f);
            textElement.setFont(Fonts.fancyFont);
            textElement.setText(text);
            textElement.setHeight(16f);

            return textElement;
        }

        protected TextSlider slider(string text)
        {
            var element = UiFactory.createUiElement<TextSlider>("Slider", itemContainer);
            element.setName(text);
            element.setAnchors(Anchors.StretchTop);
            element.setHeight(16f);

            return element;
        }

        #endregion
    }
}
