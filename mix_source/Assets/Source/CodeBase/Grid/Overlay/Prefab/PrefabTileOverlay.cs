using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class PrefabTileOverlay : TileOverlay<PrefabTileOverlayData>
    {
        private TileOverlayPrefab _instance;
        public PrefabTileOverlay(GridTile owner) : base(owner) { }
        
        public override void ApplyData(PrefabTileOverlayData data)
        {
            _instance = CreateInstance(data.Prefab);
            SpriteRenderer = _instance.SpriteRenderer;
            
            base.ApplyData(data);
        }

        public TileOverlayPrefab CreateInstance(TileOverlayPrefab prefab)
        {
            return GameObject.Instantiate(prefab, Owner.transform.position, prefab.transform.rotation, Owner.transform);
        }

        public override void OnPointed()
        {
            _instance.OnPointed();
        }

        public override void OnUnpointed()
        {
            _instance.OnUnpointed();
        }
    }
}