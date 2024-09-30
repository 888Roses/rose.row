using UnityEngine;

namespace rose.row.util
{
    public static class Vec3Util
    {
        public static Vector3 grounded(this Vector3 v, float distance = 100f)
        {
            if (Physics.Raycast(v, Vector3.down, out var hit, maxDistance: distance))
                return hit.point;

            return v;
        }

        public static Vector3 add(this Vector3 v, float x = 0, float y = 0, float z = 0)
        {
            return v + new Vector3(x, y, z);
        }

        public static Vector3 with(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            var point = v;
            if (x.HasValue)
                point.x = x.Value;
            if (y.HasValue)
                point.y = y.Value;
            if (z.HasValue)
                point.z = z.Value;

            return point;
        }
    }
}