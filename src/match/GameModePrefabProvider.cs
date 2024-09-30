using System.Collections.Generic;
using UnityEngine;

namespace rose.row.match
{
    public static class GameModePrefabProvider
    {
        public static Transform gameModePrefabsContainer;
        public static readonly Dictionary<string, GameModeBase> gameModes = new Dictionary<string, GameModeBase>();

        public static T createGameModePrefab<T>(string name) where T : GameModeBase
        {
            if (gameModePrefabsContainer == null)
                createGameModePrefabsContainer();

            var gameObject = new GameObject(name + " GameMode Prefab");
            gameObject.transform.SetParent(gameModePrefabsContainer);
            var prefab = gameObject.AddComponent<T>();

            if (gameModes.ContainsKey(name))
            {
                gameModes[name] = prefab;
            }
            else
            {
                gameModes.Add(name, prefab);
            }

            return prefab;
        }

        private static void createGameModePrefabsContainer()
        {
            gameModePrefabsContainer = new GameObject("GameMode Prefabs Container").transform;
            GameObject.DontDestroyOnLoad(gameModePrefabsContainer.gameObject);
        }
    }
}