using rose.row.easy_package.coroutines;

using System;
using System.Collections;
using System.IO;

using UnityEngine;

namespace rose.row.easy_package.asset_bundles
{
    public static class AssetBundles
    {
        /// <summary>
        /// Loads an asset bundle using a given <paramref name="stream" />, and returns the result
        /// with an <see cref="Action{T}"/>, where T is an <see cref="AssetBundleResult"/>.
        /// </summary>
        /// <param name="stream">A <see cref="Stream"/> containing an asset bundle to be loaded.</param>
        /// <param name="result">An <see cref="AssetBundleResult"/> containing the result of the loading operation.</param>
        /// <remarks>
        /// This method requires the <see cref="CoroutineManager"/> to be running in the scene.
        /// </remarks>
        public static void loadFromStream(Stream stream, Action<AssetBundleResult> result)
        {
            if (!CoroutineManager.initialized)
            {
                Debug.LogError($"Cannot run 'AssetBundles.loadFromStream' without an active CoroutineManager running.");
                return;
            }

            CoroutineManager.startCoroutine(_loadFromStream(stream, result));
        }

        private static IEnumerator _loadFromStream(Stream stream, Action<AssetBundleResult> result)
        {
            AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromStreamAsync(stream);
            yield return bundleLoadRequest;

            AssetBundle requestedAssetBundle = bundleLoadRequest.assetBundle;

            bool hasFailed = requestedAssetBundle == null;
            if (hasFailed)
            {
                Debug.LogError($"Failed to load asset bundle at path '{stream}'.");
                result?.Invoke(AssetBundleResult.failure());
                yield break;
            }

            result?.Invoke(AssetBundleResult.success(requestedAssetBundle));
            yield break;
        }
    }
}