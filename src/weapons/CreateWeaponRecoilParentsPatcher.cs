using HarmonyLib;
using UnityEngine;

namespace rose.row.weapons
{
    [HarmonyPatch(typeof(FpsActorController), "Awake")]
    internal class CreateWeaponRecoilParentsPatcher
    {
        [HarmonyPrefix]
        static void postfix(FpsActorController __instance)
        {
            var gameObject = new GameObject("Weapon Recoil Parent");
            gameObject.transform.SetParent(__instance.weaponParent.parent);
            gameObject.AddComponent<PlayerRecoilParent>().controller = __instance;
        }
    }
}
