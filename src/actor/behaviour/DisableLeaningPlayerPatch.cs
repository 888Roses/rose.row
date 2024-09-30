using HarmonyLib;
using rose.row.data;

namespace rose.row.actor.behaviour
{
    [HarmonyPatch(typeof(FpsActorController), "GetLeanInput")]
    internal class DisableLeaningPlayerPatch
    {
        public static readonly ConstantHolder<bool> canPlayerLean = new ConstantHolder<bool>(
            name: "actor.player.can_lean",
            description: "Whether the player is able to lean or not.",
            defaultValue: false
        );

        [HarmonyPrefix]
        static bool prefix()
        {
            return canPlayerLean.get();
        }
    }
}
