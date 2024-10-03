using rose.row.easy_package.audio;
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
            if (SteelInput.GetButtonDown(SteelInput.KeyBinds.Fire))
            {
                var actor = player.controller.actor;
                if (actor.IsSeated() && actor.IsDriver() && !actor.seat.HasActiveWeapon())
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
