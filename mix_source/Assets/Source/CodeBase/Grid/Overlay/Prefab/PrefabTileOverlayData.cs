using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class PrefabTileOverlayData : TileOverlayData
    {
        public readonly TileOverlayPrefab Prefab;
        
        public PrefabTileOverlayData(TileOverlayPrefab prefab, string name, Vector2 offset, float rotation, int orderInLayer = 1) : base(name, offset, rotation, orderInLayer)
        {
            Prefab = prefab;
        }
    }
}