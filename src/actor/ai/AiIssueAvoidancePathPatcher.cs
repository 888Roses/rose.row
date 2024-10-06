using HarmonyLib;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.ai
{
    [HarmonyPatch(typeof(Squad), nameof(Squad.ReissueLastMoveSegment))]
    internal class ReissueLastMoveSegmentPatcher
    {
        [HarmonyPrefix]
        static bool prefix(Squad __instance)
        {
            if (__instance.activePathSegment() != null)
            {
                Debug.DrawLine(__instance.leader().actor.Position(), __instance.activePathSegment().destination, Color.magenta, 10f);
                __instance.issueMovePathSegment(__instance.activePathSegment());
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(Vehicle), nameof(Vehicle.IssueAvoidancePath))]
    internal class VehicleIssueAvoidancePathPatcher
    {
        [HarmonyPrefix]
        public static bool prefix(Vehicle __instance, VehicleParticleManager.AvoidancePath path)
        {
            if (__instance.Driver() == null)
            {
                return false;
            }

            ((AiActorController) __instance.Driver().controller).IssueAvoidancePath(path);
            return false;
        }
    }

    [HarmonyPatch(typeof(AiActorController), nameof(AiActorController.IssueAvoidancePath))]
    internal class AiIssueAvoidancePathPatcher
    {
        [HarmonyPrefix]
        public static bool prefix(AiActorController __instance, VehicleParticleManager.AvoidancePath path)
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

            return false;
        }
    }

    [HarmonyPatch(typeof(AiActorController), "OnAvoidancePathCompleted")]
    internal class OnAvoidancePathCompletedPatcher
    {
        [HarmonyPrefix]
        public static bool prefix(AiActorController __instance)
        {
            if (__instance.isSquadLeader)
            {
                __instance.squad.ReissueLastMoveSegment();
                return false;
            }

            Traverse.Create(__instance).Method("RecalculatePath").GetValue();
            return false;
        }
    }
}
