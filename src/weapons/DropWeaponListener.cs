using HarmonyLib;
using rose.row.actor;
using rose.row.easy_events;
using rose.row.util;
using UnityEngine;

namespace rose.row.weapons
{
    public class DropWeaponListener
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onActorDie.after += onActorDie;
        }

        static void onActorDie(Actor actor, DamageInfo info, bool silentKill)
        {
            if (actor.weapons.isEmpty())
            {
                Debug.LogWarning($"Could not find any weapon to be dropped upon actor '{actor.getNameSafe()}'s death.");
                return;
            }

            // The weapon that we wish to be dropped when the actor died.
            var weapon = actor.weapons[0];

            if (weapon == null)
            {
                Debug.LogError($"Tried to drop weapon on death of actor '{actor.getNameSafe()}' but the weapon is null :(");
                return;
            }

            PickupableWeapons.dropWeaponOnGround(weapon);
        }
    }
}