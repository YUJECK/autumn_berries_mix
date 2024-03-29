using autumn_berries_mix.Sounds;
using autumn_berries_mix.Turns;
using Source.Content;
using UnityEngine;

namespace autumn_berries_mix.Grid.BasicProcessor
{
    public sealed class BorderDrawer : SelectedTileProcessor
    {
        private GridTile _prev;
        private GridTile _prevLocked;
        private readonly GameplayResources _resources;

        private readonly StaticTileOverlayData borderData;
        private readonly StaticTileOverlayData lockedBorderData;

        public BorderDrawer(GameplayResources resources)
        {
            _resources = resources;

            borderData = new StaticTileOverlayData(_resources.borderSprite, _resources.borderSprite, "Selected Cell Border", Vector2.zero, 0);
            lockedBorderData = new StaticTileOverlayData(_resources.lockedBorderSprite, _resources.lockedBorderSprite, "Selected Cell Locked Border", Vector2.zero, 0);
        }
        
        public override void Disable()
        {
            base.Disable();

            Reset();
        }

        private void Reset()
        {
            if (_prev != null)
            {
                _prev.Overlay.RemoveStaticOverlay(borderData);
                _prev = null;
            }

            if (_prevLocked != null)
            {
                _prevLocked.Overlay.RemoveStaticOverlay(lockedBorderData);
                _prevLocked = null;
            }
        }

        public override void ProcessPointedTile(GridTile tile)
        {
            if (_prev != null)
            {
                _prev.Overlay.RemoveStaticOverlay(borderData);
            }
            
            if (tile != _prevLocked)
            {
                tile.Overlay.PushStaticOverlay(borderData);
                _prev = tile;
            }
        }

        public override void ProcessSelectedTile(GridTile tile)
        {
            if (_prevLocked != null)
            {
                _prev.Overlay.RemoveStaticOverlay(borderData);
                _prevLocked.Overlay.RemoveStaticOverlay(lockedBorderData);
            }
            
            tile.Overlay.PushStaticOverlay(lockedBorderData);
            _prevLocked = tile;
            AudioPlayer.Play("TileLocked");
        }
    }
}