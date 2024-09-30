using rose.row.weapon;
using System.Collections.Generic;

namespace rose.row.util
{
    public static class WeaponUtil
    {
        public static readonly Dictionary<Weapon, AdvancedWeapon> registry = new Dictionary<Weapon, AdvancedWeapon>();

        public static AdvancedWeapon getAdvancedWeapon(this Weapon weapon)
        {
            if (registry.ContainsKey(weapon))
                return registry[weapon];

            var advancedWeapon = weapon.gameObject.AddComponent<AdvancedWeapon>();
            advancedWeapon.weapon = weapon;
            registry.Add(weapon, advancedWeapon);
            return advancedWeapon;
        }

        public static float getAimingAmount(this Weapon weapon)
            => weapon.getAdvancedWeapon().aimAmount;
    }
}
