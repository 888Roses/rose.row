using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using rose.row.data;
using rose.row.data.localisation;
using rose.row.easy_package.coroutines;
using rose.row.loading;
using rose.row.main;
using rose.row.match;

namespace rose.row
{
    [BepInPlugin(k_ProjectId, k_ProjectName, k_ProjectVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string k_ProjectId = "rose.row";
        public const string k_ProjectName = "Rise of War";
        public const string k_ProjectVersion = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(k_ProjectId);
        public static ManualLogSource Log;

        private void SendLoadedPluginMessage()
        {
            Logger.LogInfo($"Successfully loaded {k_ProjectName} (Version {k_ProjectVersion})!");
            Logger.LogInfo($"> Thank you very much for using my mod! - Rose");
        }

        private void Awake()
        {
            Harmony.PatchAll();
            SendLoadedPluginMessage();

            Log = Logger;

            // ImageLoader.loadRequiredImages();
            CoroutineManager.create();

            IconChanger.change(
                $"{Constants.basePath}/Textures/icons/small.ico",
                $"{Constants.basePath}/Textures/icons/big.ico"
            );

            SpashScreenSkipper.skip();

            Local.populateLanguages();
            EventInitializer.initialize();
            GameModes.initializeGameModes();
        }

        private void Start()
        {
            IconChanger.change(
                $"{Constants.basePath}/Textures/icons/small.ico",
                $"{Constants.basePath}/Textures/icons/big.ico"
            );
        }
    }
}