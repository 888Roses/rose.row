using HarmonyLib;
using UnityEngine;

namespace rose.row.ui.ingame.scoreboard
{
    [HarmonyPatch(typeof(FpsActorController), nameof(FpsActorController.ReceivedDamage))]
    internal class RemoveDamageScreenshakePatcher
    {
        [HarmonyPrefix]
        static bool prefix(FpsActorController __instance, bool friendlyFire, float damage, float balanceDamage, Vector3 point, Vector3 direction, Vector3 force)
        {
            Vector3 vector = __instance.GetActiveCamera().transform.worldToLocalMatrix.MultiplyVector(-direction);
            float num = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
            DamageUI.instance.ShowDamageIndicator(num, damage < 2f && balanceDamage > damage);

            return false;
        }
    }
}
