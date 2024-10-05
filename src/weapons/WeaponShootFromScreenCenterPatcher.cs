using HarmonyLib;
using UnityEngine;

namespace rose.row.weapons
{
    [HarmonyPatch(typeof(Weapon), "SpawnProjectile")]
    internal class WeaponShootFromScreenCenterPatcher
    {
        [HarmonyPrefix]
        static void prefix(Weapon __instance, ref Vector3 muzzlePosition, bool hasUser)
        {
            if (hasUser && __instance.UserIsPlayer())
            {
                muzzlePosition = PlayerFpParent.instance.fpCamera.transform.position;
            }
        }
    }
}
