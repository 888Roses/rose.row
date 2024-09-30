using System;

namespace rose.row.loading
{
    public interface ILoadAction
    {
        void start(Action<LoadResult> onFinished);
        void pause(LoadContext context);
        void stop(LoadResult result);
    }
}
