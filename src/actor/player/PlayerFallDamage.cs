using rose.row.util;
using UnityEngine;

namespace rose.row.actor.player
{
    public class PlayerFallDamage : PlayerBehaviour
    {
        private float _lastFallDistance;

        private void OnGUI()
        {
            GUI.Label(new Rect(24, 24, 200, 200), $"FallDistance: {_lastFallDistance} - {player.controller.actor.fallStartHeight()} = {_lastFallDistance - player.controller.actor.fallStartHeight()}");
        }

        private void Update()
        {
            var actor = player.controller.actor;
            var fallDistance = actor.fallStartHeight();

            if (fallDistance != _lastFallDistance)
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
