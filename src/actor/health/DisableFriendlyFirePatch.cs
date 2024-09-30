using HarmonyLib;

namespace rose.row.actor.health
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.Damage))]
    internal class DisableFriendlyFirePatch
    {
        [HarmonyPrefix]
        static bool prefix(Actor __instance, DamageInfo info)
        {
            if (info.sourceActor != null)
            {
                if (info.sourceActor.team == __instance.team && info.type == DamageInfo.DamageSourceType.Projectile)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
