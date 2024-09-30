using rose.row.easy_events;
using System;
using UnityEngine;

namespace rose.row.loading
{
    public class FinalizeLoadAction : LoadAction
    {
        public override void start(Action<LoadResult> onFinished)
        {
            Debug.Log("[FinalizeLoadAction]: Finished loading.");
            base.start(onFinished);

            Events.onFinishedLoading.before?.Invoke();
            Events.onFinishedLoading.after?.Invoke();

            stop(LoadResult.Success);
        }
    }
}
