using UnityEngine;

namespace rose.row.easy_package.ui.text
{
    public class ColorStyleProperty : AbstractStyleProperty
    {
        private bool _hasColor;
        private Color _color;

        public bool hasColor => _hasColor;
        public Color color => _color;

        public void setColor(Color color)
        {
            _color = color;
            _hasColor = true;
        }

        public void setColor(string color)
        {
            if (ColorUtility.TryParseHtmlString(color, out var parsedColor))
            {
                _color = parsedColor;
                _hasColor = true;
            }
            else
            {
                _color = Color.white;
                _hasColor = false;
            }
        }

        public static implicit operator Color(ColorStyleProperty prop) => prop.color;

        public ColorStyleProperty(Color color)
        {
            _color = color;
            _hasColor = true;
        }

        public ColorStyleProperty(string color)
        {
            if (ColorUtility.TryParseHtmlString(color, out var parsedColor))
            {
                _color = parsedColor;
                _hasColor = true;
            }
            else
            {
                _color = Color.white;
                _hasColor = false;
            }
        }

        public ColorStyleProperty()
        {
            _hasColor = false;
        }

        public override string getString(string text)
        {
            if (_hasColor)
                return $"<#{ColorUtility.ToHtmlStringRGBA(_color)}>{text}</color>";
            else
                return text;
        }
    }
}