using UnityEngine;

namespace rose.row.data
{
    public class ImageHolder
    {
        public string path;

        public ImageHolder(string path)
        {
            this.path = path;
            ImageLoader.loadImage(path);
            Debug.Log($"Loaded image '{path}'.");
        }

        public Texture2D get()
        {
            return ImageLoader.textures[path];
        }
    }
}
