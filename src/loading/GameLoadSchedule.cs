using System;
using System.Collections.Generic;

namespace rose.row.loading
{
    public static class GameLoadSchedule
    {
        private static void startChainActions(List<Type> actions)
        {
            var current = actions[0];
            actions.RemoveAt(0);

            if (actions.Count == 0)
            {
                LoadAction.start(current, null);
            }
            else
            {
                LoadAction.start(current, (result) =>
                {
                    if (result == LoadResult.Success)
                        startChainActions(actions);
                });
            }
        }

        /// <summary>
        /// Starts loading the game, including every resource it needs to function properly (mods, textures, updating, etc).
        /// </summary>
        public static void startLoading()
        {
            startChainActions(new List<Type>()
            {
                typeof(CreateMenuUiLoadAction),
                typeof(ModLoadAction),
                typeof(VehiclePreviewLoadAction),
                typeof(InitializePostLoadEventsLoadAction),
            });
        }
    }
}