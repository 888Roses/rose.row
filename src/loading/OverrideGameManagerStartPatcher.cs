using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rose.row.loading
{
    [HarmonyPatch(typeof(GameManager), "Start")]
    internal class OverrideGameManagerStartPatcher
    {
        [HarmonyPrefix]
        private static bool prefix(GameManager __instance)
        {
            #region ui

            Traverse.Create(__instance).Method("UpdateUIScale").GetValue();

            if (EventSystem.current != null)
                EventSystem.current.sendNavigationEvents = false;

            #endregion ui

            #region game info

            // This is usually located just before "if (EventSystem.current != null)".
            // If there's an error, don't hesitate putting it back where it was :)
            Debug.Log("Reset gameInfo");
            __instance.gameInfo = GameInfoContainer.Default();

            #endregion game info

            #region launch options

            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int i = 0; i < commandLineArgs.Length; i++)
            {
                string[] array = commandLineArgs[i].Split(new char[] { ' ' });
                string text = "";

                for (int j = 1; j < array.Length; j++)
                {
                    text += array[j];
                    if (j < array.Length - 1)
                        text += " ";
                }

                Traverse.Create(__instance).Method("HandleArgument", array[0].ToLowerInvariant(), text).GetValue();
            }

            #endregion launch options

            // Usually the mod manager will now load all of the mods, but in our case, we want to take
            // the hand on the loading process and use our own GameLoadSchedule instead!
            // ModManager.instance.OnGameManagerStart();
            GameLoadSchedule.startLoading();

            #region armed map

            var autoStartMapArmed = (bool) Traverse.Create(__instance).Field("autoStartMapArmed").GetValue();
            if (autoStartMapArmed && ModManager.instance.contentHasFinishedLoading)
                Traverse.Create(__instance).Method("AutoStartArmedMap").GetValue();

            #endregion

            // Unallow default behaviour.
            return false;
        }
    }
}