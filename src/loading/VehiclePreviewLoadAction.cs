using rose.row.easy_events;
using rose.row.rendering.game_preview;
using System;
using UnityEngine;

namespace rose.row.loading
{
    public class VehiclePreviewLoadAction : LoadAction
    {
        private void onFinishedRenderingVehiclePreviews()
        {
            stop(LoadResult.Success);
        }

        public override void start(Action<LoadResult> onFinished)
        {
            Debug.Log("[VehiclePreviewLoadAction]: Started rendering or pulling vehicle previews.");
            base.start(onFinished);

            Events.onFinishedRenderingVehiclePreviews.after += onFinishedRenderingVehiclePreviews;
            VehiclePreviewManager.create();
        }

        public override void stop(LoadResult result)
        {
            Debug.Log("[VehiclePreviewLoadAction]: Finished rendering or pulling vehicle previews.");
            Events.onFinishedRenderingVehiclePreviews.after -= onFinishedRenderingVehiclePreviews;

            base.stop(result);
        }
    }
}
