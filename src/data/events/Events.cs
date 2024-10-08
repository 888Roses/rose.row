using rose.row.actor.ai;
using rose.row.actor.player;
using rose.row.client;
using rose.row.data;
using rose.row.easy_package.events;
using rose.row.match;
using UnityEngine;

namespace rose.row.easy_events
{
    public static class Events
    {
        #region loading process

        public static DynamicEvent<LoginInfo> onLoggedIn;
        public static DynamicEvent onFinishedLoading;
        public static DynamicEvent onFinishedRenderingVehiclePreviews;

        #region mods

        public static DynamicEvent onAllContentLoaded;
        public static DynamicEvent<LoadModWorker.State> onLoadModWorkerEndJob;
        public static DynamicEvent onStartLoadingMods;
        public static DynamicEvent onFinalizeLoadedModContent;

        #endregion mods

        #endregion loading process

        #region match

        /// <summary>
        /// Event called when the <see cref="GameManager"/> starts a match using the given map and paremeters.
        /// </summary>
        /// <remarks>
        /// This event is not to be mistaken with <see cref="onMatchManagerLaunchMatch"/>, which is called before this one,
        /// indicating the <see cref="GameManager"/> what map and game parameters to use for this match.
        /// </remarks>
        public static DynamicEvent onGameManagerStartLevel;

        /// <summary>
        /// Event called when the current match has ended.
        /// 
        /// <list type="table">
        ///     <listheader>This event provides the following parameters:</listheader>
        ///     <item>
        ///         <term><see cref="int"/></term>
        ///         <description>The team who will win or has won this match.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="bool"/></term>
        ///         <description>Whether the <see cref="GameModeBase.activeGameMode"/> allows the match to keep playing after it ended or not.</description>
        ///     </item>
        /// </list>
        /// </summary>
        public static DynamicEvent<int, bool> onMatchEnded;

        /// <summary>
        /// Event called when the <see cref="MatchManager"/> joins a new match.
        /// </summary>
        /// <remarks>
        /// This is not to be mistaken with <see cref="onGameManagerStartLevel"/>, which is called when the
        /// <see cref="GameManager"/> starts a game on a level with the given game parameters.
        /// This event is called before that, since it tells the <see cref="GameManager"/> what parameters and what
        /// map to start a game on.
        /// </remarks>
        public static DynamicEvent onMatchManagerLaunchMatch;

        /// <summary>
        /// Event called when a match is left to return to the main menu.
        /// Is called when <see cref="GameManager.ReturnToMenu"/> is executed.
        /// </summary>
        public static DynamicEvent onReturnToMenu;

        #endregion match

        #region loadout ui

        public static DynamicEvent onLoadoutUiStart;
        public static DynamicEvent<bool> onLoadoutUiShow;

        #endregion loadout ui

        #region capture point

        /// <summary>
        /// Event called when any <see cref="SpawnPoint"/> was captured or neutralized.
        /// 
        /// <list type="table">
        ///     <listheader>This event provides the following parameters:</listheader>
        ///     <item>
        ///         <term><see cref="SpawnPoint"/></term>
        ///         <description>The spawn point that will be captured or has been captured.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="int"/></term>
        ///         <description>The team that will capture or has captured the spawn point.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="bool"/></term>
        ///         <description>Describes whether the team that will capture or has captured the spawn point is the original owner of the spawn point or not.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// When a spawn point is neutralized, the capturing team is set to -1.
        /// </remarks>
        public static DynamicEvent<SpawnPoint, int, bool> onSpawnPointCaptured;

        #endregion capture point

        #region actor

        #region any

        /// <summary>
        /// Event called when an <see cref="Actor"/> is first created and "awakes".
        /// See Unity monobehaviour methods for more information on that.
        /// 
        /// <list type="table">
        ///     <listheader>This event provides the following parameters:</listheader>
        ///     <item>
        ///         <term><see cref="Actor"/></term>
        ///         <description>The actor that will awake or has awaken.</description>
        ///     </item>
        /// </list>
        /// </summary>
        public static DynamicEvent<Actor> onActorAwake;

        /// <summary>
        /// Event called when an <see cref="Actor"/> spawns at a given location, rotation and with a given weapon set.
        /// 
        /// <list type="table">
        ///     <listheader>This event provides the following parameters:</listheader>
        ///     <item>
        ///         <term><see cref="Actor"/></term>
        ///         <description>The actor that will spawn in the world, or has spawned in the world.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="Vector3"/></term>
        ///         <description>The position in world space where the actor will spawn or has spawned.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="Quaternion"/></term>
        ///         <description>The rotation in world space with which the actor will spawn or has spawned.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="WeaponManager.LoadoutSet"/></term>
        ///         <description>The loadout with which the actor will spawn or has spawned.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// By default, when the <see cref="Actor.SpawnAt(Vector3, Quaternion, WeaponManager.LoadoutSet)"/> method is called,
        /// the <see cref="WeaponManager.LoadoutSet"/> is null. Expect a null provided parameter here too.
        /// </remarks>
        public static DynamicEvent<Actor, Vector3, Quaternion, WeaponManager.LoadoutSet> onActorSpawnAt;
        /// <summary>
        /// Event called when an <see cref="Actor"/> dies.
        ///
        /// <list type="table">
        ///     <listheader>This event provides the following parameters:</listheader>
        ///     <item>
        ///         <term><see cref="Actor"/></term>
        ///         <description>The actor that has died or will die.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="DamageInfo"/></term>
        ///         <description>Information about how the actor died or will die (amount, source...)</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="bool"/></term>
        ///         <description>Whether the actor's death was silent or not.</description>
        ///     </item>
        /// </list>
        /// </summary>
        public static DynamicEvent<Actor, DamageInfo, bool> onActorDie;

        /// <summary>
        /// Event called when an <see cref="Actor"/> is damaged by any source.
        ///
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="Actor"/></term><description>The actor that has been damaged or will be damaged.</description></item>
        /// <item><term><see cref="DamageInfo"/></term><description>Information about how the actor was damaged or will be damaged (amount, source...)</description></item>
        /// </summary>
        public static DynamicEvent<Actor, DamageInfo> onActorHurt;

        /// <summary>
        /// Event called when an <see cref="Actor"/> leaves its seat using <see cref="Actor.LeaveSeat(bool)"/>.
        ///
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="Actor"/></term><description>The actor that has left its seat or will leave its seat.</description></item>
        /// <item><term><see cref="bool"/></term><description>Whether the actor is forced out of its seat by falling over or not.</description></item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// Even if the <see cref="bool"/> provided parameter is equal to True, the <see cref="Actor"/> will only fall over if
        /// <see cref="Constants.k_ActorCanFallOver"/> is also set to true.
        /// </remarks>
        public static DynamicEvent<Actor, bool> onActorLeaveSeat;

        /// <summary>
        /// Event called when an <see cref="Actor"/> turns into ragdoll and gets affected by physics that way (loses control).
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="Actor"/></term><description>The actor that will fall over or has fallen over.</description></item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// Note: While this event is called, it will only have the <see cref="Actor"/> fall over if <see cref="Constants.k_ActorCanFallOver"/> is set to true.
        /// </remarks>
        public static DynamicEvent<Actor> onActorFallOver;

        /// <summary>
        /// Event called when any <see cref="Actor"/> whistles.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="ActorController"/></term><description>The <see cref="ActorController"/> of the actor that whistled or will whistle.</description></item>
        /// </list>
        /// </summary>
        public static DynamicEvent<ActorController> onActorWhistle;

        /// <summary>
        /// Event called when any <see cref="Actor"/> switches active weapon.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="Actor"/></term><description>The actor that switched active weapon.</description></item>
        /// <item><term><see cref="int"/></term><description>The new active weapon slot.</description></item>
        /// </list>
        /// </summary>
        public static DynamicEvent<Actor, int> onActorSwitchActiveWeapon;

        #endregion any

        #region ai

        /// <summary>
        /// Event called when an AI actor whistles.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="AdvancedAi"/></term><description>The AI actor that will whistle or has whistled.</description></item>
        /// </list>
        /// </summary>
        public static DynamicEvent<AdvancedAi> onAiWhistle;

        #endregion ai

        #region player

        /// <summary>
        /// Event called when the local player spawns in the world.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="FpsActorController"/></term><description>The <see cref="ActorController"/> of the local player.</description></item>
        /// </list>
        /// </summary>
        public static DynamicEvent<FpsActorController> onPlayerSpawn;

        /// <summary>
        /// Event called when the local player dies.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="FpsActorController"/></term><description>The <see cref="ActorController"/> of the local player.</description></item>
        /// </list>
        /// </summary>
        public static DynamicEvent<FpsActorController> onPlayerDie;

        /// <summary>
        /// Event called when the local player whistles.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item><term><see cref="Player"/></term><description>The player that will whistle or has whistled.</description></item>
        /// </list>
        /// </summary>
        public static DynamicEvent<Player> onPlayerWhistle;

        #endregion player

        #endregion actor

        #region vehicle

        /// <summary>
        /// Event called when a <see cref="Vehicle"/> spawns in the world.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item>
        /// <term><see cref="Vehicle"/></term>
        /// <description>The vehicle that was spawned in the world.</description>
        /// </item>
        /// <item>
        /// <term><see cref="VehicleSpawner.VehicleSpawnType"/></term>
        /// <description>The type of vehicle that was or will be spawned.</description>
        /// </item>
        /// <item>
        /// <term><see cref="Vector3"/></term>
        /// <description>Where in the world that vehicle was or will be spawned at.</description>
        /// </item>
        /// <item>
        /// <term><see cref="Quaternion"/></term>
        /// <description>The rotation in world space that the vehicle will or was spawned with.</description>
        /// </item>
        /// <item>
        /// <term><see cref="int"/></term>
        /// <description>The team of the vehicle that will or was spawned.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// When using <see cref="DynamicEvent.before"/>, the <see cref="Vehicle"/> provided parameter will be null.
        /// </remarks>

        public static DynamicEvent<Vehicle, VehicleSpawner.VehicleSpawnType, Vector3, Quaternion, int> onVehicleSpawn;

        /// <summary>
        /// Event called when a <see cref="Vehicle"/> dies (<see cref="Vehicle.Die(DamageInfo)"/>.
        /// 
        /// <list type="table">
        /// <listheader>This event provides the following parameters:</listheader>
        /// <item>
        /// <term><see cref="Vehicle"/></term>
        /// <description>The vehicle that will die or has died.</description>
        /// </item>
        /// <item>
        /// <term><see cref="DamageInfo"/></term>
        /// <description>Information about how the vehicle died (damage, source, etc).</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// This event is <b>not</b> called when a vehicle is disabled (<see cref="Vehicle.OnVehicleDisabled(DamageInfo)"/>), but only when it dies!
        /// </remarks>
        public static DynamicEvent<Vehicle, DamageInfo> onVehicleDie;

        #endregion vehicle
    }
}