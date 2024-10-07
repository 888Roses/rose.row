using HarmonyLib;
using rose.row.easy_events;
using UnityEngine;

namespace rose.row.data.events.patchers.actors
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.SpawnAt))]
    internal class OnActorSpawnPatcher
    {
        [HarmonyPrefix]
        static void prefix(Actor __instance, Vector3 position, Quaternion rotation, WeaponManager.LoadoutSet forcedLoadout)
            => Events.onActorSpawnAt.before?.Invoke(__instance, position, rotation, forcedLoadout);

        [HarmonyPostfix]
        static void postfix(Actor __instance, Vector3 position, Quaternion rotation, WeaponManager.LoadoutSet forcedLoadout)
            => Events.onActorSpawnAt.after?.Invoke(__instance, position, rotation, forcedLoadout);
    }
}
