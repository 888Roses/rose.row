using HarmonyLib;
using UnityEngine;

namespace rose.row.ui.ingame.scoreboard
{
    [HarmonyPatch(typeof(ScoreboardUi), "Update")]
    internal class DisableDefaultScoreboardPatch
    {
        [HarmonyPrefix]
        static bool prefix(ScoreboardUi __instance)
        {
            ((Canvas) Traverse.Create(__instance).Field("canvas").GetValue()).enabled = false;
            return false;
        }
    }
}
