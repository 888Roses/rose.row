using HarmonyLib;

namespace rose.row.actor.behaviour
{
    [HarmonyPatch(typeof(Actor), nameof(Actor.Hurt))]
    internal class DisableActorAnimationWhenHurtPatcher
    {
        [HarmonyPrefix]
        static bool prefix(Actor __instance, float x)
        {
            if (__instance.fallenOver || __instance.dead)
            {
                return false;
            }

            //__instance.animator.SetFloat(Actor.ANIM_PAR_HURT_X, x);
            //__instance.animator.SetTrigger(Actor.ANIM_PAR_HURT);
            //__instance.hurtAction().Start();

            return false;
        }
    }
}
