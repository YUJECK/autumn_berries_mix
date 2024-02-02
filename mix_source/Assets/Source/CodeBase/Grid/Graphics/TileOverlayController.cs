using System.Collections.Generic;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlayController
    {

        private GridTile owner;
        
        private readonly List<StaticTileOverlay> overlaysPull = new();
        private readonly List<TileOverlayPrefab> prefabOverlays = new();
        
        private readonly Dictionary<string, StaticTileOverlay> overlays = new();
        
        protected void GenerateOverlayObjects()
        {
            for (int i = 0; i < 4; i++)
            {
                var overlayObject = new GameObject(owner.name + "Overlay");
                var spriteRenderer = overlayObject.AddComponent<SpriteRenderer>();
                overlayObject.transform.parent = owner.transform;
                
                overlaysPull.Add(new StaticTileOverlay(spriteRenderer, owner));
            }
        }

        public void Clear()
        {
            foreach (var overlay in overlaysPull)
            {
                overlay.Disable();
            }
        }
        
        public StaticTileOverlay GetEmpty()
        {
            foreach (var tileOverlay in overlaysPull)
            {
                if (!tileOverlay.Enabled)
                    return tileOverlay;
            }

            return null;
        }

        public StaticTileOverlay GetOverlay(string key)
        {
            return overlays[key];
        }
        
        public void PushOverlay(TileOverlayData data)
        {
            var overlay = GetEmpty();
            
            if (overlays.TryAdd(data.Name, overlay))
            {
                overlay?.ApplyData(data);    
            }
        }
        
        public void PushPrefabAsOverlay(TileOverlayPrefab prefab, string key)
        {
            prefabOverlays.Add(prefab);
            prefab.transform.position = owner.transform.position;
            prefab.transform.parent = owner.transform;
        }

        public void RemoveOverlay(TileOverlayData data)
        {
            RemoveOverlay(data.Name);
        }

        public void RemoveOverlay(string key)
        {
            if (overlays.TryGetValue(key, out var tileOverlay))
            {
                tileOverlay.Disable();
                overlays.Remove(key);
            }
        }
    }
}