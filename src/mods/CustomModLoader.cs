using HarmonyLib;
using Lua;
using System;
using UnityEngine.Profiling;

namespace rose.row.mods
{
    public class CustomModLoader
    {
        // RN I'm loading mods manually as testing mods.
        // TODO: Find a way to loads as an official one maybe?
        public static object testingModInfo
            => Traverse.Create<ModInformation>().Field("Testing").GetValue();

        public static void load(string path)
        {
            GC.Collect();

            var totalAllocatedMemoryLong = Profiler.GetTotalAllocatedMemoryLong();

            // This is the same as:
            // ModManager.instance.LoadSingleModContentBundle(path, testingModInfo);
            // but since vs is being a moron I can't have access to newer methods which
            // are what i need :(
            // so i have to use traverse which is HIGHLY unstable and hard to fix when it
            // comes to actually upgrading the game to newer versions.
            Traverse
                .Create(ModManager.instance)
                .Method("LoadSingleModContentBundle", path, testingModInfo)
                .GetValue();

            GC.Collect();

            var memoryAmount = (int) ((Profiler.GetTotalAllocatedMemoryLong() - totalAllocatedMemoryLong) / 1000000L);
            ScriptConsole.instance.LogInfo($"Loaded content mod: {path}, memory usage: {memoryAmount} MB");

            Traverse
                .Create(GameManager.instance.gameInfo)
                .Method("AdditiveLoadSingleMod", testingModInfo)
                .GetValue();
        }
    }
}