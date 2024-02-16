using System.Collections.Generic;
using UnityEngine;

namespace autumn_berries_mix.Sounds
{
    public static class AudioPlayer
    {
        private static readonly AudioContainer GlobalContainer;
        
        private static readonly List<AudioContainer> TempContainers;

        static AudioPlayer()
        {
            GlobalContainer = CreateNewContainer();
            GlobalContainer.gameObject.name = "GLOBAL AUDIO CONTAINER";
            
            GameObject.DontDestroyOnLoad(GlobalContainer);
        }

        private static AudioContainer CreateNewContainer()
        {
            var containerGameObject = new GameObject();
            return containerGameObject.AddComponent<AudioContainer>();
        }

        public static void CreateGlobalFromPreset(AudioPlayerPreset preset)
        {
            if (GlobalContainer.AudiosCount > 0)
            {
#if DEBUG
                Debug.LogError("GLOBAL PRESET IS ALREADY INITIALIZED");
#endif  
                return;
            }
            
            foreach (var audioData in preset.audios)
            {
                GlobalContainer.Push(audioData);
            }
        }

        public static void Play(string audioName)
        {
            if (GlobalContainer.TryGet(audioName, out AudioInstance instance))
            {
                instance.Source.Play();
                return;
            }
#if DEBUG
            Debug.LogError($"NO AUDIO WITH NAME {audioName}");    
#endif    
        }

        public static void Stop(string audioName)
        {
            if (GlobalContainer.TryGet(audioName, out AudioInstance instance))
            {
                instance.Source.Stop();
                return;
            }
#if DEBUG
            Debug.LogError($"NO AUDIO WITH NAME {audioName}");    
#endif    
        }
        
        public static void Pause(string audioName)
        {
            if (GlobalContainer.TryGet(audioName, out AudioInstance instance))
            {
                instance.Source.Pause();
                return;
            }
#if DEBUG
            Debug.LogError($"NO AUDIO WITH NAME {audioName}");    
#endif    
        }

        public static bool IsPlaying(string audioName)
        {
            if (GlobalContainer.TryGet(audioName, out AudioInstance instance))
            {
                return instance.Source.isPlaying;
            }
#if DEBUG
            Debug.LogError($"NO AUDIO WITH NAME {audioName}");    
#endif
            return false;
        }

        public static float GetLength(string audioName)
        {
            if (GlobalContainer.TryGet(audioName, out AudioInstance instance))
            {
                return instance.AudioData.Clip.length;
            }
#if DEBUG
            Debug.LogError($"NO AUDIO WITH NAME {audioName}");    
#endif
            return 0;
        }

        public static void StopAllNonCrossScene()
        {
            GlobalContainer.ForEach((audio) => audio.Source.Stop());
        }
    }
}