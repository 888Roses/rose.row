using MapEditor;
using rose.row.data;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory.elements
{
    public class TextElement : UiElement
    {
        public UiElement textContainer;
        public TMP_Text text;

        public bool isAdaptiveHeight;

        public override void build()
        {
            // Cannot built twice!
            if (text != null)
                return;

            textContainer = UiFactory.createGenericUiElement("Text", this);
            textContainer.setAnchors(Anchors.FillParent);
            text = textContainer.use<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (isAdaptiveHeight)
            {
                setHeight(text.rectTransform.sizeDelta.y);
            }
        }

        public void setAdaptiveHeight()
        {
            text.rectTransform.anchorMin = new Vector2(0, 1);
            text.rectTransform.anchorMax = new Vector2(1, 1);
            text.rectTransform.pivot = new Vector2(0.5f, 1);
            text.gameObject.GetOrCreateComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            isAdaptiveHeight = true;
        }

        public void setInteractable(bool interactable)
        {
            text.raycastTarget = interactable;
        }

        public void setText(string textContent, bool autoBuild = true)
        {
            if (text == null)
            {
                if (autoBuild)
                {
                    build();
                }
                else
                {
                    Debug.LogError($"You tried setting the text of this element before building it. This is illegal. Either enable the \"autoBuild\" parameter or build() manually before setting the text's content.");
                    return;
                }
            }

            text.text = textContent;

            if (shadowText != null)
                setShadowText(textContent);
        }

        public void setColor(Color color) => text.color = color;

        public void setColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out var color))
            {
                setColor(color);
            }
            else
            {
                setColor(Color.white);
            }
        }

        public void setTextAlign(HorizontalAlignmentOptions align)
        {
            text.horizontalAlignment = align;
            updateShadowText();
        }

        public void setTextAlign(VerticalAlignmentOptions align)
        {
            text.verticalAlignment = align;
            updateShadowText();
        }

        public void setTextAlign(VerticalAlignmentOptions ver, HorizontalAlignmentOptions hor)
        {
            setTextAlign(ver);
            setTextAlign(hor);
        }

        public void setTextAlign(HorizontalAlignmentOptions hor, VerticalAlignmentOptions ver)
        {
            setTextAlign(ver);
            setTextAlign(hor);
        }

        public void setFontSize(float fontSize)
        {
            text.fontSize = fontSize;
            updateShadowText();
        }

        public void setFont(TMP_FontAsset font)
        {
            text.font = font;
            updateShadowText();
        }

        public void setFont(string fontName)
        {
            setFont(Fonts.getFont(fontName));
            updateShadowText();
        }

        public void setAllowRichText(bool allowRichText)
        {
            text.richText = allowRichText;
            updateShadowText();
        }

        public void setItalic(bool isItalic)
        {
            if (isItalic)
            {
                text.fontStyle |= FontStyles.Italic;
                updateShadowText();
            }
        }

        public void setFontWeight(FontWeight fontWeight)
        {
            text.fontWeight = fontWeight;
            updateShadowText();
        }

        public void setFontWeight(int fontWeight)
        {
            text.fontWeight = (FontWeight) fontWeight;
            updateShadowText();
        }

        public void setLetterSpacing(float spacing)
        {
            text.characterSpacing = spacing;
            updateShadowText();
        }

        public void setLineSpacing(float spacing)
        {
            text.lineSpacing = spacing;
            updateShadowText();
        }

        public void setWordSpacing(float spacing)
        {
            text.wordSpacing = spacing;
            updateShadowText();
        }

        public void setParagraphSpacing(float spacing)
        {
            text.paragraphSpacing = spacing;
            updateShadowText();
        }

        public UiElement shadowTextContainer;
        public TMP_Text shadowText;

        private void setShadowText(string text)
        {
            if (text == null)
            {
                shadowText.text = "";
                return;
            }

            var builder = new StringBuilder();
            var isInTag = false;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '<')
                {
                    isInTag = true;
                    continue;
                }

                if (text[i] == '>')
                {
                    isInTag = false;
                    continue;
                }

                if (!isInTag)
                    builder.Append(text[i]);
            }

            shadowText.text = builder.ToString();
        }

        public void setShadow(float x, float y, Color color, bool autoBuild = true)
            => setShadow(new Vector2(x, y), color, autoBuild);

        public void setShadow(float offset, Color color, bool autoBuild = true)
            => setShadow(offset, -offset, color, autoBuild);

        public void setShadow(Vector2 offset, Color color, bool autoBuild = true)
        {
            if (text == null)
            {
                if (autoBuild)
                {
                    build();
                }
                else
                {
                    Debug.LogError("You tried to add a shadow before building the element. This is illegal. Either enable the \"autoBuild\" parameter, or build the element before adding the shadow.");
                    return;
                }
            }

            shadowTextContainer = UiFactory.createGenericUiElement("Text Shadow", this);
            shadowTextContainer.setAnchors(Anchors.FillParent);
            shadowText = shadowTextContainer.use<TextMeshProUGUI>();
            shadowTextContainer.moveToBack();
            shadowTextContainer.setAnchoredPosition(offset);

            shadowText.color = color;
            updateShadowText();
        }

        private void updateShadowText()
        {
            if (shadowText == null)
                return;

            shadowText.font = text.font;
            shadowText.fontSize = text.fontSize;
            shadowText.horizontalAlignment = text.horizontalAlignment;
            shadowText.verticalAlignment = text.verticalAlignment;
            shadowText.fontStyle = text.fontStyle;
            shadowText.richText = false;
            shadowText.characterSpacing = text.characterSpacing;
            shadowText.lineSpacing = text.lineSpacing;
            shadowText.paragraphSpacing = text.paragraphSpacing;
            shadowText.wordSpacing = text.wordSpacing;
            setShadowText(text.text);
        }

        //public void setShadowEnabled(bool enabled)
        //{
        //    if (enabled)
        //        text.fontSharedMaterial.EnableKeyword("UNDERLAY_ON");
        //    else
        //        text.fontSharedMaterial.DisableKeyword("UNDERLAY_ON");
        //}

        //// Softness: 0..1
        //public void setShadowSoftness(float softness) => text.fontSharedMaterial.SetFloat("_UnderlaySoftness", softness);
        //// Softness: -0..1
        //public void setShadowDilate(float dilate) => text.fontSharedMaterial.SetFloat("_UnderlayDilate", dilate);
        //public void setShadowOffset(float x, float y)
        //{
        //    setShadowOffsetX(x);
        //    setShadowOffsetY(y);
        //}
        //// Softness: -0..1
        //public void setShadowOffsetX(float x) => text.fontSharedMaterial.SetFloat("_UnderlayOffsetX", x);
        //// Softness: -0..1
        //public void setShadowOffsetY(float y) => text.fontSharedMaterial.SetFloat("_UnderlayOffsetY", y);
        //public void setShadowColor(Color color) => text.fontSharedMaterial.SetColor("_UnderlayColor", color);
    }
}