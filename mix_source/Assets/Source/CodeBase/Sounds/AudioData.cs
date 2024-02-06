using System;
using UnityEngine;

namespace autumn_berries_mix.Sounds
{
    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }
        [field: SerializeField] public bool Looped { get; private set; }
        [field: SerializeField, Range(0, 1)] public float Volume { get; private set; }
        [field: SerializeField] public AudioTags[] Tags { get; private set; }
    }

    public enum AudioTags
    {
        
    }
}