using rose.row.easy_events;

namespace rose.row.data.mod
{
    public static class ModHelper
    {
        public static bool hasLoadedAllContent { get; private set; }
        public static int activeMods => ModManager.instance.GetActiveMods().Count;
        public static int loadedMods { get; private set; }
        public static bool hasLoadedOnce { get; private set; }
        public static bool isLoading { get; private set; }
        public static float progress;

        public static void initializeEvents()
        {
            Events.onLoadModWorkerEndJob.after += onLoadModWorkerEndJob;
            Events.onAllContentLoaded.after += onAllContentLoaded;
            Events.onStartLoadingMods.before += onStartLoadingMods;
        }

        private static void onStartLoadingMods()
        {
            hasLoadedOnce = true;
            isLoading = true;
        }

        private static void onAllContentLoaded()
        {
            hasLoadedAllContent = true;
            isLoading = false;
        }

        private static void onLoadModWorkerEndJob(LoadModWorker.State state)
        {
            loadedMods++;
        }
    }
}
