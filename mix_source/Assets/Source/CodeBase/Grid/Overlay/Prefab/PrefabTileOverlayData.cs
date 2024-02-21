using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class PrefabTileOverlayData : TileOverlayData
    {
        public readonly TileOverlayPrefab Prefab;
        
        public PrefabTileOverlayData(TileOverlayPrefab prefab, string name, Vector2 offset, float rotation, Color color, int orderInLayer = 1) 
            : base(name, offset, rotation, color, orderInLayer)
        {
            Prefab = prefab;
        }
        
        public PrefabTileOverlayData(TileOverlayPrefab prefab, string name, Vector2 offset) 
            : base(name, offset, 0, Color.white, 0)
        {
            Prefab = prefab;
        }
        
        public PrefabTileOverlayData(TileOverlayPrefab prefab, string name, float rotation) 
            : base(name, Vector2.zero, rotation, Color.white, 0)
        {
            Prefab = prefab;
        }
        
        public PrefabTileOverlayData(TileOverlayPrefab prefab, string name) 
            : base(name, Vector2.zero, 0, Color.white, 0)
        {
            Prefab = prefab;
        }
    }
}