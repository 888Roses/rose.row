using HarmonyLib;
using rose.row.util;
using UnityEngine;

namespace rose.row.vehicles
{
    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.OnVehicleDisabled))]
    internal class KillActorsInVehicleOnDeathOnVehicleDisabledPatcher
    {
        [HarmonyPrefix]
        static void prefix(Vehicle __instance, DamageInfo info)
        {
            //Debug.Log("==================================");
            //Debug.Log("Vehicle was disabled:");
            //foreach (var seat in __instance.seats)
            //{
            //    var occupant = seat.IsOccupied()
            //        ? seat.occupant.getNameSafe()
            //        : "Empty";
            //    Debug.Log($"Seat '{seat.name}': {occupant}");

            //    if (seat.IsOccupied())
            //    {
            //        Debug.Log($" -> Seat has occupant ({seat.occupant.getNameSafe()}) and tries to kill them.");
            //        seat.occupant.Kill(info);
            //    }
            //}
            //Debug.Log("==================================");

            Debug.Log("=======================================================================");
            Debug.Log($"Vehicle '{__instance.name}' was disabled. Checking actors to kill:");

            foreach (var possibleActor in ActorManager.instance.actors)
            {
                if (possibleActor == null || possibleActor.dead || !possibleActor.IsSeated())
                    continue;

                if (possibleActor.IsSeatedInVehicle(__instance))
                {
                    Debug.LogWarning($"* '{possibleActor.getNameSafe()}'.");
                    possibleActor.Kill(info);
                    continue;
                }

                Debug.Log($"* '{possibleActor.getNameSafe()}'.");
            }

            Debug.Log("=======================================================================");
        }
    }

    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.Die))]
    internal class KillActorsInVehicleOnDeathDiePatcher
    {
        [HarmonyPrefix]
        static void prefix(Vehicle __instance, DamageInfo info)
        {
            Debug.Log("=============================================================");
            Debug.Log($"Vehicle '{__instance.name}' died. Checking actors to kill:");

            foreach (var possibleActor in ActorManager.instance.actors)
            {
                if (possibleActor == null || possibleActor.dead || !possibleActor.IsSeated())
                    continue;

                if (possibleActor.IsSeatedInVehicle(__instance))
                {
                    Debug.LogWarning($"* '{possibleActor.getNameSafe()}'.");
                    possibleActor.Kill(info);
                    continue;
                }

                Debug.Log($"* '{possibleActor.getNameSafe()}'.");
            }

            Debug.Log("=============================================================");
        }
    }
}
