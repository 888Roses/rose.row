using UnityEngine;

namespace rose.row.weapon
{
    public class AdvancedWeapon : MonoBehaviour
    {
        public Weapon weapon;
        public float aimAmount;

        private void Update()
        {
            aimAmount = Mathf.MoveTowards(aimAmount, weapon.aiming ? 1f : 0f, Time.deltaTime * 6f);

            if (weapon.configuration.aimSensitivityMultiplier != 1f)
                weapon.configuration.aimSensitivityMultiplier = 1f;
        }
    }
}
