using System.Collections.Generic;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlayController
    {
        private GridTile owner;
        
        private readonly List<StaticTileOverlay> staticOverlaysPull = new();
        
        private readonly Dictionary<string, StaticTileOverlay> staticOverlays = new();
        private readonly Dictionary<string, PrefabTileOverlay> prefabOverlays = new();

        public TileOverlayController(GridTile owner)
        {
            this.owner = owner;
            GenerateStaticOverlayObjects();
        }

        public void Clear()
        {
            foreach (var overlay in staticOverlaysPull)
            {
                overlay.Disable();
            }
        }

        public void OnPointed()
        {
            foreach (var prefabOverlay in prefabOverlays)
            {
                prefabOverlay.Value.OnPointed();
            }
        }
        public void OnUnpointed()
        {
            foreach (var prefabOverlay in prefabOverlays)
            {
                prefabOverlay.Value.OnUnpointed();
            }
        }
        
        #region StaticOverlays

        private void GenerateStaticOverlayObjects()
        {
            for (int i = 0; i < 4; i++)
            {
                var overlayObject = new GameObject(owner.name + "Overlay");
                var spriteRenderer = overlayObject.AddComponent<SpriteRenderer>();
                overlayObject.transform.parent = owner.transform;
                
                staticOverlaysPull.Add(new StaticTileOverlay(spriteRenderer, owner));
            }
        }

        public StaticTileOverlay GetEmptyStatic()
        {
            foreach (var tileOverlay in staticOverlaysPull)
            {
                if (!tileOverlay.Enabled)
                    return tileOverlay;
            }

            return null;
        }

        public StaticTileOverlay GetStaticOverlay(string key)
        {
            return staticOverlays[key];
        }

        public void PushStaticOverlay(StaticTileOverlayData data)
        {
            var overlay = GetEmptyStatic();
            
            if (staticOverlays.TryAdd(data.Name, overlay))
            {
                overlay?.ApplyData(data);    
            }
        }

        public void RemoveStaticOverlay(TileOverlayData data)
        {
            RemoveStaticOverlay(data.Name);
        }

        public void RemoveStaticOverlay(string key)
        {
            if (staticOverlays.TryGetValue(key, out var tileOverlay))
            {
                tileOverlay.Disable();
                
                staticOverlays.Remove(key);
            }
        }

        #endregion

        #region PrefabOverlays

        public void PushPrefabOverlay(PrefabTileOverlayData prefabData)
        {
            prefabOverlays.TryAdd(prefabData.Name, new PrefabTileOverlay(owner));
            prefabOverlays[prefabData.Name].ApplyData(prefabData);
        }

        public void RemovePrefabOverlay(string key)
        {
            if(!prefabOverlays.ContainsKey(key))
                return;
            
            prefabOverlays[key].Disable();
            
            prefabOverlays.Remove(key);
        }
        
        public void RemovePrefabOverlay(PrefabTileOverlayData prefabData)
        {
            RemovePrefabOverlay(prefabData.Name);
        }
        #endregion
    }
}