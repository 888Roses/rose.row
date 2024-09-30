using rose.row.data;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

namespace rose.row.main_menu.war.war_data
{
    /// <summary>
    /// Represents information about a city on the War map.
    /// </summary>
    public readonly struct CityInfo
    {
        public const int k_NameIndex = 1;
        public const int k_Iso2Index = 5;
        public const int k_CountryIndex = 4;
        public const int k_LatitudeIndex = 2;
        public const int k_LongitudeIndex = 3;

        public readonly string name;
        public readonly string iso2;
        public readonly string country;
        public readonly float latitude;
        public readonly float longitude;

        public CityInfo(string name, string iso2, string country, float latitude, float longitude)
        {
            this.name = name;
            this.iso2 = iso2;
            this.country = country;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        /// <summary>
        /// Creates a new <see cref="CityInfo"/> from a given line of CSV data.
        /// </summary>
        /// <param name="fullLine">
        /// The CSV line which will be converted into a city info.
        /// </param>
        /// <param name="cityInfo">
        /// The created city info.
        /// The value will be default if the returned boolean is false.
        /// </param>
        /// <param name="force">
        /// Whether to restrict the creation to only return true when the city is
        /// contained by the <see cref="WarDatabase.countries"/> list or not.
        /// False = only create if valid, True = always create.
        /// </param>
        /// <returns>
        /// Whether the city info could be created or not.
        /// For instance, if the city info's ISO2 is not contained in the
        /// <see cref="WarDatabase.countries"/> list, this will return false, unless
        /// <paramref name="force"/> is set to true (which will always output a city
        /// info no matter what).
        /// </returns>
        public static bool fromLine(string fullLine, out CityInfo cityInfo, bool force = false)
        {
            var splits = fullLine.Split(',');

            // This is required because we are using a readonly struct.
            // That means that we NEED to assign those values, and that we
            // can't assign them later on. So we have some default values
            // prepared.
            var _name = "";
            var _iso2 = "";
            var _country = "";
            var _lat = 0f;
            var _lng = 0f;

            for (int i = 0; i < splits.Length; i++)
            {
                // We don't care about those info, ignore them.
                if (WarCityDatabase.ignoredCityInfoIndexes.Contains(i))
                    continue;

                var info = splits[i].ToLowerInvariant();
                info = info.Replace("\"", "");

                switch (i)
                {
                    case k_NameIndex:
                        _name = info;
                        break;

                    case k_Iso2Index:
                        _iso2 = info;
                        break;

                    case k_CountryIndex:
                        _country = info;
                        break;

                    case k_LatitudeIndex:
                        _lat = float.Parse(info, NumberStyles.Any, CultureInfo.InvariantCulture);
                        break;

                    case k_LongitudeIndex:
                        _lng = float.Parse(info, NumberStyles.Any, CultureInfo.InvariantCulture);
                        break;
                }
            }

            if (!WarDatabase.countries.Contains(_iso2))
            {
                cityInfo = default;
                return false;
            }

            cityInfo = new CityInfo(_name, _iso2, _country, _lat, _lng);
            return true;
        }
    }

    public static class WarCityDatabase
    {
        /// <summary>
        /// The indexes of the different type of info that we don't care about.
        /// </summary>
        public static readonly int[] ignoredCityInfoIndexes = new int[]
        {
            0, // We don't care about the name, only the ASCII name since this is what we can display.
            6, 7, 8, 9, 10 // We don't care about all that garbage huh, only the important stuff B)
            // BTW, this contains ISO3 name, admin name, capital, pop, and id.
        };

        public static readonly string citiesPath = "War/worldcities.csv";
        public static string getCitiesFullPath => $"{Constants.basePath}/Data/{citiesPath}";

        /// <summary>
        /// Contains every cities currently loaded.
        /// If this list is empty, use <see cref="readCities"/> to load in data.
        /// </summary>
        // (Said cities are sorted alphabetically by their country ISO2. NVM LOL)
        public static readonly List<CityInfo> loadedCities = new List<CityInfo>();

        /// <summary>
        /// Reads cities from the cities database file, and add them to the <see cref="loadedCities"/>.
        /// If the <see cref="loadedCities"/> list already contains cities, it will first clear it.
        /// </summary>
        public static void readCities()
        {
            if (!File.Exists(getCitiesFullPath))
            {
                Debug.LogError($"Could not find the cities database path!");
                return;
            }

            loadedCities.Clear();

            var lines = File.ReadAllLines(getCitiesFullPath).ToList();
            // Removing the header.
            lines.RemoveAt(0);

            for (int i = 0; i < lines.Count; i++)
            {
                // "city","city_ascii","lat","lng","country","iso2","iso3","admin_name","capital","population","id"
                var cityInfoText = lines[i];
                if (CityInfo.fromLine(cityInfoText, out var cityInfo))
                    loadedCities.Add(cityInfo);
            }
        }
    }
}