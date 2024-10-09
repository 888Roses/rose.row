using TMPro;
using UnityEngine;

namespace rose.row.easy_package.ui.factory.elements
{
    public interface ITextElement
    {
        public void setText(string content, bool autoBuild = true);
        public void setColor(Color color);
        public void setColor(string hex);
        public void setTextAlign(HorizontalAlignmentOptions horizontalAlignment);
        public void setTextAlign(VerticalAlignmentOptions verticalAlignment);
        public void setTextAlign(VerticalAlignmentOptions vertical, HorizontalAlignmentOptions horizontal);
        public void setTextAlign(HorizontalAlignmentOptions horizontal, VerticalAlignmentOptions vertical);
        public void setFontSize(float size, bool adaptiveFontSize = false);
        public void setFont(TMP_FontAsset font);
        public void setFont(string fontName);
        public void setAllowRichText(bool allow);
        public void setItalic(bool isItalic);
        public void setFontWeight(FontWeight fontWeight);
        public void setFontWeight(int fontWeight);
        public void setLetterSpacing(float spacing);
        public void setLineSpacing(float spacing);
        public void setWordSpacing(float spacing);
        public void setParagraphSpacing(float spacing);
        public void setShadow(float x, float y, Color color, bool autoBuild = true);
        public void setShadow(float offset, Color color, bool autoBuild = true);
        public void setShadow(Vector2 offset, Color color, bool autoBuild = true);
    }
}
