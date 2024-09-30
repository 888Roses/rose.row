using rose.row.data;
using rose.row.easy_package.ui.text;
using UnityEngine;

namespace rose.row.killfeed.info
{
    public static class KillInfoUtil
    {
        public static TextComponent withColor(string text, Color color)
        {
            return new TextComponent(text).withStyle(TextStyle.empty.withColor(color));
        }

        public static TextComponent yellow(string text) => withColor(text, Colors.yellow);

        public static TextComponent red(string text) => withColor(text, Colors.red);

        public static TextComponent text(string text) => withColor(text, Colors.text);
    }
}