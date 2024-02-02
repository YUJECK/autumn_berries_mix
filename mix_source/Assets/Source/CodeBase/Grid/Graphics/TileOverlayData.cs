using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public sealed class TileOverlayData
    {
        public readonly string Name;
        public Vector3 Offset;
        public int OrderInLayer;
        public float Rotation;

        public TileOverlayData(string name, Vector2 offset, float rotation, int orderInLayer = 1)
        {
            Name = name;
            Offset = new Vector3(offset.x, offset.y, 0);
            Rotation = rotation;
            OrderInLayer = orderInLayer;
        }
    }
}