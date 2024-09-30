using rose.row.actor.ai;
using rose.row.actor.player;
using rose.row.easy_package.events;
using rose.row.main_menu.ui.desktop.war.missions;
using rose.row.match;
using UnityEngine;

namespace rose.row.easy_events
{
    public static class Events
    {
        public static DynamicEvent<FpsActorController> onPlayerSpawn;
        public static DynamicEvent<FpsActorController> onPlayerDie;

        public static DynamicEvent<Actor, DamageInfo, bool> onActorDie;
        public static DynamicEvent<Actor, DamageInfo> onActorHurt;
        public static DynamicEvent<Actor> onActorAwake;

        /// <summary>
        /// Note that this event will not actually have the actor fall over since this functionality is disabled by default.
        /// </summary>
        public static DynamicEvent<Actor> onActorFallOver;

        /// <summary>
        /// Last int represents the team.
        /// </summary>
        public static DynamicEvent<Vehicle, VehicleSpawner.VehicleSpawnType, Vector3, Quaternion, int> onVehicleSpawn;

        public static DynamicEvent<Vehicle, DamageInfo> onVehicleDie;

        /// <summary>
        /// Called when the game manager actually starts a game.
        /// </summary>
        public static DynamicEvent onGameManagerStartLevel;

        /// <summary>
        /// int winner, bool allowContinueBattle = true
        /// </summary>
        public static DynamicEvent<int, bool> onMatchEnded;

        /// <summary>
        /// Called when the <see cref="MatchManager"/> executes <see cref="MatchManager.startGame(WarMission)"/>.
        /// </summary>
        public static DynamicEvent onMatchManagerStartGame;

        public static DynamicEvent onReturnToMenu;
        public static DynamicEvent<SpawnPoint, int, bool> onPointCaptured;

        public static DynamicEvent onLoadoutUiStart;
        public static DynamicEvent<bool> onLoadoutUiShow;

        public static DynamicEvent onAllContentLoaded;
        public static DynamicEvent<LoadModWorker.State> onLoadModWorkerEndJob;
        public static DynamicEvent onStartLoadingMods;

        public static DynamicEvent<Player> onPlayerWhistle;
        public static DynamicEvent<AdvancedAi> onAiWhistle;
        public static DynamicEvent<ActorController> onWhistle;

        public static DynamicEvent onFinishedRenderingVehiclePreviews;
    }
}