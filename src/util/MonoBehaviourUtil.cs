using HarmonyLib;
using UnityEngine;

namespace rose.row.util
{
    public static class MonoBehaviourUtil
    {
        public static void executePrivate(this Component component, string name, params object[] arguments)
        {
            Traverse.Create(root: component)
                    .Method(name: name, arguments: arguments)
                    .GetValue();
        }
    }
}
