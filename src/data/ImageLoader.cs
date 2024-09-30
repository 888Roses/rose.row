using rose.row.util;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace rose.row.data
{
    public static class ImageLoader
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static bool loadImage(string name)
        {
            var path = Path.Combine(ImageRegistry.basePath, name);
            if (TextureUtil.loadTexture(path, out var texture))
            {
                textures.Add(name, texture);
                return true;
            }

            Debug.LogError($"Couldn't load image \"{name}\"");
            return false;
        }
    }
}