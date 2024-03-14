using System;
using autumn_berries_mix.Grid;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [Serializable]
    public sealed class MovementAbilityData : AbilityData
    {
        public TileOverlayPrefab ArrowPrefab;
        public float Speed = 2;
        public Vector2Int[] Directions;
    }
}