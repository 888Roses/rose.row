using UnityEngine;

namespace rose.row.util
{
    public static class ColorUtil
    {
        public static Color with(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            var _r = r.HasValue ? r.Value : color.r;
            var _g = g.HasValue ? g.Value : color.g;
            var _b = b.HasValue ? b.Value : color.b;
            var _a = a.HasValue ? a.Value : color.a;
            return new Color(_r, _g, _b, _a);
        }

        public static Color32 with(this Color32 color,
                                   byte? r = null,
                                   byte? g = null,
                                   byte? b = null,
                                   byte? a = null)
        {
            var _r = r.HasValue ? r.Value : color.r;
            var _g = g.HasValue ? g.Value : color.g;
            var _b = b.HasValue ? b.Value : color.b;
            var _a = a.HasValue ? a.Value : color.a;
            return new Color32(_r, _g, _b, _a);
        }

        public static Color toColor(this string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out var color))
                return color;

            return Color.white;
        }
    }
}