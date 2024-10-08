using rose.row.easy_package.audio;
using rose.row.util;
using UnityEngine;

namespace rose.row.actor.player
{
    // TODO: Change this whenever I'll implement vehicles!
    public class PlayerVehicleHornTemp : PlayerBehaviour
    {
        private AudioSource _hornAudioSource;

        private void Awake()
        {
            _hornAudioSource = Audio.createAudioSource(name: "Horn Audio Source", parent: transform);
            _hornAudioSource.loop = true;
            _hornAudioSource.playOnAwake = false;
            _hornAudioSource.clip = AudioRegistry.hornTemp.get();
        }

        private void Update()
        {
            var actor = player.controller.actor;
            if (actor.IsSeated() && actor.IsDriver())
            {
                if (actor.seat.HasActiveWeapon() && actor.seat.activeWeapon.enabled)
                {
                    if (actor.seat.activeWeapon.isHorn())
                    {
                        Destroy(actor.seat.activeWeapon);
                        actor.seat.activeWeapon = null;
                    }
                    else
                    {
                        return;
                    }
                }

                if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Fire))
                {
                    _hornAudioSource.Play();
                }
            }

            if (SteelInput.GetButtonUp(SteelInput.KeyBinds.Fire) && _hornAudioSource.isPlaying)
            {
                _hornAudioSource.Stop();
            }
        }
    }
}
