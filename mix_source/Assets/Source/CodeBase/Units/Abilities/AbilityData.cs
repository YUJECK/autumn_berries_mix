using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace autumn_berries_mix.Units
{
    [Serializable]
    public class AbilityData
    {
        public string Name;
        [TextArea(3, 32)] public string Description;
        public Sprite DefaultIcon;
        public Sprite SelectedIcon;

        public int Cost = 1;
    }
}