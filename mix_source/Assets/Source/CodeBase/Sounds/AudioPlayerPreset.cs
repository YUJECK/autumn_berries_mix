using autumn_berries_mix.Helpers;
using UnityEngine;

namespace autumn_berries_mix.Sounds
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Configs + nameof(AudioPlayerPreset))]
    public class AudioPlayerPreset : ScriptableObject
    {
        public AudioData[] audios;
    }
}