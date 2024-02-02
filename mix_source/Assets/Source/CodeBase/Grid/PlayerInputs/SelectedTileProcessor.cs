namespace autumn_berries_mix.Grid
{
    public abstract class SelectedTileProcessor
    {
        public bool Enabled { get; private set; } = true;

        public virtual void Disable()
        {
            Enabled = false;
        }
        
        public void Enable()
        {
            Enabled = true;
        }
        
        public abstract void ProcessPointedTile(GridTile tile);
        
        public abstract void ProcessSelectedTile(GridTile tile);
    }
}