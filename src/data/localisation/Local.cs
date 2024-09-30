using rose.row.data.localisation.serialisation;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace rose.row.data.localisation
{
    public readonly struct Language
    {
        public readonly string path;
        public readonly string name;
        public readonly LanguageRoot languageRoot;

        public Language(string path, string name, LanguageRoot data)
        {
            this.path = path;
            this.name = name;
            languageRoot = data;
        }
    }

    public static class Local
    {
        public static readonly List<Language> languages = new List<Language>();
        public static int currentLanguageIndex = 0;
        public static Language current => languages[currentLanguageIndex];
        public static string localisationDirectoryPath => $"{Constants.basePath}/Localisation";

        /// <summary>
        /// Loads every languages from the localisation directory.
        /// </summary>
        public static void populateLanguages()
        {
            var files = Directory.GetFiles(localisationDirectoryPath, "*.xml");
            var serializer = new XmlSerializer(typeof(LanguageRoot));
            foreach (var file in files)
            {
                using (var reader = new StringReader(File.ReadAllText(file)))
                {
                    var root = (LanguageRoot) serializer.Deserialize(reader);
                    languages.Add(new Language(file, root.language, root));
                }
            }
        }

        public static string get(string name)
        {
            if (name.StartsWith("@") && name.Length > 1)
                name = name.Substring(1);

            foreach (var key in current.languageRoot.data)
            {
                if (key.name == name)
                    return key.value;
            }

            return name;
        }
    }
}