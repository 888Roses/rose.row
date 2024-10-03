using HarmonyLib;
using rose.row.actor;
using rose.row.util;
using UnityEngine;

namespace rose.row.weapons
{
    [HarmonyPatch(typeof(Actor), "Die")]
    internal class DropWeaponPatcher
    {
        [HarmonyPostfix]
        static void postfix(Actor __instance)
        {
            if (__instance.weapons.isEmpty())
            {
                Debug.LogWarning($"Could not find any weapon to be dropped upon actor '{__instance.getNameSafe()}'s death.");
                return;
            }

            // The weapon that we wish to be dropped when the actor died.
            var wishDroppedWeapon = __instance.weapons[0];

            if (wishDroppedWeapon == null)
            {
                Debug.LogError($"Tried to drop weapon on death of actor '{__instance.getNameSafe()}' but the weapon is null :(");
                return;
            }

            PickupableWeapons.dropWeaponOnGround(wishDroppedWeapon);
        }
    }
}