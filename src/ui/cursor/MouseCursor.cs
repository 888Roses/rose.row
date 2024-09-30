using System;
using System.Collections.Generic;

namespace rose.row.ui.cursor
{
    public static class MouseCursor
    {
        public static readonly List<Func<FpsActorController, bool>> cursorHandlers = new List<Func<FpsActorController, bool>>()
        {
            (controller) => controller.unlockCursorRavenscriptOverride,
            (controller) => IngameMenuUi.IsOpen(),
            (controller) => LoadoutUi.IsOpen(),
            (controller) => StrategyUi.IsOpen(),
            (controller) => ConfigFlagsUi.IsOpen(),
            (controller) => ScoreboardUi.IsOpenAndFocused(),
        };
    }
}