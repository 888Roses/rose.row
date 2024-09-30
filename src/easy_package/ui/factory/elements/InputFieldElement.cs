using rose.row.easy_package.ui.factory;
using TMPro;
using UnityEngine;

namespace rose.row.easy_package.ui.factory.elements
{
    public class InputFieldElement : UiElement
    {
        public string font;
        public float fontSize = 16f;
        public Color textColor;
        public Color placeholderColor;
        public Vector2 padding;

        public UiElement inputFieldElement;
        public TMP_InputField inputField;
        public TextElement text;
        public TextElement placeholder;
        public UiElement viewport;

        protected override void Awake()
        { }

        public override void build()
        {
            createInputField();
            updateSize();
        }

        public bool isFocused => inputField.isFocused;

        public void updateSize()
        {
            inputFieldElement.setAnchors(Anchors.FillParent);
            viewport.setAnchors(Anchors.FillParent);
            viewport.setOffset(padding.x, padding.y, -padding.x, -padding.y);
        }

        public void setContent(string content)
        {
            inputField.text = content;
        }

        public void createInputField()
        {
            inputFieldElement = UiFactory.createGenericUiElement("Input Field", this);
            inputField = inputFieldElement.use<TMP_InputField>();

            viewport = UiFactory.createGenericUiElement("Viewport", inputFieldElement);

            text = createInputFieldText("Text", viewport, font, fontSize, textColor, false);
            placeholder = createInputFieldText("Placeholder", viewport, font, fontSize, placeholderColor, true);
            placeholder.setText("Input command...");

            inputField.textViewport = viewport.rectTransform;
            inputField.textComponent = text.text;
            inputField.placeholder = placeholder.text;
            inputField.caretColor = textColor;
            inputField.customCaretColor = true;
            inputField.selectionColor = placeholderColor;
            inputField.caretWidth = 2;
            inputField.onFocusSelectAll = false;

            // Otherwise the selection and caret just disappear and never show up :(
            // https://gamedev.stackexchange.com/questions/202942/caret-not-visible-with-tmp-inputfield-created-from-script
            inputField.enabled = false;
            inputField.enabled = true;
        }

        public static TextElement createInputFieldText(
            string name,
            UiElement parent,
            string font,
            float fontSize,
            Color color,
            bool isItalic
        )
            => createInputFieldText(name, parent.transform, font, fontSize, color, isItalic);

        public static TextElement createInputFieldText(
            string name,
            Transform parent,
            string font,
            float fontSize,
            Color color,
            bool isItalic
        )
        {
            var text = UiFactory.createUiElement<TextElement>(name, parent);
            text.setAnchors(Anchors.FillParent);
            text.setFontSize(fontSize);
            text.setFont(font);
            text.setColor(color);
            text.setItalic(isItalic);
            text.setTextAlign(VerticalAlignmentOptions.Middle);

            return text;
        }
    }
}