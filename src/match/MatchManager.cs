using rose.row.easy_events;
using rose.row.main_menu.ui.desktop.war.missions;
using rose.row.util;
using System.Linq;
using UnityEngine;
using static InstantActionMaps;

namespace rose.row.match
{
    public static class MatchManager
    {
        public static void startGame(WarMission mission)
        {
            if (Maps.cachedMaps == null)
                Maps.cacheMaps();

            Events.onMatchManagerLaunchMatch.before?.Invoke();

            CurrentMatch.mission = mission;
            CurrentMatch.playerTeam = mission.type == WarMission.MissionType.Attack ? 1 : 0;
            CurrentMatch.enemyFaction = mission.getRandomFaction();

            // Loading the instant action page so that the different managers present on the page load.
            // If I didnt do that, there would be errors about the map manager not existing, etc.
            MainMenu.instance.OpenPageIndex(MainMenu.PAGE_INSTANT_ACTION);

            // Basically launch a game with a given set of parameters.
            var prefab = GameManager.GetGameModePrefab(GameModeType.PointMatch);
            var pointMatchComponent = prefab.GetComponent<PointMatch>();
            // True for now since we have the EndGameScreen.
            pointMatchComponent.canContinuePlayingAfterGameEnd = true;

            // I would love to keep working on the custom game mode but for now there's other priorities.
            // TODO: Make progress on the testing custom game mode.
            // var prefab = GameModePrefabProvider.gameModes[GameModes.k_TestingGameMode];

            var parameters = new GameModeParameters
            {
                gameModePrefab = prefab,

                actorCount = 40,

                // Adding some variation to the balance so it's a little bit more interesting.
                // This is to replicate the balance variation brought by the "lines" system in
                // HNG, that I didn't add yet. Gotta wait pal, one day it's gon be added :)
                balance = Random.Range(0.4f, 0.6f),

                playerTeam = CurrentMatch.playerTeam,

                // Very slight chance because OH MY GOD this is crap but at the same time I
                // think it's cool that 1/10 times you find urself on a night map.
                nightMode = Random.value < 0.1f,

                // Only for now.
                // TODO: Change this so that the player only has weapons from his faction.
                playerHasAllWeapons = true,

                respawnTime = 1,

                // For a point match, length 0 is 150 points to win.
                // Here are the different lengths:
                // case 0: victoryPoints = 150;
                // case 1: victoryPoints = 300;
                // case 2: victoryPoints = 600;
                // default: victoryPoints = 2000;
                gameLength = 0
            };

            // Determines the map that's gonna be played on.
            var possibleMaps = Maps.cachedMaps;
            // Adds the Island map to the map pool.
            bool isMap(MapEntry x, string name) => x.getDisplayName().ToLowerInvariant() == name;

            foreach (var officialMap in Maps.officialWhitelistedMaps)
            {
                var whitelisted = instance.officialEntries.First(x => isMap(x, officialMap));
                if (whitelisted == null)
                    continue;

                possibleMaps.Add(whitelisted);
            }

            // Throws the name of the possible maps in the console to make my life easier.
            Debug.Log("Available maps:");
            foreach (var possibleMap in possibleMaps)
                Debug.Log($" * '{possibleMap.getDisplayName()}'");
            var map = Maps.cachedMaps.random();
            //var map = islandMap;

            // TODO: Make it so it pulls from a list of black listed vehicles for both teams.
            // HACK: Remove this when we implement spawning with vehicles instead of vehicle spawners.
            // Then, only bikes and tractors will be able to be spawned this way.
            MatchDefaultVehicleBinder.updateVehicles();

            GameManager.StartLevel(map, parameters);

            Events.onMatchManagerLaunchMatch.after?.Invoke();
        }
    }
}