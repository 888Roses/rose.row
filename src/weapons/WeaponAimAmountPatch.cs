using HarmonyLib;

namespace rose.row.weapon
{
    [HarmonyPatch(typeof(Weapon), "Awake")]
    internal class WeaponAimAmountPatch
    {
        [HarmonyPostfix]
        static void postfix(Weapon __instance)
        {
            if (__instance.TryGetComponent(out AdvancedWeapon weapon))
            {
                weapon.weapon = __instance;
            }
            else
            {
                __instance.gameObject.AddComponent<AdvancedWeapon>().weapon = __instance;
            }
        }
    }
}
