using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.actors
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.SwitchWeapon))]
    internal class OnActorSwapsWeaponPatcher
    {
        [HarmonyPrefix]
        static void prefix(Actor __instance, int slot) => Events.onActorSwitchActiveWeapon.before?.Invoke(__instance, slot);

        [HarmonyPostfix]
        static void postfix(Actor __instance, int slot) => Events.onActorSwitchActiveWeapon.after?.Invoke(__instance, slot);
    }
}
