using autumn_berries_mix.EC;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlayPrefab : Entity, IOnTileSelected
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void OnPointed()
        {
            
        }

        public virtual void OnUnpointed()
        {
            
        }
    }
}