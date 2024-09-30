using HarmonyLib;
using rose.row.easy_events;
using System;

namespace rose.row.data.events.patchers.vehicles
{
    [HarmonyPatch(typeof(VehicleSpawner), "SpawnVehicle", new Type[] { typeof(int) })]
    internal class OnVehicleSpawnAtPatcher
    {
        [HarmonyPrefix]
        private static void prefix(VehicleSpawner __instance, int team)
            => Events.onVehicleSpawn.before?.Invoke(null,
                                                    __instance.typeToSpawn,
                                                    __instance.transform.position,
                                                    __instance.transform.rotation,
                                                    team);

        [HarmonyPostfix]
        private static void postfix(VehicleSpawner __instance, int team)
            => Events.onVehicleSpawn.after?.Invoke(__instance.lastSpawnedVehicle,
                                                   __instance.typeToSpawn,
                                                   __instance.transform.position,
                                                   __instance.transform.rotation,
                                                   team);
    }
}