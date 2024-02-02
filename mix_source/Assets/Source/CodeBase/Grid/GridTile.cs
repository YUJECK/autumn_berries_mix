using System.Collections.Generic;
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

        public readonly TileOverlayController Overlay = new();

        [field: ReadOnly, SerializeField] public Entity TileStuff { get; private set; }
        
        public virtual void OnPointed() {  }
        public virtual void OnUnpointed() { }

        public GridTile Clear()
        {
            if(TileStuff != null)
                Destroy(TileStuff.gameObject);
            
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