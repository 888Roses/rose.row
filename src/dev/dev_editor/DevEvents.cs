using rose.row.easy_package.events;

namespace rose.row.dev.dev_editor
{
    public static class DevEvents
    {
        /// <summary>
        /// Called whenever the user tries to start a session on a sandbox level using the dev tools.
        /// This events also provides a Boolean value representing whether the level could be started or not.
        /// </summary>
        /// <remarks>
        /// In the case of the <see cref="DynamicEvent.before"/>, this Boolean value will always be true.
        /// </remarks>
        public static DynamicEvent<bool> onStartedSandboxLevel;
    }
}
