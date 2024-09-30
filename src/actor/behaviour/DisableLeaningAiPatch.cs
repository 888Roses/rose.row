using HarmonyLib;
using rose.row.data;

namespace rose.row.actor.behaviour
{
    [HarmonyPatch(typeof(AiActorController), "UpdateLean")]
    internal class DisableLeaningAiPatch
    {
        public static readonly ConstantHolder<bool> canAiLean = new ConstantHolder<bool>(
            name: "actor.ai.can_lean",
            description: "Whether non player actors are able to lean or not.",
            defaultValue: false
        );

        [HarmonyPrefix]
        static bool prefix()
        {
            return canAiLean.get();
        }
    }
}
