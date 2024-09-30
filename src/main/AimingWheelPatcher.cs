using HarmonyLib;

namespace rose.row.main
{
    /// <summary>
    /// Patches the aiming wheel so that it doesn't throw errors
    /// in the console if it's target is null.
    /// </summary>
    [HarmonyPatch(typeof(AimingWheel), "Update")]
    internal class AimingWheelPatcher
    {
        [HarmonyPrefix]
        private static bool prefix(AimingWheel __instance)
        {
            if (__instance.target == null)
                return false;

            return true;
        }
    }
}