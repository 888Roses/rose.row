using UnityEngine;

namespace rose.row.util
{
    public static class PlayerPrefsUtil
    {
        // Using a string and not an int32 since a one char string is lighter (2b) than an int32 (4b).
        public static void setBool(string key, bool value) => PlayerPrefs.SetString(key, value ? "t" : "f");
        public static bool getBool(string key) => PlayerPrefs.GetString(key) == "t";
        public static bool getBool(string key, bool defaultValue)
            => PlayerPrefs.GetString(key, defaultValue ? "t" : "f") == "t";
    }
}
