using rose.row.util;
using TMPro;
using UnityEngine;

namespace rose.row.easy_package.ui.factory.elements
{
    public class InputFieldElement : UiElement, ITextElement
    {
        //public string font;
        //public float fontSize = 16f;
        //public Color textColor;
        //public Color placeholderColor;
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

            text = createInputFieldText("Text", viewport/*, font, fontSize, textColor, false*/);
            placeholder = createInputFieldText("Placeholder", viewport/*, font, fontSize, placeholderColor, true*/);
            placeholder.setText("Input command...");

            inputField.textViewport = viewport.rectTransform;
            inputField.textComponent = text.text;
            inputField.placeholder = placeholder.text;

            // Otherwise the selection and caret just disappear and never show up :(
            // https://gamedev.stackexchange.com/questions/202942/caret-not-visible-with-tmp-inputfield-created-from-script
            inputField.enabled = false;
            inputField.enabled = true;
        }

        public static TextElement createInputFieldText(
            string name,
            UiElement parent
        //string font,
        //float fontSize,
        //Color color,
        //bool isItalic
        ) => createInputFieldText(name, parent.transform/*, font, fontSize, color, isItalic*/);

        public static TextElement createInputFieldText(
            string name,
            Transform parent
        //string font,
        //float fontSize,
        //Color color,
        //bool isItalic
        )
        {
            var text = UiFactory.createUiElement<TextElement>(name, parent);
            text.setAnchors(Anchors.FillParent);
            //text.setFontSize(fontSize);
            //text.setFont(font);
            //text.setColor(color);
            //text.setItalic(isItalic);
            text.setTextAlign(VerticalAlignmentOptions.Middle);

            return text;
        }

        public void setCaretColor(Color color)
        {
            inputField.caretColor = color;
            inputField.customCaretColor = true;
        }
        public void setCaretColor(string hex) => setCaretColor(hex.toColor());
        public void setCaretWidth(int width) => inputField.caretWidth = width;
        public void setOnFocusSelectAll(bool onFocusSelectAll) => inputField.onFocusSelectAll = onFocusSelectAll;
        public void setSelectionColor(Color color) => inputField.selectionColor = color;
        public void setSelectionColor(string hex) => setSelectionColor(hex.toColor());

        public void setText(string content, bool autoBuild = true) => text.setText(content, autoBuild);
        public void setColor(Color color) => text.setColor(color);
        public void setColor(string hex) => text.setColor(hex);
        public void setTextAlign(HorizontalAlignmentOptions horizontalAlignment) => text.setTextAlign(horizontalAlignment);
        public void setTextAlign(VerticalAlignmentOptions verticalAlignment) => text.setTextAlign(verticalAlignment);
        public void setTextAlign(VerticalAlignmentOptions vertical, HorizontalAlignmentOptions horizontal) => text.setTextAlign(vertical, horizontal);
        public void setTextAlign(HorizontalAlignmentOptions horizontal, VerticalAlignmentOptions vertical) => text.setTextAlign(horizontal, vertical);
        public void setFontSize(float size, bool isAdaptive = false) => text.setFontSize(size, isAdaptive);
        public void setFont(TMP_FontAsset font) => text.setFont(font);
        public void setFont(string fontName) => text.setFont(fontName);
        public void setAllowRichText(bool allow) => text.setAllowRichText(allow);
        public void setItalic(bool isItalic) => text.setItalic(isItalic);
        public void setFontWeight(FontWeight fontWeight) => text.setFontWeight(fontWeight);
        public void setFontWeight(int fontWeight) => text.setFontWeight(fontWeight);
        public void setLetterSpacing(float spacing) => text.setLetterSpacing(spacing);
        public void setLineSpacing(float spacing) => text.setLineSpacing(spacing);
        public void setWordSpacing(float spacing) => text.setWordSpacing(spacing);
        public void setParagraphSpacing(float spacing) => text.setParagraphSpacing(spacing);
        public void setShadow(float x, float y, Color color, bool autoBuild = true) => text.setShadow(x, y, color, autoBuild);
        public void setShadow(float offset, Color color, bool autoBuild = true) => text.setShadow(offset, color, autoBuild);
        public void setShadow(Vector2 offset, Color color, bool autoBuild = true) => text.setShadow(offset, color, autoBuild);

        public void setPlaceholderText(string content, bool autoBuild = true) => placeholder.setText(content, autoBuild);
        public void setPlaceholderColor(Color color) => placeholder.setColor(color);
        public void setPlaceholderColor(string hex) => placeholder.setColor(hex);
        public void setPlaceholderTextAlign(HorizontalAlignmentOptions horizontalAlignment) => placeholder.setTextAlign(horizontalAlignment);
        public void setPlaceholderTextAlign(VerticalAlignmentOptions verticalAlignment) => placeholder.setTextAlign(verticalAlignment);
        public void setPlaceholderTextAlign(VerticalAlignmentOptions vertical, HorizontalAlignmentOptions horizontal) => placeholder.setTextAlign(vertical, horizontal);
        public void setPlaceholderTextAlign(HorizontalAlignmentOptions horizontal, VerticalAlignmentOptions vertical) => placeholder.setTextAlign(horizontal, vertical);
        public void setPlaceholderFontSize(float size, bool adaptiveSize = false) => placeholder.setFontSize(size, adaptiveSize);
        public void setPlaceholderFont(TMP_FontAsset font) => placeholder.setFont(font);
        public void setPlaceholderFont(string fontName) => placeholder.setFont(fontName);
        public void setPlaceholderAllowRichText(bool allow) => placeholder.setAllowRichText(allow);
        public void setPlaceholderItalic(bool isItalic) => placeholder.setItalic(isItalic);
        public void setPlaceholderFontWeight(FontWeight fontWeight) => placeholder.setFontWeight(fontWeight);
        public void setPlaceholderFontWeight(int fontWeight) => placeholder.setFontWeight(fontWeight);
        public void setPlaceholderLetterSpacing(float spacing) => placeholder.setLetterSpacing(spacing);
        public void setPlaceholderLineSpacing(float spacing) => placeholder.setLineSpacing(spacing);
        public void setPlaceholderWordSpacing(float spacing) => placeholder.setWordSpacing(spacing);
        public void setPlaceholderParagraphSpacing(float spacing) => placeholder.setParagraphSpacing(spacing);
        public void setPlaceholderShadow(float x, float y, Color color, bool autoBuild = true) => placeholder.setShadow(x, y, color, autoBuild);
        public void setPlaceholderShadow(float offset, Color color, bool autoBuild = true) => placeholder.setShadow(offset, color, autoBuild);
        public void setPlaceholderShadow(Vector2 offset, Color color, bool autoBuild = true) => placeholder.setShadow(offset, color, autoBuild);
    }
}