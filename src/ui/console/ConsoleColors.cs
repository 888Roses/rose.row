using UnityEngine;

namespace rose.row.ui.console
{
    public static class ConsoleColors
    {
        public static readonly Color32 background = new Color32(0, 0, 0, 255);

        public static readonly Color32 log = new Color32(206, 242, 220, 255);
        public static readonly Color32 error = new Color32(223, 109, 141, 255);
        public static readonly Color32 exception = new Color32(238, 42, 97, 255);
        public static readonly Color32 assert = new Color32(42, 136, 238, 255);
        public static readonly Color32 warning = new Color32(255, 220, 171, 255);

        public static readonly Color32 inputFieldText = new Color32(206, 242, 220, 255);
        public static readonly Color32 inputFieldPlaceholder = new Color32(206, 242, 220, 100);

        public static readonly Color32 autoCompletionBorder = new Color32(206, 242, 220, 100);
        public static readonly Color32 autoCompletionSuggestionBackground = new Color32(206, 242, 220, 50);
        public static readonly Color32 autoCompletionSuggestionText = new Color32(206, 242, 220, 125);
        public static readonly Color32 autoCompletionSuggestionDescription = new Color32(206, 242, 220, 75);
    }
}