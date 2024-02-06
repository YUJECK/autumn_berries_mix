using System;
using UnityEngine;

namespace autumn_berries_mix.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioInstance : MonoBehaviour
    {
        public AudioSource Source { get; private set; }
        public AudioData AudioData { get; private set; }

        private void Awake()
        {
            Source = GetComponent<AudioSource>();
        }

        public void SetData(AudioData data)
        {
            if(data == null)
                return;

            gameObject.name = data.Name;
            AudioData = data;
            Source.clip = data.Clip;
            
            CommitDataChanges();
        }

        public void CommitDataChanges()
        {
            Source.loop = AudioData.Looped;
            Source.volume = AudioData.Volume;
        }
    }
}