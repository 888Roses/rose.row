using rose.row.actor.ai;
using rose.row.actor.health;
using rose.row.actor.player;
using rose.row.actor.player.camera;
using rose.row.audio;
using rose.row.data.mod;
using rose.row.dev.dev_info_screen;
using rose.row.dev.dev_weapon_selec;
using rose.row.dev.vehicle_selector;
using rose.row.easy_events;
using rose.row.main_menu.ui;
using rose.row.rendering.game_preview;
using rose.row.ui.console;
using rose.row.ui.ingame.crosshair;
using rose.row.ui.ingame.ingame_displayables;
using rose.row.ui.ingame.scoreboard;
using rose.row.ui.ingame.screens.death_screen;
using rose.row.ui.ingame.screens.end_screen;
using rose.row.ui.ingame.screens.killfeed;
using rose.row.ui.ingame.screens.pause_menu;
using rose.row.ui.spawn_menu;
using rose.row.util;

namespace rose.row
{
    public static class EventInitializer
    {
        public static void initialize()
        {
            ModHelper.initializeEvents();

            ConsoleManager.create();
            DeveloperInfoScreen.create();
            AudioEvents.initializeEvents();
            MainMenuUiManager.create().initialize();

            KillfeedProvider.initializeOwnEvents();
            CustomActorHealth.subscribeToInitializationEvents();
            DisplayableUi.initializeEvents();
            EndGameScreen.initializeEvents();
            DeathScreen.initializeEvents();

            AiEventsListener.initializeEvents();
            Scoreboard.initializeEvents();

            Events.onAllContentLoaded.after += () =>
            {
                VehiclePreviewManager.create();
            };

            Events.onFinishedRenderingVehiclePreviews.after += () =>
            {
                //WeaponPreviewManager.create();
            };

            Events.onGameManagerStartLevel.before += () =>
            {
                PauseMenu.create();
                DeathCamera.create();
                DeathScreen.create();
                WeaponSelectionScreen.create();
                VehicleSelectionScreen.create();

                CrosshairManager.create();

                KillfeedProvider.createKillfeedManager();
            };

            Events.onGameManagerStartLevel.after += () =>
            {
                SpawnPointUtil.randomizeSpawnPoints();

                ScoreboardScreen.create(); // TODO: Renable scoreboard in-game instead of in the main menu.
                SpawnMenuScreen.create();
            };

            // Very important that this stays AFTER "Events.onGameManagerStartLevel.after", because it contains a
            // show method that makes use of the created spawn menu screen. If it is anywhere BEFORE "Events.onGameManagerStartLevel.after",
            // it'd try to access the created spawn menu screen BEFORE it was even created in the first place.
            SpawnMenu.initializeEvents();

            Events.onPlayerSpawn.before += Player.create;
        }
    }
}