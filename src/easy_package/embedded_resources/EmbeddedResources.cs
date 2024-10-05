using rose.row.easy_package.asset_bundles;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace rose.row.easy_package.embedded_resources
{
    public static class EmbeddedResources
    {
        /// <summary>
        /// Creates a stream for a given bundle.
        /// This may then be used to load said bundle using, for instance,
        /// <see cref="AssetBundles.loadFromStream(Stream, System.Action{AssetBundleResult})"/>.
        /// </summary>
        /// <param name="name">
        /// The path or name of the bundle you want to make a stream with.
        /// Example: "dev_flat_ui".
        /// Note that while the full name of the bundle is "row.assets.dev_flat_ui", you only need to feed the last part of it).
        /// </param>
        /// <param name="appendPrefix">
        /// If passed True, the <paramref name="name"/> won't have to start by "rose.row.{name}".
        /// Set this to False when you want to pass in a complex name.
        /// </param>
        /// <returns>A stream containing the bundle whose name you provided.</returns>
        public static Stream getBundle(string name, bool appendPrefix = true)
        {
            var fullName = appendPrefix ? $"rose.row.assets.{name}" : name;
            Debug.Log($"Created bundle stream with full name: {fullName}");
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(fullName);
        }
    }
}
