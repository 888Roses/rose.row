using UnityEngine;

namespace rose.row.easy_package.audio
{
    public class AudioHolder
    {
        public string path;

        public AudioHolder(string path)
        {
            this.path = path;
            loadAudio();
            Debug.Log($"Loaded audio file at path '{path}'.");
        }

        protected virtual void loadAudio()
        {
            Audio.load(path);
        }

        public AudioClip get()
        {
            return Audio.registered[path];
        }
    }
}
