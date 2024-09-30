using HarmonyLib;

namespace rose.row.actor.player.camera
{
    [HarmonyPatch(typeof(PlayerFpParent), "Awake")]
    internal class PlayerCameraManagerPatcher
    {
        [HarmonyPostfix]
        private static void postfix(PlayerFpParent __instance)
            => __instance.gameObject.AddComponent<PlayerCameraManager>().setup(__instance);
    }
}