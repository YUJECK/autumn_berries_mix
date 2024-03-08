using System;
using System.Collections.Generic;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    [Serializable]
    public class ChainAttackData : AbilityData
    {
        [Header("Ability Configuration")]
        public List<Vector2Int> directions;
        public int range;
        
        [Header("Overlay Data")]
        public Sprite rangeCell;
        public Sprite selectedRangeCell;
    }
}