using HarmonyLib;
using rose.row.easy_events;

namespace rose.row
{
    [HarmonyPatch(typeof(GameManager), "StartGame")]
    internal class GameManagerEventsPatcher
    {
        [HarmonyPrefix]
        private static void prefix()
            => Events.onGameManagerStartLevel.before?.Invoke();

        [HarmonyPostfix]
        private static void postfix()
            => Events.onGameManagerStartLevel.after?.Invoke();
    }
}