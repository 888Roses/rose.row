using HarmonyLib;

namespace rose.row.actor.ai
{
    [HarmonyPatch(typeof(AiActorController), "Awake")]
    internal class AdvancedAiActorPatcher
    {
        [HarmonyPrefix]
        static void prefix(AiActorController __instance)
        {
            var advanced = __instance.gameObject.AddComponent<AdvancedAi>();
            advanced.controller = __instance;
        }
    }
}
