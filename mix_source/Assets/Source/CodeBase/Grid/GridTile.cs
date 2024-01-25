using System.Collections.Generic;
using autumn_berries_mix.EC;
using NaughtyAttributes;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class GridTile : MonoBehaviour
    {
        public virtual bool Empty => TileStuff == null;
        public virtual bool Walkable { get; protected set; } = true;

        private readonly List<SpriteRenderer> overlays = new();

        [field: ReadOnly, SerializeField] public Entity TileStuff { get; private set; }

        protected virtual void Awake()
        {
            GenerateOverlayObjects();
        }

        public GridTile Clear()
        {
            if(TileStuff != null)
                Destroy(TileStuff.gameObject);

            RemoveOverlay();
            
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

        protected void GenerateOverlayObjects()
        {
            var overlayObject = new GameObject(gameObject.name + "Overlay");
            overlays.Add(overlayObject.AddComponent<SpriteRenderer>());
            overlayObject.transform.position = transform.position;

            RemoveOverlay();
        }
        
        public GridTile PushOverlay(Sprite sprite)
        {
            overlays[0].color = Color.white;
            overlays[0].sprite = sprite;
            
            return this;
        }
        
        public GridTile RemoveOverlay()
        {
            overlays[0].color = Color.clear;
            
            return this;
        }
    }
}