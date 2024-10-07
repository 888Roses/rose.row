using HarmonyLib;
using rose.row.easy_events;
using System.Collections.Generic;
using UnityEngine;

namespace rose.row.spawn
{
    [HarmonyPatch(typeof(SpawnPoint), "Start")]
    internal class AdvancedSpawnPointPatcher
    {
        [HarmonyPostfix]
        private static void postfix(SpawnPoint __instance)
        {
            __instance.gameObject.AddComponent<AdvancedSpawnPoint>().initialize(__instance);
        }
    }

    public static class AdvancedSpawnPointUtil
    {
        public static AdvancedSpawnPoint advancedSpawnPoint(this SpawnPoint spawnPoint)
        {
            AdvancedSpawnPoint.tryGetAdvancedSpawnPoint(spawnPoint, out var advancedSpawnPoint);
            return advancedSpawnPoint;
        }

        /// <inheritdoc cref="AdvancedSpawnPoint.tryGetAdvancedSpawnPoint(SpawnPoint, out AdvancedSpawnPoint)"/>
        public static bool tryGetAdvancedSpawnPoint(this SpawnPoint point, out AdvancedSpawnPoint advancedSpawnPoint)
        {
            return AdvancedSpawnPoint.tryGetAdvancedSpawnPoint(point, out advancedSpawnPoint);
        }
    }

    public class AdvancedSpawnPoint : MonoBehaviour
    {
        #region events

        /// <summary>
        /// Subscribes to every events when launching the game.
        /// </summary>
        public static void subscribeToInitializationEvents()
        {
            Events.onPointCaptured.before += onPointCapturedEvent;
        }

        private static void onPointCapturedEvent(SpawnPoint point, int team, bool isInitialOwner)
        {
            if (point.tryGetAdvancedSpawnPoint(out var advanced))
            {
                advanced.onPointCaptured(team, isInitialOwner);
            }
        }

        #endregion

        #region static

        /// <summary>
        /// Describes how the <see cref="register(AdvancedSpawnPoint)"/> method performed.
        /// </summary>
        public enum RegisterResult
        {
            /// <summary>
            /// Represents a <see cref="AdvancedSpawnPoint"/> with a null <see cref="spawnPoint"/>.
            /// </summary>
            NullSpawnPoint,

            /// <summary>
            /// Represents a <see cref="AdvancedSpawnPoint"/> whose <see cref="spawnPoint"/> was already registered
            /// in the <see cref="advancedSpawnPointBinder"/>.
            /// </summary>
            AlreadyRegistered,

            /// <summary>
            /// Represents a registerable <see cref="AdvancedSpawnPoint"/>.
            /// </summary>
            Success
        }

        public static readonly Dictionary<SpawnPoint, AdvancedSpawnPoint> advancedSpawnPointBinder = new();

        /// <summary>
        /// Registers a given <see cref="AdvancedSpawnPoint"/> in the <see cref="advancedSpawnPointBinder"/> dictionary,
        /// while making sure that the provided <see cref="AdvancedSpawnPoint"/> is a valid one. For more information on
        /// the validity of an <see cref="AdvancedSpawnPoint"/>, see the "remarks" section.
        /// </summary>
        ///
        /// <param name="advancedSpawnPoint">
        /// The <see cref="AdvancedSpawnPoint"/> you wish to register, so it can be found easily.
        /// </param>
        ///
        /// <remarks>
        /// An <see cref="AdvancedSpawnPoint"/> is declared valid when:
        /// <list type="number">
        ///     <item>
        ///         It's <see cref="spawnPoint"/> is not null.
        ///     </item>
        ///     <item>
        ///         It's <see cref="spawnPoint"/> is not already registered as a key in the
        ///         <see cref="advancedSpawnPointBinder"/>.
        ///     </item>
        /// </list>
        /// </remarks>
        ///
        /// <returns>
        /// Returns whether the <see cref="AdvancedSpawnPoint"/> could be registered or not. For more information on
        /// how this is determined, see <see cref="RegisterResult"/>.
        /// </returns>
        public static RegisterResult registerAdvancedSpawnPoint(AdvancedSpawnPoint advancedSpawnPoint)
        {
            if (advancedSpawnPoint.spawnPoint == null)
            {
                Debug.LogWarning($"Trying to register an advanced spawn point with a null spawn point.");
                return RegisterResult.NullSpawnPoint;
            }

            if (advancedSpawnPointBinder.ContainsKey(advancedSpawnPoint.spawnPoint))
            {
                Debug.LogWarning($"Trying to register an advanced spawn point with an already registered spawn point.");
                return RegisterResult.AlreadyRegistered;
            }

            advancedSpawnPointBinder.Add(advancedSpawnPoint.spawnPoint, advancedSpawnPoint);
            return RegisterResult.Success;
        }

        /// <summary>
        /// Tries to retrieve an <see cref="AdvancedSpawnPoint"/> corresponding to the provided <see cref="SpawnPoint"/>.
        /// </summary>
        /// <param name="spawnPoint">
        /// The <see cref="SpawnPoint"/> whose <see cref="AdvancedSpawnPoint"/> you wish to retrieve.
        /// </param>
        /// <param name="advancedSpawnPoint">
        /// The retrieved <see cref="AdvancedSpawnPoint"/>, if it could be found.
        /// </param>
        /// <returns>
        /// Whether an <see cref="AdvancedSpawnPoint"/> could be retrieved for the given <see cref="SpawnPoint"/>.
        /// </returns>
        public static bool tryGetAdvancedSpawnPoint(SpawnPoint spawnPoint, out AdvancedSpawnPoint advancedSpawnPoint)
        {
            if (advancedSpawnPointBinder.ContainsKey(spawnPoint))
            {
                advancedSpawnPoint = advancedSpawnPointBinder[spawnPoint];
                return true;
            }

            advancedSpawnPoint = null;
            return false;
        }

        #endregion static

        public SpawnPoint spawnPoint;
        public int previousOwner;

        public void initialize(SpawnPoint spawnPoint)
        {
            this.spawnPoint = spawnPoint;

            var registeredResult = registerAdvancedSpawnPoint(this);
            if (registeredResult != RegisterResult.Success)
            {
                Debug.LogError($"Could not register spawn point as an advanced spawn point. This <b>WILL</b> produce undesired behaviour. Please inspect the warning message above this one and report it to the official discord bug report channel with as much context as possible (map name, pc specs, etc).");
                return;
            }

            previousOwner = spawnPoint.owner;
        }

        /// <summary>
        /// Called whenever this point changed owner.
        /// </summary>
        /// <param name="team">The new owner.</param>
        /// <param name="isInitialOwner">Whether this also is the starting owner of that point.</param>
        /// <remarks>
        /// Since this is called before any logic is called on the spawnpoint (after changing owner that is),
        /// the <see cref="SpawnPoint.owner"/> remains the previous owner of that spawnpoint. For this reason,
        /// we can safely use that field to store the previous owner of the spawn point.
        /// </remarks>
        private void onPointCaptured(int team, bool isInitialOwner)
        {
            previousOwner = spawnPoint.owner;
        }
    }
}