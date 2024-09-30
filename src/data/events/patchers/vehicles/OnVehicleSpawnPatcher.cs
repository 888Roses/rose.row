using HarmonyLib;
using rose.row.easy_events;
using System;
using UnityEngine;

namespace rose.row
{
    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.Die))]
    internal class AdvancedVehiclePatcher_OnDestroyed
    {
        [HarmonyPrefix]
        private static void prefix(Vehicle __instance, DamageInfo info)
        {
            Events.onVehicleDie.before?.Invoke(__instance, info);
        }

        [HarmonyPostfix]
        private static void postfix(Vehicle __instance, DamageInfo info)
        {
            Events.onVehicleDie.after?.Invoke(__instance, info);
        }
    }

    [HarmonyPatch(typeof(VehicleSpawner), nameof(VehicleSpawner.SpawnVehicleAt))]
    internal class AdvancedVehiclePatcher_SpawnVehicleAt
    {
        [HarmonyPrefix]
        private static void prefix(Vector3 position,
                           Quaternion rotation,
                           int team,
                           VehicleSpawner.VehicleSpawnType type)
        {
            Events.onVehicleSpawn.before?.Invoke(null, type, position, rotation, team);
        }

        [HarmonyPostfix]
        private static void postfix(VehicleSpawner __instance,
                            Vector3 position,
                            Quaternion rotation,
                            int team,
                            VehicleSpawner.VehicleSpawnType type)
        {
            Events.onVehicleSpawn.after?.Invoke(__instance.lastSpawnedVehicle, type, position, rotation, team);
        }
    }

    [HarmonyPatch(typeof(VehicleSpawner), "SpawnVehicle", new Type[] { typeof(int) })]
    internal class AdvancedVehiclePatcher_SpawnVehicle
    {
        [HarmonyPrefix]
        private static void prefix(VehicleSpawner __instance, int team)
        {
            Events.onVehicleSpawn.before?.Invoke(null,
                                                       __instance.typeToSpawn,
                                                       __instance.transform.position,
                                                       __instance.transform.rotation,
                                                       team);
        }

        [HarmonyPostfix]
        private static void postfix(VehicleSpawner __instance, int team)
        {
            Events.onVehicleSpawn.after?.Invoke(__instance.lastSpawnedVehicle,
                                                      __instance.typeToSpawn,
                                                      __instance.transform.position,
                                                      __instance.transform.rotation,
                                                      team);
        }
    }
}