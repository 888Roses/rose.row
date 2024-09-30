using UnityEngine;

namespace rose.row.util
{
    public static class TransformUtil
    {
        public static T addComponent<T>(this Transform t) where T : Behaviour
            => t.gameObject.AddComponent<T>();
    }
}