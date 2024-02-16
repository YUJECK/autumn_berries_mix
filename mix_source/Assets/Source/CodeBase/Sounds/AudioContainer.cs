using System;
using System.Collections.Generic;
using UnityEngine;

namespace autumn_berries_mix.Sounds
{
    public sealed class AudioContainer : MonoBehaviour
    {
        public int AudiosCount => _audios.Count;
        
        private readonly List<AudioInstance> _audios = new();
        
        private readonly Dictionary<string, AudioInstance> _nameToAudio = new();
        private readonly Dictionary<AudioTags, List<AudioInstance>> _tagToAudio = new();
        
        private void Start()
        {
            CreateEmptyTagToAudio();
        }

        public void ForEach(Action<AudioInstance> action)
        {
            _audios.ForEach(action);
        }

        public void Push(AudioData data)
        {
            GameObject audioGameObject = new GameObject();
            audioGameObject.transform.parent = transform;
            AudioInstance audioInstance = audioGameObject.AddComponent<AudioInstance>();
            
            audioInstance.SetData(data);
            
            _audios.Add(audioInstance);
            _nameToAudio.Add(data.Name, audioInstance);
            
            foreach (AudioTags audioTag in data.Tags)
            {
                _tagToAudio[audioTag].Add(audioInstance);    
            }
        }

        public void Remove(string audioName)
        {
            AudioInstance audioInstance = _nameToAudio[audioName];

            _audios.Remove(audioInstance);
            _nameToAudio.Remove(audioName);

            foreach (var audioTag in audioInstance.AudioData.Tags)
            {
                _tagToAudio[audioTag].Remove(audioInstance);
            }
            
            Destroy(audioInstance);
        }
        
        public void Remove(AudioData data)
            => Remove(data.Name);

        public bool TryGet(string audioName, out AudioInstance instance)
        {
            return _nameToAudio.TryGetValue(audioName, out instance);
        }
        
        public AudioInstance Get(string audioName)
        {
            return _nameToAudio[audioName];
        }
        
        public AudioInstance[] Get(AudioTags audiosTag)
        {
            return _tagToAudio[audiosTag].ToArray();
        }

        private void CreateEmptyTagToAudio()
        {
            foreach (AudioTags tag in Enum.GetValues(typeof(AudioTags)))
            {
                _tagToAudio.Add(tag, new List<AudioInstance>());
            }
        }
    }
}