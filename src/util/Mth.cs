using UnityEngine;

namespace rose.row.util
{
    public static class Mth
    {
        private static float getDeltaTimeIndependantSpeed(float speed)
        {
            return 1f - Mathf.Exp(-speed * Time.deltaTime);
        }

        public static Vector3 lerp(Vector3 a, Vector3 b, float speed)
        {
            return Vector3.Lerp(a, b, getDeltaTimeIndependantSpeed(speed));
        }
    }
}
