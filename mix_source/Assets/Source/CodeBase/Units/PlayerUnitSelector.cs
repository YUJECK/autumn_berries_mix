using autumn_berries_mix.Grid;
using autumn_berries_mix.Source.CodeBase.GUI;
using Zenject;

namespace autumn_berries_mix.Units
{
    public class PlayerUnitSelector : SelectedTileProcessor
    {
        private PlayerUnitAbilitiesGUI _gui;
        public PlayerUnit PlayerUnit { get; private set; }

        [Inject]
        private void Construct(PlayerUnitAbilitiesGUI gui)
            => _gui = gui;

        public override void ProcessPointedTile(GridTile tile) { }
        
        public override void ProcessSelectedTile(GridTile tile)
        {
            if(tile.TileStuff == null)
                return;
            
            if (tile.TileStuff.TryGetComponent(out PlayerUnit playerUnit))
            {
                PlayerUnit = playerUnit;
                _gui.OnUnitSelected(PlayerUnit);
            }
            // else
            // {
            //     PlayerUnit = null;
            //     _gui.OnUnitSelected(null);
            // }
        }
    }
}