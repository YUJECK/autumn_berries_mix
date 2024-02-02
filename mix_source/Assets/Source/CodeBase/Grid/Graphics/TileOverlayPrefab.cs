using autumn_berries_mix.EC;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlayPrefab : Entity
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        public TileOverlayData Data { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}