namespace autumn_berries_mix.Grid
{
    public abstract class SelectedTileProcessor
    {
        public abstract void ProcessPointedTile(GridTile tile);
        
        public abstract void ProcessSelectedTile(GridTile tile);
    }
}