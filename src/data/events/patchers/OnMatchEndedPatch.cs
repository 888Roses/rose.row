using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers
{
    [HarmonyPatch(typeof(VictoryUi), nameof(VictoryUi.EndGame))]
    internal class OnMatchEndedPatch
    {
        [HarmonyPrefix]
        private static void prefix(int winner, bool allowContinueBattle)
            => Events.onMatchEnded.before?.Invoke(winner, allowContinueBattle);

        [HarmonyPostfix]
        private static void postfix(int winner, bool allowContinueBattle)
            => Events.onMatchEnded.after?.Invoke(winner, allowContinueBattle);
    }
}