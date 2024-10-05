using HarmonyLib;
using rose.row.dev.dev_editor;
using rose.row.util;

namespace rose.row.ui.cursor
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.Aiming))]
    internal class FpsPlayerCharacterAimingPatcher
    {
        [HarmonyPrefix]
        static bool prefix(FpsActorController __instance, ref bool __result)
        {
            __result = DevMainInfo.forceAim
                || ((
                       (__instance.inputEnabled && !__instance.IsCursorFree())
                       || DevMainInfo.isCursorLocked
                    )
                    && __instance.aimInput().GetInput()
                    && !__instance.isKicking()
                    && !__instance.actor.IsSwimming()
                    && !__instance.actor.parachuteDeployed
                    && !GameManager.gameOver);

            return false;
        }
    }

    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.WantsToFire))]
    internal class FpsPlayerCharacterWantsToFirePatcher
    {
        [HarmonyPrefix]
        static bool prefix(FpsActorController __instance, ref bool __result)
        {
            __result =
                (
                    (__instance.inputEnabled && !__instance.IsCursorFree())
                    || DevMainInfo.isCursorLocked
                )
                && !__instance.IsSprinting()
                && __instance.sprintCannotFireAction().TrueDone()
                && !GameManager.gameOver
                && SteelInput.GetButton(SteelInput.KeyBinds.Fire)
                && !__instance.isKicking();

            return false;
        }
    }
}
