using rose.row.default_package;
using System.Collections;
using UnityEngine;

namespace rose.row.easy_package.coroutines
{
    public class CoroutineManager : Singleton<CoroutineManager>
    {
        public static bool initialized;

        public static void create()
        {
            var gameObject = new GameObject("Coroutine Manager (DO NOT DESTROY)");
            gameObject.AddComponent<CoroutineManager>();
            DontDestroyOnLoad(gameObject);

            initialized = true;
        }

        public static Coroutine startCoroutine(string methodName) => instance.StartCoroutine(methodName);
        public static Coroutine startCoroutine(string methodName, object value = null) => instance.StartCoroutine(methodName, value);
        public static Coroutine startCoroutine(IEnumerator routine) => instance.StartCoroutine(routine);

        public static void stopCoroutine(IEnumerator routine) => instance.StopCoroutine(routine);
        public static void stopCoroutine(Coroutine routine) => instance.StopCoroutine(routine);
        public static void stopCoroutine(string methodName) => instance.StopCoroutine(methodName);
        public static void stopAllCoroutines() => instance.StopAllCoroutines();
    }
}
