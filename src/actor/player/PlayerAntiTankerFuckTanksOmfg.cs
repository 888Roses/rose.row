using rose.row.util;
using UnityEngine;

namespace rose.row.actor.player
{
    public class PlayerAntiTankerFuckTanksOmfg : PlayerBehaviour
    {

        private Vehicle _tagged;
        private bool _isActive;

        private void Update()
        {
            if (_isActive)
            {
                _tagged.transform.position -= Physics.gravity * 4 * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                if (_isActive)
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        DecalManager.CreateBloodDrop(_tagged.transform.position + Random.insideUnitSphere * 5f, Random.insideUnitSphere * Random.Range(5, 20), 1);
                    }

                    _tagged.rigidbody.velocity = Vector3.zero;

                    _tagged.executePrivate("Explode");
                    _tagged.Die(DamageInfo.Explosion);

                    _isActive = false;
                    _tagged = null;
                }
                else
                {
                    if (Physics.Raycast(player.cameraForward(), out RaycastHit hit))
                    {
                        if (hit.transform.TryGetComponent(out Vehicle vehicle))
                        {
                            setActive(vehicle);
                        }
                    }
                }
            }

            if (_isActive)
            {
                for (int i = 0; i < 15; i++)
                    DecalManager.CreateBloodDrop(_tagged.transform.position - Vector3.up * 2f, Random.insideUnitSphere * Random.Range(5, 15), 1);
            }
        }

        private void setActive(Vehicle vehicle)
        {
            _tagged = vehicle;
            _isActive = true;
        }

    }
}
