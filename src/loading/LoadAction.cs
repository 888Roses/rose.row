using System;

namespace rose.row.loading
{
    public abstract class LoadAction : ILoadAction
    {
        public Action onStarted;
        public Action onPaused;

        public Action<LoadResult> onFinished;

        public static T start<T>(Action<LoadResult> onFinished) where T : LoadAction
        {
            return (T) start(typeof(T), onFinished);
        }

        public static LoadAction start(Type type, Action<LoadResult> onFinished)
        {
            var loadAction = (LoadAction) Activator.CreateInstance(type);
            loadAction.start(onFinished);
            return loadAction;
        }

        public virtual void pause(LoadContext context)
        {
            onPaused?.Invoke();
        }

        public virtual void start(Action<LoadResult> onFinished)
        {
            onStarted?.Invoke();
            this.onFinished += onFinished;
        }

        public virtual void stop(LoadResult result)
        {
            onFinished?.Invoke(result);
        }
    }
}
