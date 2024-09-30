using rose.row.easy_events;

namespace rose.row.ui.spawn_menu
{
    public static class SpawnMenu
    {
        public static void initializeEvents()
        {
            Events.onPlayerDie.after += (e) => show();
            Events.onPlayerSpawn.after += (e) => hide();
            Events.onGameManagerStartLevel.after += show;
        }

        public static void show()
        {
            SpawnMenuScreen.instance.setEnabled(true);
        }

        public static void hide()
        {
            SpawnMenuScreen.instance.setEnabled(false);
        }
    }
}
