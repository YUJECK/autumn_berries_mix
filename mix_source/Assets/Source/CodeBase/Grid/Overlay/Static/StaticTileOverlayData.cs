using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class StaticTileOverlayData : TileOverlayData
    {
        public Sprite IdleSprite;
        public Sprite PointedSprite;
        
        public StaticTileOverlayData(Sprite idle, Sprite pointedSprite, string name, Vector2 offset, float rotation, Color color, int orderInLayer = 0)
            : base(name, offset, rotation, color, orderInLayer)
        {
            IdleSprite = idle;
            PointedSprite = pointedSprite;
        }
        
        public StaticTileOverlayData(Sprite idle, Sprite pointedSprite, string name, Vector2 offset, float rotation)
            : base(name, offset, rotation, Color.white, 0)
        {
            IdleSprite = idle;
            PointedSprite = pointedSprite;
        }
        
        public StaticTileOverlayData(Sprite idle, Sprite pointedSprite, string name, float rotation)
            : base(name, Vector2.zero, rotation, Color.white, 0)
        {
            IdleSprite = idle;
            PointedSprite = pointedSprite;
        }
        
        public StaticTileOverlayData(Sprite idle, Sprite pointedSprite, string name, Color color)
            : base(name, Vector2.zero, 0, color, 0)
        {
            IdleSprite = idle;
            PointedSprite = pointedSprite;
        }


        public StaticTileOverlayData(Sprite idle, Sprite pointedSprite, string name)
            : base(name, Vector2.zero, 0, Color.white, 0)
        {
            IdleSprite = idle;
            PointedSprite = pointedSprite;
        }
    }
}