using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace rose.row.easy_package.ui.text
{
    public class TextStyle
    {
        public static TextStyle empty => new TextStyle();

        public ColorStyleProperty color = new ColorStyleProperty();
        public SimpleFormatStyleProperty bold = new SimpleFormatStyleProperty("b", false);
        public SimpleFormatStyleProperty italic = new SimpleFormatStyleProperty("i", false);
        public SimpleFormatStyleProperty underlined = new SimpleFormatStyleProperty("u", false);
        public SimpleFormatStyleProperty strikethrough = new SimpleFormatStyleProperty("s", false);

        public TextStyle withColor(string color)
        { this.color = new ColorStyleProperty(color); return this; }

        public TextStyle withColor(Color color)
        { this.color = new ColorStyleProperty(color); return this; }

        public TextStyle withBold(bool bold)
        { this.bold = new SimpleFormatStyleProperty("b", bold); return this; }

        public TextStyle withUnderlined(bool underlined)
        { bold = new SimpleFormatStyleProperty("u", underlined); return this; }

        public TextStyle withItalic(bool italic)
        { bold = new SimpleFormatStyleProperty("i", italic); return this; }

        public TextStyle withStrikethrough(bool strikethrough)
        { bold = new SimpleFormatStyleProperty("s", strikethrough); return this; }

        public string applyOn(string text)
        {
            text = color.getString(text);
            text = bold.getString(text);
            text = italic.getString(text);
            text = underlined.getString(text);
            text = strikethrough.getString(text);

            return text;
        }
    }

    public class TextComponent
    {
        public StringBuilder text;

        private TextStyle _style = new TextStyle();
        public TextStyle style => _style;

        public List<TextComponent> children = new List<TextComponent>();

        public TextComponent append(TextComponent component)
        {
            children.Add(component);
            return this;
        }

        public TextComponent()
        {
            text = new StringBuilder();
        }

        public TextComponent(string text)
        {
            this.text = new StringBuilder(text);
        }

        public TextComponent withStyle(TextStyle style)
        {
            _style = style;
            return this;
        }

        public void setStyle(TextStyle style) => _style = style;

        public virtual string getString()
        {
            var baseText = new StringBuilder(style.applyOn(text.ToString()));
            foreach (var child in children)
                baseText.Append(child.getString());

            return baseText.ToString();
        }

        public static TextComponent formatWithHighlighting(string text, TextStyle normal, TextStyle highlight)
        {
            var finalText = new TextComponent("");

            var isHighlighting = false;
            var current = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '_')
                {
                    finalText.append(new TextComponent(current.ToString()).withStyle(isHighlighting ? highlight : normal));
                    current.Clear();

                    isHighlighting = !isHighlighting;
                    continue;
                }

                current.Append(text[i]);
            }

            finalText.append(new TextComponent(current.ToString()).withStyle(isHighlighting ? highlight : normal));
            return finalText;
        }
    }
}