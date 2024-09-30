using HarmonyLib;
using rose.row.data;
using UnityEngine;

namespace rose.row.actor.behaviour
{
    [HarmonyPatch(typeof(Actor), "Die")]
    internal class RagdollForceOnRam
    {
        [HarmonyPostfix]
        static void postfix(Actor __instance, DamageInfo info)
        {
            if (info.type == DamageInfo.DamageSourceType.VehicleRam)
            {
                var source = info.sourceActor;
                if (source != null)
                {
                    var sourceMiddlePos = source.transform.position + Vector3.up * 0.5f;
                    var actorMiddlePos = __instance.transform.position + Vector3.up * 0.5f;
                    var dir = (actorMiddlePos - sourceMiddlePos).normalized;
                    dir += Vector3.up * 0.5f;
                    dir.Normalize();

                    var vehicleVelocity = Vector3.zero;
                    if (source.IsSeated())
                    {
                        vehicleVelocity = source.seat.vehicle.Velocity();
                    }

                    __instance.FallOver();

                    Rigidbody[] rigidbodies = __instance.ragdoll.rigidbodies;
                    var force = (float) Constants.defaultValues[Constants.k_VehicleRamForce];
                    foreach (var rigidbody in rigidbodies)
                    {
                        rigidbody.AddForceAtPosition(
                            force: dir * force + vehicleVelocity,
                            position: rigidbody.position,
                            mode: ForceMode.VelocityChange
                        );
                    }
                }
            }
        }
    }
}
