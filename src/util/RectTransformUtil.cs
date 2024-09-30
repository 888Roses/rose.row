using UnityEngine;

namespace rose.row.util
{
    public static class RectTransformUtil
    {
        public static void stretchToParentDimensions(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;
        }
    }
}