using HarmonyLib;

namespace rose.row.data.mod
{
    [HarmonyPatch(typeof(LoadContentDialog), "SetProgress")]
    internal class ModHelperSetProgressPatch
    {
        [HarmonyPostfix]
        static void postfix(float progress) => ModHelper.progress = progress;
    }
}
