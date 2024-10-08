using rose.row.actor;
using rose.row.actor.ai;
using rose.row.actor.behaviour;
using rose.row.actor.health;
using rose.row.actor.player;
using rose.row.actor.player.camera;
using rose.row.audio;
using rose.row.data.mod;
using rose.row.dev.dev_editor;
using rose.row.dev.dev_info_screen;
using rose.row.dev.dev_weapon_selec;
using rose.row.dev.vehicle_selector;
using rose.row.easy_events;
using rose.row.main_menu.ui;
using rose.row.spawn;
using rose.row.ui.console;
using rose.row.ui.ingame.crosshair;
using rose.row.ui.ingame.ingame_displayables;
using rose.row.ui.ingame.match_start;
using rose.row.ui.ingame.pickupable_weapons_popup;
using rose.row.ui.ingame.scoreboard;
using rose.row.ui.ingame.screens.death_screen;
using rose.row.ui.ingame.screens.end_screen;
using rose.row.ui.ingame.screens.killfeed;
using rose.row.ui.ingame.screens.pause_menu;
using rose.row.ui.ingame.weapon_display;
using rose.row.ui.spawn_menu;
using rose.row.util;
using rose.row.weapons;

namespace rose.row
{
    public static class EventInitializer
    {
        public static void initializePostLoadEvents()
        {
            Plugin.updateWindow();

            AudioEvents.subscribeToInitializationEvents();
            AiEventsListener.subscribeToInitializationEvents();

            PlayPainSoundOnHurt.subscribeToInitializationEvents();
            PlayCrushedSoundOnPlayerDeath.subscribeToInitializationEvents();
            PlayFallSoundOnPlayerDeath.subscribeToInitializationEvents();

            AdvancedSpawnPoint.subscribeToInitializationEvents();
            AiSpawnInVehicle.subscribeToInitializationEvents();

            KillfeedProvider.subscribeToInitializationEvents();
            CustomActorHealth.subscribeToInitializationEvents();
            DisplayableUi.subscribeToInitializationEvents();

            EndGameScreen.subscribeToInitializationEvents();
            DeathState.subscribeToInitializationEvents();
            // DeathCamera.subscribeToInitializationEvents();

            Scoreboard.subscribeToInitializationEvents();
            ScoreboardElement.subscribeToInitializationEvents();

            PickupableWeapons.subscribeToInitializationEvents();
            ResetPickupableWeaponSlotOnSpawn.subscribeToInitializationEvents();
        }

        public static void createMenuUi()
        {
            Plugin.updateWindow();

            MainMenuUiManager.create().initialize();
            DevEditorScreen.create();
        }

        public static void initialize()
        {
            Plugin.updateWindow();

            ModHelper.subscribeToInitializationEvents();

            ConsoleManager.create();
            DeveloperInfoScreen.create();

            Events.onGameManagerStartLevel.before += () =>
            {
                PauseMenu.create();
                OldDeathCamera.create();
                DeathState.create();
                WeaponSelectionScreen.create();
                VehicleSelectionScreen.create();

                CrosshairManager.create();

                KillfeedProvider.createKillfeedManager();
                PickupableWeaponsPopupScreen.create();
                WeaponDisplayScreen.create();
            };

            Events.onGameManagerStartLevel.after += () =>
            {
                SpawnPointUtil.randomizeSpawnPoints();

                ScoreboardScreen.create(); // TODO: Renable scoreboard in-game instead of in the main menu.
                SpawnMenuScreen.create();

                MatchStartScreen.create();
            };

            // Very important that this stays AFTER "Events.onGameManagerStartLevel.after", because it contains a
            // show method that makes use of the created spawn menu screen. If it is anywhere BEFORE "Events.onGameManagerStartLevel.after",
            // it'd try to access the created spawn menu screen BEFORE it was even created in the first place.
            SpawnMenu.initializeEvents();

            Events.onPlayerSpawn.before += Player.create;

            loadInitialBundles();
        }

        public static void loadInitialBundles()
        {
        }
    }
}