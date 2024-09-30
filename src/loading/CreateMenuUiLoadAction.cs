using System;
using UnityEngine;

namespace rose.row.loading
{
    public class CreateMenuUiLoadAction : LoadAction
    {
        public override void start(Action<LoadResult> onFinished)
        {
            Debug.Log("[CreateMenuUiLoadAction]: Creating menu Ui.");
            base.start(onFinished);

            EventInitializer.createMenuUi();
            stop(LoadResult.Success);
        }
    }
}
