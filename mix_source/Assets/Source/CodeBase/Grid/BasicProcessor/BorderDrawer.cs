using Source.Content;
using UnityEngine;

namespace autumn_berries_mix.Grid.BasicProcessor
{
    public sealed class BorderDrawer : SelectedTileProcessor
    {
        private GridTile _prev;
        private readonly GameplayResources _resources;

        private readonly TileOverlayData borderData;

        public BorderDrawer(GameplayResources resources)
        {
            _resources = resources;

            borderData = new TileOverlayData("Selected Cell Border", _resources.borderSprite, Vector2.zero, 0);
        }

        public override void ProcessTile(GridTile tile)
        {
            if (_prev != null)
            {
                _prev.RemoveOverlay(borderData);
            }
            
            if (tile != null)
            {
                tile.PushOverlay(borderData);
                _prev = tile;
            }
        }
    }
}