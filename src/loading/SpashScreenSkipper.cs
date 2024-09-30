using HarmonyLib;
using UnityEngine;

namespace rose.row.loading
{
    public static class SpashScreenSkipper
    {
        public static void skip()
        {
            Traverse.Create(Object.FindObjectOfType<GotoMenu>()).Method("GotoNextScene").GetValue();
        }
    }
}
