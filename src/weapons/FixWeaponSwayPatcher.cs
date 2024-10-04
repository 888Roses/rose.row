using HarmonyLib;
using rose.row.util;
using UnityEngine;

namespace rose.row.weapons
{
    [HarmonyPatch(typeof(PlayerFpParent), "Awake")]
    internal class FixWeaponSwayPatcher
    {
        const float k_MinMaxMultiplier = 0.01f;

        [HarmonyPostfix]
        static void postfix(PlayerFpParent __instance)
        {
            __instance.setPositionSpring(new Spring(150f, 10f, -Vector3.one * 0.2f * k_MinMaxMultiplier, Vector3.one * 0.2f * k_MinMaxMultiplier, 8 / 4));
            __instance.setRotationSpring(new Spring(70f, 6f, -Vector3.one * 15f / 4 * k_MinMaxMultiplier, Vector3.one * 15f / 4 * k_MinMaxMultiplier, 8 / 4));
        }
    }
}
