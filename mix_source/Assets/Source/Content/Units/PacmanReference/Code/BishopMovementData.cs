using System;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    [Serializable]
    public class BishopMovementData : AbilityData
    {
        [Range(1, 10)] public float speed = 5;
        public int damage;
    }
}