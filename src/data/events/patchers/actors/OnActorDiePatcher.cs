using HarmonyLib;
using rose.row.easy_events;

namespace rose.row
{
    [HarmonyPatch(typeof(Actor), "Die")]
    internal class OnActorDiePatcher
    {
        [HarmonyPrefix]
        private static void prefix(Actor __instance, DamageInfo info, bool isSilentKill)
        {
            Events.onActorDie.before?.Invoke(__instance, info, isSilentKill);
        }

        [HarmonyPostfix]
        private static void postfix(Actor __instance, DamageInfo info, bool isSilentKill)
        {
            Events.onActorDie.after?.Invoke(__instance, info, isSilentKill);
        }

    }
}