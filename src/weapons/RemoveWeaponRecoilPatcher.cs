using HarmonyLib;
using UnityEngine;

namespace rose.row.weapons
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.ApplyRecoil))]
    internal class RemoveWeaponRecoilPatcher
    {
        [HarmonyPrefix]
        static bool prefix(FpsActorController __instance, Vector3 impulse)
        {
            //__instance.fpParent.ApplyRecoil(impulse, true);
            //Weapon activeWeapon = __instance.actor.activeWeapon;
            //__instance.fpParent.ApplyWeaponSnap(activeWeapon.configuration.snapMagnitude, activeWeapon.configuration.snapDuration, activeWeapon.configuration.snapFrequency);
            return false;
        }
    }
}
