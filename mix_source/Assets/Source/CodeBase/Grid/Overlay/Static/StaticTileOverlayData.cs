using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class StaticTileOverlayData : TileOverlayData
    {
        public Sprite IdleSprite;
        public Sprite PointedSprite;
        
        public StaticTileOverlayData(Sprite idle, Sprite pointedSprite, string name, Vector2 offset, float rotation, int orderInLayer = 0)
            : base(name, offset, rotation, orderInLayer)
        {
            IdleSprite = idle;
            PointedSprite = pointedSprite;
        }
    }
}