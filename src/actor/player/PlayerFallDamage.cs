using rose.row.easy_events;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.player
{
    public class PlayerFallDamage : PlayerBehaviour
    {
        private float _lastFallDistance;
        private float _invulnerabilityTimeStamp;

        private void Awake()
        {
            Events.onPlayerSpawn.after += onPlayerSpawn;
            Events.onActorLeaveSeat.after += onActorLeaveSeat;
        }


        private void OnDestroy()
        {
            Events.onPlayerSpawn.after -= onPlayerSpawn;
            Events.onActorLeaveSeat.after -= onActorLeaveSeat;
        }

        private void onPlayerSpawn(FpsActorController controller)
        {
            _invulnerabilityTimeStamp = Time.time;
        }

        private void onActorLeaveSeat(Actor actor, bool forcedByFallingOver)
        {
            if (!actor.aiControlled)
                _invulnerabilityTimeStamp = Time.time;
        }

        private void Update()
        {
            var actor = player.controller.actor;
            var fallDistance = actor.fallStartHeight();

            if (fallDistance != _lastFallDistance && !actor.IsSeated() && Time.time > _invulnerabilityTimeStamp + 0.5f)
            {
                var difference = _lastFallDistance - fallDistance;
                if (difference > 5f)
                {
                    // Meaning you must fall 15 meters before dying.
                    var damage = difference * (100f / 15);

                    var damageInfo = new DamageInfo(DamageInfo.DamageSourceType.FallDamage, null, null)
                    {
                        healthDamage = damage,
                        balanceDamage = damage,
                        isPiercing = true,
                        point = actor.Position(),
                        direction = -actor.controller.FacingDirection()
                    };

                    actor.Damage(damageInfo);
                }
            }

            _lastFallDistance = fallDistance;
        }
    }
}
