using System.IO;
using UnityEngine;

namespace rose.row.easy_package.audio
{
    public class LocalAudioHolder : AudioHolder
    {
        public LocalAudioHolder(string path) : base(path) { }

        private static AudioType getAudioTypeFromFilePath(string path)
        {
            var extension = Path.GetExtension(path).Replace(".", "");

            if (extension == "mp3" || extension == "mp4")
            {
                return AudioType.MPEG;
            }

            if (extension == "wav")
            {
                return AudioType.WAV;
            }

            if (extension == "ogg")
            {
                return AudioType.OGGVORBIS;
            }

            return AudioType.UNKNOWN;
        }

        protected override void loadAudio()
        {
            var type = getAudioTypeFromFilePath(path);

            if (type == AudioType.UNKNOWN)
            {
                Debug.LogError($"Could not load clip at '{path}' because it's extension is invalid or unsupported!");
                return;
            }

            Audio.load(
                path: $"{AudioRegistry.audioRoot}/{path}",
                callback: (AudioClip clip) => Audio.register(path, clip),
                autoRegister: false,
                audioType: type);
        }
    }
}
