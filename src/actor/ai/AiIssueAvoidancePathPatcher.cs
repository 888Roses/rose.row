using HarmonyLib;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.ai
{
    [HarmonyPatch(typeof(AiActorController), nameof(AiActorController.IssueAvoidancePath))]
    internal class AiIssueAvoidancePathPatcher
    {
        public static void prefix(AiActorController __instance, VehicleParticleManager.AvoidancePath path)
        {
            __instance.setAvoidancePassSegment(path.thruB - path.thruA);
            Vector3 position = __instance.actor.seat.vehicle.GetPosition();

            __instance.GotoDirect(new Vector3[]
            {
                path.thruA,
                path.thruB,
                position + __instance.getAvoidancePassSegment() * 10f
            }, false);

            __instance.setAvoidancePassPoint(path.passPoint);
            __instance.setIsOnAvoidancePath(true);
        }
    }
}
