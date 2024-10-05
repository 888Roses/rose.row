using rose.row.easy_events;
using rose.row.ui.ingame.weapon_display;
using System.Collections;
using UnityEngine;

namespace rose.row.actor.player
{
    public class PlayerUiUpdater : PlayerBehaviour
    {
        private void Awake()
        {
            Events.onPlayerSpawn.after += onPlayerSpawn;
            Events.onActorSwitchActiveWeapon.after += onActorSwitchActiveWeapon;
        }

        private void OnDestroy()
        {
            Events.onPlayerSpawn.after -= onPlayerSpawn;
            Events.onActorSwitchActiveWeapon.after -= onActorSwitchActiveWeapon;
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
