using System;
using UnityEngine;

namespace rose.row.loading
{
    public class InitializePostLoadEventsLoadAction : LoadAction
    {
        public override void start(Action<LoadResult> onFinished)
        {
            Debug.Log("[InitializePostLoadEventsLoadAction]: Initializing post load events.");
            base.start(onFinished);

            EventInitializer.initializePostLoadEvents();
            stop(LoadResult.Success);
        }
    }
}
