using rose.row.easy_events;
using System;
using UnityEngine;

namespace rose.row.loading
{
    public class ModLoadAction : LoadAction
    {
        private void onModManagerFinishedLoadingContent()
        {
            stop(LoadResult.Success);
        }

        public override void start(Action<LoadResult> onFinished)
        {
            Debug.Log($"[ModLoadAction]: Started mod loading.");

            base.start(onFinished);

            // Subscribe to be notified when the mod manager will have loaded every mod in the game.
            Events.onFinalizeLoadedModContent.after += onModManagerFinishedLoadingContent;
            // Start loading mods.
            ModManager.instance.OnGameManagerStart();
        }

        public override void stop(LoadResult result)
        {
            Debug.Log($"[ModLoadAction]: Finished mod loading.");

            // Unsubscribe since this object will then be disposed, and we don't want empty events lying around.
            Events.onFinalizeLoadedModContent.after -= onModManagerFinishedLoadingContent;

            base.stop(result);
        }
    }
}
