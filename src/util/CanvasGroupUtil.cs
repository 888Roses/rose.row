using UnityEngine;

namespace rose.row.util
{
    public static class CanvasGroupUtil
    {
        public static bool isEnabled(this CanvasGroup canvasGroup) => canvasGroup.alpha == 1f;

        public static void setEnabled(this CanvasGroup canvasGroup,
                                      bool enabled, bool changeInteractability = true, bool changeBlockRaycasts = true)
        {
            canvasGroup.alpha = enabled ? 1f : 0f;

            if (changeInteractability)
                canvasGroup.interactable = enabled;

            if (changeBlockRaycasts)
                canvasGroup.blocksRaycasts = enabled;
        }
    }
}