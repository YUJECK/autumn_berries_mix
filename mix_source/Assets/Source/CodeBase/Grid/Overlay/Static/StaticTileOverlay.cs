using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class StaticTileOverlay : TileOverlay<StaticTileOverlayData>
    {
        public StaticTileOverlay(SpriteRenderer spriteRenderer, GridTile owner)
            : base(owner)
        {
            SpriteRenderer = spriteRenderer;
            
            FinishConstructor();
        }

        public override void ApplyData(StaticTileOverlayData data)
        {
            base.ApplyData(data);
            SpriteRenderer.sprite = data.IdleSprite;
        }

        public override void OnPointed()
        {
            SpriteRenderer.sprite = Data.IdleSprite;
        }

        public override void OnUnpointed()
        {
            SpriteRenderer.sprite = Data.PointedSprite;
        }
    }
}