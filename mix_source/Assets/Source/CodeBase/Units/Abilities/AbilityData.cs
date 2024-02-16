using System;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [Serializable]
    public class AbilityData
    {
        public string Name;
        [TextArea(3, 32)] public string Description;
        public Sprite DefaultIcon;
        public Sprite SelectedIcon;
    }
}