using HarmonyLib;
using rose.row.util;
using UnityEngine;

namespace rose.row.rendering.game_preview
{
    /// <summary>
    /// Fixes the <see cref="GamePreview"/> camera's background being coloured in gray instead of transparent.
    /// </summary>
    [HarmonyPatch(typeof(GamePreview), "Awake")]
    internal class GamePreviewRenderCameraBackgroundFixPatcher
    {
        [HarmonyPostfix]
        static void postfix(GamePreview __instance)
        {
            __instance.vehiclePreviewCamera.backgroundColor = Color.black.with(a: 0);
        }
    }
}
