using HarmonyLib;
using rose.row.easy_events;

namespace rose.row.data.events.patchers.loadmodworker
{
    [HarmonyPatch(typeof(LoadModWorker), "EndJob")]
    internal class onLoadModWorkerEndJobPatcher
    {
        [HarmonyPrefix]
        static void prefix(LoadModWorker.State state) => Events.onLoadModWorkerEndJob.before?.Invoke(state);

        [HarmonyPostfix]
        static void postfix(LoadModWorker.State state) => Events.onLoadModWorkerEndJob.after?.Invoke(state);
    }
}
