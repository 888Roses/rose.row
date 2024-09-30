using System;
using System.Text;
using UnityEngine;

namespace rose.row.easy_package.ui.text
{
    public class Style
    {
        private Color _color;
        private bool _hasColor;
        public bool hasColor => _hasColor;

        public Color color
        {
            get => _color;
            set { _color = value; _hasColor = true; }
        }

        public bool bold = false;
        public bool italic = false;
        public bool underlined = false;
        public bool strikethrough = false;

        public string applyStyle(string text)
        {
            var builder = new StringBuilder();

            if (_hasColor)
                builder.Append($"<#{ColorUtility.ToHtmlStringRGBA(_color)}>");
            if (bold)
                builder.Append("<b>");
            if (italic)
                builder.Append("<i>");
            if (underlined)
                builder.Append("<u>");
            if (strikethrough)
                builder.Append("<s>");

            builder.Append(text);

            if (strikethrough)
                builder.Append("</s>");
            if (underlined)
                builder.Append("</u>");
            if (italic)
                builder.Append("</i>");
            if (bold)
                builder.Append("</b>");
            if (_hasColor)
                builder.Append($"</color>");

            return builder.ToString();
        }
    }

    public class RichTextBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        //public void append(string text, Style style)
        //{
        //    _stringBuilder.Append(style.applyStyle(text));
        //}

        //public void append(string text)
        //{
        //    _stringBuilder.Append(text);
        //}

        public RichTextBuilder append(bool value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(sbyte value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(byte value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(char value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(short value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(int value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(long value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(float value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(double value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(decimal value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(ushort value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(uint value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(ulong value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder append(object value, Style style)
        { _stringBuilder.Append(style.applyStyle(value.ToString())); return this; }

        public RichTextBuilder appendLine(string value, Style style)
        { _stringBuilder.AppendLine(style.applyStyle(value.ToString())); return this; }

        public unsafe RichTextBuilder append(string value, int startIndex, int count)
        { _stringBuilder.Append(value, startIndex, count); return this; }

        public RichTextBuilder appendLine()
        { _stringBuilder.AppendLine(); return this; }

        public RichTextBuilder appendLine(string value)
        { _stringBuilder.AppendLine(value); return this; }

        public RichTextBuilder copyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
        { _stringBuilder.CopyTo(sourceIndex, destination, destinationIndex, count); return this; }

        public unsafe RichTextBuilder insert(int index, string value, int count)
        { _stringBuilder.Insert(index, value, count); return this; }

        public RichTextBuilder remove(int startIndex, int length)
        { _stringBuilder.Remove(startIndex, length); return this; }

        public RichTextBuilder append(bool value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(sbyte value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(byte value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(char value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(short value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(int value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(long value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(float value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(double value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(decimal value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(ushort value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(uint value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(ulong value)
        { _stringBuilder.Append(value); return this; }

        public RichTextBuilder append(object value)
        { _stringBuilder.Append(value); return this; }

        public unsafe RichTextBuilder append(char[] value)
        { _stringBuilder.Append(value); return this; }

        public unsafe RichTextBuilder insert(int index, string value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, bool value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, sbyte value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, byte value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, short value)
        { _stringBuilder.Insert(index, value); return this; }

        public unsafe RichTextBuilder insert(int index, char value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, char[] value)
        { _stringBuilder.Insert(index, value); return this; }

        public unsafe RichTextBuilder insert(int index, char[] value, int startIndex, int charCount)
        { _stringBuilder.Insert(index, value, startIndex, charCount); return this; }

        public RichTextBuilder insert(int index, int value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, long value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, float value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, double value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, decimal value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, ushort value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, uint value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, ulong value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder insert(int index, object value)
        { _stringBuilder.Insert(index, value); return this; }

        public RichTextBuilder appendFormat(string format, object arg0)
        { _stringBuilder.AppendFormat(format, arg0); return this; }

        public RichTextBuilder appendFormat(string format, object arg0, object arg1)
        { _stringBuilder.AppendFormat(format, arg0, arg1); return this; }

        public RichTextBuilder appendFormat(string format, object arg0, object arg1, object arg2)
        { _stringBuilder.AppendFormat(format, arg0, arg1, arg2); return this; }

        public RichTextBuilder appendFormat(string format, params object[] args)
        { _stringBuilder.AppendFormat(format, args); return this; }

        public RichTextBuilder appendFormat(IFormatProvider provider, string format, object arg0)
        { _stringBuilder.AppendFormat(provider, format, arg0); return this; }

        public RichTextBuilder appendFormat(IFormatProvider provider, string format, object arg0, object arg1)
        { _stringBuilder.AppendFormat(provider, format, arg0, arg1); return this; }

        public RichTextBuilder appendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2)
        { _stringBuilder.AppendFormat(provider, format, arg0, arg1, arg2); return this; }

        public RichTextBuilder appendFormat(IFormatProvider provider, string format, params object[] args)
        { _stringBuilder.AppendFormat(provider, format, args); return this; }

        public RichTextBuilder replace(string oldValue, string newValue)
        { _stringBuilder.Replace(oldValue, newValue); return this; }

        public RichTextBuilder replace(string oldValue, string newValue, int startIndex, int count)
        { _stringBuilder.Replace(oldValue, newValue, startIndex, count); return this; }

        public RichTextBuilder replace(char oldChar, char newChar)
        { _stringBuilder.Replace(oldChar, newChar); return this; }

        public RichTextBuilder replace(char oldChar, char newChar, int startIndex, int count)
        { _stringBuilder.Replace(oldChar, newChar, startIndex, count); return this; }

        public unsafe RichTextBuilder append(char* value, int valueCount)
        { _stringBuilder.Append(value, valueCount); return this; }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }
}