using System;
using System.Collections;
using UnityEngine;

namespace rose.row.main_menu.war.war_data.networking
{
    public static class WMS
    {
        public const string k_DefaultLayer = "VIIRS_NOAA20_CorrectedReflectance_TrueColor";
        public static readonly double[] defaultBBox = { -180, -90, 180, 90 };

        public static IEnumerator request(
            Action<Texture2D> callback,
            int width,
            int height,
            double[] bBox,
            string time,
            string projection = "4326",
            string format = "image/jpeg",
            string server = "https://gibs.earthdata.nasa.gov",
            string styles = ""
        )
        {
            yield return request(callback, k_DefaultLayer, width, height, bBox, time, projection, format, server, styles);
        }

        public static IEnumerator request(
            Action<Texture2D> callback,
            string layer,
            int width,
            int height,
            double[] bBox,
            string time,
            string projection = "4326",
            string format = "image/jpeg",
            string server = "https://gibs.earthdata.nasa.gov",
            string styles = ""
        )
        {
            var request = new WMSRequest(
                layer: layer,
                width: width,
                height: height,
                bBox: bBox,
                time: time,
                projection: projection,
                format: format,
                server: server,
                styles: styles
            );

            Texture2D result = new Texture2D(width, height);

            using (var webRequest = new WWW(request.Url))
            {
                yield return webRequest;
                webRequest.LoadImageIntoTexture(result);
                callback?.Invoke(result);
            }
        }
    }
}