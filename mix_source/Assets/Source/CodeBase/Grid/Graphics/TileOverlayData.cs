using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public sealed class TileOverlayData
    {
        public readonly string Name;
        public readonly Sprite Sprite;
        public Vector3 Offset;
        public int OrderInLayer;
        public float Rotation;

        public TileOverlayData(string name, Sprite sprite, Vector2 offset, float rotation, int orderInLayer = 1)
        {
            if (sprite == null)
            {
                Debug.LogError("NULL SPRITE");
            }
            
            Name = name;
            Sprite = sprite;
            Offset = new Vector3(offset.x, offset.y, 0);
            Rotation = rotation;
            OrderInLayer = orderInLayer;
        }
    }
}