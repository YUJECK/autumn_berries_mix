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

        private readonly List<TileOverlay> overlaysPull = new();
        private readonly Dictionary<string, TileOverlay> overlays = new();

        [field: ReadOnly, SerializeField] public Entity TileStuff { get; private set; }

        protected virtual void Awake()
        {
            GenerateOverlayObjects();
        }
        
        public virtual void OnSelected() { }
        public virtual void OnDeselected() { }

        public GridTile Clear()
        {
            if(TileStuff != null)
                Destroy(TileStuff.gameObject);

            foreach (var overlay in overlaysPull)
            {
                overlay.Disable();
            }
            
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
            for (int i = 0; i < 4; i++)
            {
                var overlayObject = new GameObject(gameObject.name + "Overlay");
                var spriteRenderer = overlayObject.AddComponent<SpriteRenderer>();
                
                overlaysPull.Add(new TileOverlay(spriteRenderer, this));
            }
        }

        public TileOverlay GetEmpty()
        {
            foreach (var tileOverlay in overlaysPull)
            {
                if (!tileOverlay.Enabled)
                    return tileOverlay;
            }

            return null;
        }
        
        public GridTile PushOverlay(TileOverlayData data)
        {
            var overlay = GetEmpty();
            
            if (overlays.TryAdd(data.Name, overlay))
            {
                overlay?.ApplyData(data);    
            }

            return this;
        }

        public GridTile RemoveOverlay(TileOverlayData data)
        {
            return RemoveOverlay(data.Name);
        }

        public GridTile RemoveOverlay(string key)
        {
            if (overlays.TryGetValue(key, out var tileOverlay))
            {
                tileOverlay.Disable();
                overlays.Remove(key);
            }
            
            return this;
        }
    }
}