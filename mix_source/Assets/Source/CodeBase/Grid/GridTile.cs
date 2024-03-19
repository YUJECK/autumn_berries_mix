using autumn_berries_mix.EC;
using NaughtyAttributes;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public abstract class GridTile : MonoBehaviour, IOnTileSelected
    {
        public virtual bool Empty => TileStuff == null;
        public abstract bool Walkable { get; }
        
        public Vector2Int Position2Int => new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

        public TileOverlayController Overlay { get; private set; }

        [field: ReadOnly, SerializeField] public Entity TileStuff { get; private set; }

        private void Start()
        {
            Overlay = new TileOverlayController(this);
        }

        public virtual void OnPointed()
        {
            if(Overlay == null)
                Overlay = new TileOverlayController(this);
            
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