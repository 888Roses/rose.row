using UnityEngine;

namespace rose.row.easy_package.asset_bundles
{
    public class AssetBundleResult
    {
        public enum Result
        {
            Success,
            Failure
        }

        public Result result;
        public AssetBundle bundle;

        public static AssetBundleResult failure()
        {
            return new AssetBundleResult { result = Result.Failure, bundle = null };
        }

        public static AssetBundleResult success(AssetBundle bundle)
        {
            return new AssetBundleResult { result = Result.Success, bundle = bundle };
        }
    }
}
