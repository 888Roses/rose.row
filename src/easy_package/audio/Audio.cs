using rose.row.easy_package.coroutines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace rose.row.easy_package.audio
{
    public class AudioWorker
    {
        public AudioSource audioSource;
        public bool followPoint;
        public Vector3 point;

        public AudioWorker(AudioSource audioSource, bool followPoint, Vector3 point)
        {
            this.audioSource = audioSource;
            this.followPoint = followPoint;
            this.point = point;
        }

        public bool isPlaying => audioSource.isPlaying;
    }

    public class AudioManager : MonoBehaviour
    {
        private void Update()
        {
            Audio.update();
        }
    }

    public static class Audio
    {
        #region loading

        // Responsible for helping to load in sounds as AudioClips at runtime.

        /// <summary>
        /// Every registered clips in the game.
        /// </summary>
        public static Dictionary<string, AudioClip> registered = new Dictionary<string, AudioClip>();

        /// <summary>
        /// Registers a new audio clip with it's path in the <see cref="registered"/> dictionary for easy pickup.
        /// </summary>
        /// <param name="path">The path of the clip to be registered, used as a key in the dictionary.</param>
        /// <param name="clip">The clip to register, used as the value in the dictionary.</param>
        public static void register(string path, AudioClip clip)
        {
            if (registered.ContainsKey(path))
                registered[path] = clip;
            else
                registered.Add(path, clip);
        }

        /// <summary>
        /// <inheritdoc cref="load(string, Action{AudioClip}, bool, AudioType)"/>
        /// </summary>
        /// <param name="path">
        /// <inheritdoc cref="load(string, Action{AudioClip}, bool, AudioType)"/>
        /// </param>
        /// <param name="autoRegister">
        /// <inheritdoc cref="load(string, Action{AudioClip}, bool, AudioType)"/>
        /// </param>
        /// <param name="audioType">
        /// <inheritdoc cref="load(string, Action{AudioClip}, bool, AudioType)"/>
        /// </param>
        public static void load(string path, bool autoRegister = true, AudioType audioType = AudioType.MPEG)
            => load(path, null, autoRegister, audioType);

        /// <summary>
        /// Loads an audio clip at the given path and calls a callback with the loaded audio clip.
        /// </summary>
        /// <param name="path">Where that audio clip is stored on the computer.</param>
        /// <param name="callback">
        /// Called when the audio clip has finished loading in.
        /// In the case of an error, this callback's audio clip will be null.
        /// </param>
        /// <param name="autoRegister">Whether to automatically call <see cref="register"/> with the loaded audio clip or not.</param>
        /// <param name="audioType">The type of audio clip you're trying to convert.</param>
        public static void load(string path,
                                Action<AudioClip> callback,
                                bool autoRegister = true,
                                AudioType audioType = AudioType.MPEG)
        {
            if (autoRegister)
            {
                if (callback == null)
                    callback = (AudioClip audioClip) => register(path, audioClip);
                else
                    callback += (AudioClip audioClip) => register(path, audioClip);
            }

            CoroutineManager.startCoroutine(loadClipEnumerator(path, callback, audioType));
        }

        private static IEnumerator loadClipEnumerator(string path, Action<AudioClip> callback, AudioType audioType)
        {
            AudioClip audioClip = null;

            using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(path, audioType))
            {
                yield return request.SendWebRequest();

                try
                {
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        audioClip = DownloadHandlerAudioClip.GetContent(request);
                    }
                    else
                    {
                        Debug.LogError($"Could not download clip at path '{path}': {request.error} ({request.result})!");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            callback?.Invoke(audioClip);
            yield return null;
        }

        #endregion loading

        #region playing

        public const int k_AudioWorkersCount = 10;

        private static AudioWorker[] _audioWorkers = new AudioWorker[k_AudioWorkersCount];
        private static bool _audioWorkersInitialized;

        private static void initializeAudioWorkers()
        {
            if (_audioWorkersInitialized)
                return;

            _audioWorkersInitialized = true;
            var audioWorkerContainer = new GameObject("Audio Workers");

            for (int i = 0; i < _audioWorkers.Length; i++)
            {
                var workerGameObject = new GameObject($"Audio Worker {i}");
                var worker = workerGameObject.AddComponent<AudioSource>();
                worker.playOnAwake = false;
                worker.spatialBlend = 1f;
                worker.spatialize = true;

                _audioWorkers[i] = new AudioWorker(worker, false, Vector3.zero);
                workerGameObject.transform.SetParent(audioWorkerContainer.transform);
                workerGameObject.transform.localPosition = Vector3.zero;
            }

            GameObject.DontDestroyOnLoad(audioWorkerContainer);
        }

        public static void update()
        {
        }

        private static AudioWorker getFreeWorker()
        {
            foreach (var worker in _audioWorkers)
            {
                if (worker.isPlaying)
                    continue;

                return worker;
            }

            var firstWorker = _audioWorkers[0];
            firstWorker.audioSource.Stop();
            return firstWorker;
        }

        public static void play(AudioClip clip, float volume = 1, float pitch = 1, ulong delay = 0)
        {
            initializeAudioWorkers();

            var worker = getFreeWorker();

            if (worker.isPlaying)
                worker.audioSource.Stop();

            worker.audioSource.transform.position = Vector3.zero;
            worker.audioSource.spatialBlend = 0f;
            worker.audioSource.spatialize = false;

            worker.audioSource.volume = volume;
            worker.audioSource.pitch = pitch;
            worker.audioSource.clip = clip;
            worker.audioSource.Play(delay);
        }

        public static void playOneShot(AudioClip clip, float volume = 1)
        {
            initializeAudioWorkers();

            var worker = getFreeWorker();
            worker.audioSource.PlayOneShot(clip, volume);
        }

        public static void playAtPoint(AudioClip clip, Vector3 point, float volume = 1f, float pitch = 1f, ulong delay = 0)
        {
            initializeAudioWorkers();

            var worker = getFreeWorker();

            if (worker.isPlaying)
                worker.audioSource.Stop();

            worker.audioSource.transform.position = point;
            worker.audioSource.spatialBlend = 1f;
            worker.audioSource.spatialize = true;
            worker.audioSource.volume = volume;
            worker.audioSource.pitch = pitch;
            worker.audioSource.clip = clip;
            worker.audioSource.Play(delay);
        }

        #endregion
    }
}