using rose.row.easy_events;
using rose.row.ui.ingame.weapon_display;
using System.Collections;
using UnityEngine;

namespace rose.row.actor.player
{
    public class PlayerUiUpdater : PlayerBehaviour
    {
        public static void subscribeToInitializationEvents()
        {
            Events.onPlayerSpawn.after += onPlayerSpawnStatic;
            Events.onActorSwitchActiveWeapon.after += onActorSwitchActiveWeaponStatic;
        }

        private static void onActorSwitchActiveWeaponStatic(Actor actor, int slot)
        {
            if (Player.instance != null && Player.instance.uiUpdater != null)
                Player.instance.uiUpdater.onActorSwitchActiveWeapon(actor, slot);
        }

        private static void onPlayerSpawnStatic(FpsActorController controller)
        {
            if (Player.instance != null && Player.instance.uiUpdater != null)
                Player.instance.uiUpdater.onPlayerSpawn(controller);
        }

        private void onActorSwitchActiveWeapon(Actor actor, int slot)
        {
            if (actor == null)
                return;

            StartCoroutine(onActorSwitchActiveWeaponCoroutine(actor, slot));
        }

        private IEnumerator onActorSwitchActiveWeaponCoroutine(Actor actor, int slot)
        {
            yield return new WaitForEndOfFrame();
            if (!actor.aiControlled)
                WeaponDisplayScreen.instance.updateWeaponItems();
        }

        private void onPlayerSpawn(FpsActorController controller)
        {
            WeaponDisplayScreen.instance.updateWeaponItems();
        }
    }
}
