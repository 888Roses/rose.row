using HarmonyLib;

namespace rose.row.ui.cursor
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.IsCursorFree))]
    internal class MouseCursorPatcher
    {
        [HarmonyPrefix]
        private static bool prefix(FpsActorController __instance, ref bool __result)
        {
            var output = false;

            foreach (var handler in MouseCursor.cursorHandlers)
            {
                if (handler(__instance))
                {
                    output = true;
                    break;
                }
            }

            __result = output;
            return false;
        }
    }
}