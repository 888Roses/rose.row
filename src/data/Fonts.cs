using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace rose.row.data
{
    public static class Fonts
    {
        public static string basePath => $"{Constants.basePath}/Fonts/";
        public static readonly string defaultFont = "FetteEngDOT_HG.ttf";
        public static readonly string fancyFont = "AlteDin.ttf";
        public static readonly string titleFont = "AiramLT_Std.ttf";
        public static readonly string consoleFont = "SourceCodePro-Regular.ttf";

        public static readonly Dictionary<string, TMP_FontAsset> fonts = new Dictionary<string, TMP_FontAsset>();

        public static TMP_FontAsset getFont(string name) => getFont(name, name);

        public static TMP_FontAsset getFont(string fileName, string registryName)
        {
            if (fonts.ContainsKey(registryName))
                return fonts[registryName];

            var path = Path.Combine(basePath, fileName);
            if (File.Exists(path))
            {
                var font = new Font(path);
                var createdFontAsset = TMP_FontAsset.CreateFontAsset(font);
                fonts[registryName] = createdFontAsset;
                return createdFontAsset;
            }

            Debug.LogError($"Could not find font with name '{fileName}' ({path}).");
            return null;
        }

        private static TMP_FontAsset _defaultHeroesAndGeneralsFontShadow;

        public static TMP_FontAsset getDefaultHeroesAndGeneralsFontShadow()
        {
            const string k_Name = "FetteEngDOT_HG_Shadow.otf";

            if (_defaultHeroesAndGeneralsFontShadow == null)
            {
                _defaultHeroesAndGeneralsFontShadow = getFont(defaultFont, k_Name);
            }

            return _defaultHeroesAndGeneralsFontShadow;
        }
    }
}