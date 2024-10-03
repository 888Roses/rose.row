using UnityEngine;

namespace rose.row.util
{
    public static class TransformUtil
    {
        public static T addComponent<T>(this Transform t) where T : Behaviour
            => t.gameObject.AddComponent<T>();

        public static void resetTransform(this Transform t)
        {
            t.position = Vector3.zero;
            t.rotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public static void resetLocalTransform(this Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }
    }
}