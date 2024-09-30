using System.IO;
using UnityEngine;

namespace rose.row.util
{
    public static class TextureUtil
    {
        public static bool loadTexture(string path, out Texture2D texture)
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning($"Cannot download texture at path '{path}': File not found.");
                texture = null;
                return false;
            }

            texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(path));
            return texture;
        }

        public static Rect getRect(this Texture2D texture) => new Rect(0, 0, texture.width, texture.height);

        public static Vector2 getCenter(this Texture2D texture) => new Vector2(texture.width / 2f, texture.height / 2f);

        public static Sprite toSprite(this Texture2D texture) => Sprite.Create(texture, texture.getRect(), texture.getCenter());

        public static Vector2 getSize(this Texture2D texture) => new Vector2(texture.width, texture.height);
    }
}