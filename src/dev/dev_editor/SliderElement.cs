using rose.row.data;
using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.dev.dev_editor
{
    public class TextSlider : UiElement
    {
        public new string name;
        public SliderElement slider;
        public TextElement text;

        public Action<float> onValueChanged;

        public override void build()
        {
            base.build();

            text = UiFactory.createUiElement<TextElement>("Text", this);
            text.build();
            text.setAnchors(Anchors.FillParent);
            text.setTextAlign(HorizontalAlignmentOptions.Left, VerticalAlignmentOptions.Geometry);
            text.setColor("#d6d6d6");
            text.setFontSize(18f);
            text.setFont(Fonts.fancyFont);
            text.setHeight(16f);

            slider = UiFactory.createUiElement<SliderElement>("Slider", this);
            slider.onValueChanged += e => updateValue();
            slider.setAnchors(new LiteralAnchors(0.35f, 0, 1, 1));
            slider.onValueChanged += e => onValueChanged?.Invoke(e);
        }

        private void Start()
        {
            slider.updateHeight();
        }

        public void setName(string name)
        {
            this.name = name;
            updateValue();
        }

        public void updateValue()
        {
            text.setText($"{name}: {Mathf.RoundToInt(slider.value * 10f) / 10f}");
        }
    }

    public class SliderElement : UiElement
    {
        private Slider _slider;

        private UiElement _fill;
        private UiElement _background;
        private UiElement _thumb;
        private UiElement _thumbImage;

        public Action<float> onValueChanged;
        public float value => _slider.value;

        public override void build()
        {
            _background = UiFactory.createGenericUiElement("Background", this);
            _background.setAnchors(Anchors.FillParent);
            _background.setBackgroundColor("#0A0A0A");

            _fill = UiFactory.createGenericUiElement("Fill", this);
            _fill.setAnchors(Anchors.FillParent);
            _fill.setBackgroundColor("#3B83BD");

            _thumb = UiFactory.createGenericUiElement("Thumb", this);
            _thumb.setAnchors(Anchors.StretchMiddle);

            _thumbImage = UiFactory.createGenericUiElement("Thumb Image", _thumb);
            _thumbImage.setSize(getHeight(), getHeight());
            _thumbImage.setBackgroundColor("#F4F4F4");

            _slider = use<Slider>();
            _slider.fillRect = _fill.rectTransform;
            _slider.handleRect = _thumb.rectTransform;
            _slider.onValueChanged.AddListener((e) => onValueChanged?.Invoke(e));
        }

        public void updateHeight()
        {
            _thumbImage.setSize(getHeight(), getHeight());
        }

        public override void setHeight(float height)
        {
            base.setHeight(height);
            updateHeight();
        }
    }
}