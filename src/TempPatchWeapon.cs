using HarmonyLib;
using UnityEngine;

namespace rose.row
{
    [HarmonyPatch(typeof(Weapon), "FireFromMuzzle")]
    internal class TempPatchWeapon
    {
        [HarmonyPrefix]
        private static void prefix(Weapon __instance, ref Vector3 direction, ref bool useMuzzleDirection, bool hasUser)
        {
            if (__instance.IsMountedWeapon())
                return;

            if (hasUser)
            {
                var user = __instance.user;
                if (user != null && !user.aiControlled)
                {
                    useMuzzleDirection = false;
                    direction = PlayerFpParent.instance.fpCamera.transform.forward;
                }
            }
        }
    }
}