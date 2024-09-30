using UnityEngine;

namespace rose.row.util
{
    public static class Vec2Util
    {
        public static Vector2 add(this Vector2 vector, float? x = null, float? y = null)
        {
            float _x, _y;

            if (!x.HasValue)
                _x = vector.x;
            else
                _x = vector.x + x.Value;

            if (!y.HasValue)
                _y = vector.y;
            else
                _y = vector.y + y.Value;

            return new Vector2(_x, _y);
        }

        public static Vector2 with(this Vector2 vector, float? x = null, float? y = null)
        {
            if (x.HasValue)
                vector.x = x.Value;
            if (y.HasValue)
                vector.y = y.Value;

            return vector;
        }
    }
}