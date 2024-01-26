using Source.Content;

namespace autumn_berries_mix.Grid.BasicProcessor
{
    public sealed class BorderDrawer : SelectedTileProcessor
    {
        private GridTile prev;
        private GameplayResources _resources;

        public BorderDrawer(GameplayResources resources)
        {
            _resources = resources;
        }

        public override void ProcessTile(GridTile tile)
        {
            if (prev != null)
            {
                prev.RemoveOverlay();
            }
            
            if (tile != null)
            {
                tile.PushOverlay(_resources.borderSprite);
                prev = tile;
            }
        }
    }
}