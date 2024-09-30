using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace rose.row.util
{
    public static class StringUtil
    {
        public static string toLegalFileName(this string str)
        {
            var stringBuilder = new StringBuilder();
            var invalidFileNameChars = Path.GetInvalidFileNameChars().AddRangeToArray(Path.GetInvalidPathChars());

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];

                if (invalidFileNameChars.Contains(c))
                    continue;

                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static string toConventionalFileName(this string str)
        {
            var x = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.')
                {
                    x.Append('_');
                    continue;
                }

                if (str[i] == '/' || str[i] == '\\')
                {
                    x.Append('_');
                    continue;
                }

                if (i > 0 && str[i] == '_' && str[i - 1] == '_')
                    continue;

                var isSpace = char.IsWhiteSpace(str[i]) || str[i] == ' ';
                var isNewWord = i > 0 && char.IsUpper(str[i]) && !char.IsUpper(str[i - 1]);
                if (isSpace || isNewWord)
                {
                    if (i > 0 && str[i - 1] == '_')
                        continue;

                    x.Append('_');

                    if (isSpace)
                    {
                        continue;
                    }
                }

                x.Append(char.ToLowerInvariant(str[i]));
            }

            return x.ToString();
        }

        public static List<string> safeSplitWhitespace(this string text)
        {
            var splits = new List<string>();
            var builder = new StringBuilder();
            var isInSimpleQuotes = false;
            var isInDoubleQuotes = false;

            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c == '"')
                    isInDoubleQuotes = !isInDoubleQuotes;
                if (c == '\'')
                    isInSimpleQuotes = !isInSimpleQuotes;

                if (!isInSimpleQuotes && !isInDoubleQuotes && c == ' ')
                {
                    splits.Add(builder.ToString());
                    builder.Clear();
                    continue;
                }

                builder.Append(c);
            }

            splits.Add(builder.ToString());
            return splits;
        }
    }
}