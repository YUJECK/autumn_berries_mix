using System;
using autumn_berries_mix.Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace autumn_berries_mix.Units.Abilities.Roll
{
    [Serializable]
    public class RollData : AbilityData
    {
        public Vector2Int[] directions;
        public int range;
        public TileOverlayPrefab overlay;
    }
}