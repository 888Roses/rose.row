using rose.row.data;
using rose.row.easy_events;
using rose.row.easy_package.audio;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.player
{
    public class PlayerWhistle : PlayerBehaviour
    {
        public static readonly ConstantHolder<float> whistleCooldown = new ConstantHolder<float>(
            name: "actor.player.whistle_cooldown",
            description: "Time before the player can whistle again.",
            defaultValue: 6f
        );

        private float _whistleCooldown;

        private void Update()
        {
            // You shouldn't be able to whistle in a vehicle.
            if (player.controller.actor.IsSeated())
                return;

            if (Input.GetKeyDown(KeyCode.V) && Time.time > _whistleCooldown)
            {
                Events.onWhistle.before?.Invoke(player.controller);
                Events.onPlayerWhistle.before?.Invoke(player);

                _whistleCooldown = Time.time + whistleCooldown.get();
                Audio.play(AudioRegistry.whistles.random().get());

                Events.onWhistle.after?.Invoke(player.controller);
                Events.onPlayerWhistle.after?.Invoke(player);
            }
        }
    }
}
