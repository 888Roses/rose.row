using rose.row.actor;
using rose.row.easy_events;
using rose.row.ui.ingame.weapon_display;
using UnityEngine;

namespace rose.row.weapons
{
    public class ResetPickupableWeaponSlotOnSpawn
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onActorSpawnAt.after += onActorSpawn;
        }

        private static void onActorSpawn(Actor actor, Vector3 vector, Quaternion quaternion, WeaponManager.LoadoutSet set)
        {
            actor.EquipNewWeaponEntry(null, PickupableWeapons.k_PickedUpWeaponSlotIndex, false);

            if (!actor.aiControlled)
                WeaponDisplayScreen.instance.updateWeaponItems();
        }
    }
}
