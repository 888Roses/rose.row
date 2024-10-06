using HarmonyLib;
using UnityEngine;

namespace rose.row.ui.ingame.ingame_displayables.displayables
{
    [HarmonyPatch(typeof(Vehicle), "Start")]
    internal class VehicleDisplayablePatch
    {
        [HarmonyPostfix]
        static void postfix(Vehicle __instance)
        {
            Debug.Log($"Registered vehicle {__instance} as a displayable holding vehicle.");
            __instance.gameObject.AddComponent<VehicleDisplayable>().setVehicle(__instance);
        }
    }
}
