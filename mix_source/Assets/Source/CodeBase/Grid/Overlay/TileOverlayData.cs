using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlayData
    {
        public readonly string Name;
        public Vector3 Offset;
        public int OrderInLayer;
        public float Rotation;
        public Color Color;

        public TileOverlayData(string name, Vector2 offset, float rotation, Color color, int orderInLayer = 1)
        {
            Name = name;
            Offset = new Vector3(offset.x, offset.y, 0);
            Rotation = rotation;
            Color = color;
            OrderInLayer = orderInLayer;
        }
    }
}