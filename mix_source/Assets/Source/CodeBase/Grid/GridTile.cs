using autumn_berries_mix.EC;
using NaughtyAttributes;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class GridTile : MonoBehaviour, IOnTileSelected
    {
        public virtual bool Empty => TileStuff == null;
        public virtual bool Walkable { get; protected set; } = true;
        public Vector2Int Position => new Vector2Int((int)transform.position.x, (int)transform.position.y);

        public TileOverlayController Overlay { get; private set; }

        [field: ReadOnly, SerializeField] public Entity TileStuff { get; private set; }

        private void Start()
        {
            Overlay = new TileOverlayController(this);
        }

        public virtual void OnPointed()
        {
            Overlay.OnPointed();
        }

        public virtual void OnUnpointed()
        {
            Overlay.OnUnpointed();
        }

        public GridTile Clear()
        {
            if(TileStuff != null)
                Destroy(TileStuff.gameObject);
            
            Overlay.Clear();
            
            return this;
        }
        
        public GridTile Place(Entity stuff)
        {
            if (stuff == null)
            {
                TileStuff = null;
                
                return this;
            }

            if(TileStuff != null)
                Destroy(TileStuff.gameObject);
            
            TileStuff = stuff;
            TileStuff.transform.position = transform.position;

            return this;
        }

    }
}