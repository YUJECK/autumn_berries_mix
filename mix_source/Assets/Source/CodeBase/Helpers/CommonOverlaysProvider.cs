using autumn_berries_mix.Grid;
using UnityEngine;

namespace autumn_berries_mix.Helpers
{
    public static class CommonOverlaysProvider
    {
        public static TileOverlayPrefab TileToAttackPrefab { get; private set; }
        public static TileOverlayPrefab DirectionArrowPrefab { get; private set; }
        
        static CommonOverlaysProvider()
        {
            TileToAttackPrefab = Resources.Load<TileOverlayPrefab>("Overlay/AttackArea");
            DirectionArrowPrefab = Resources.Load<TileOverlayPrefab>("Overlay/MovementArrow");
        }
    }
}