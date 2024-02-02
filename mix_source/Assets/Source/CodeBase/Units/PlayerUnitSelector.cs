using System;
using autumn_berries_mix.Grid;
using autumn_berries_mix.PrefabTags.CodeBase.GUI;
using autumn_berries_mix.Turns;
using Zenject;

namespace autumn_berries_mix.Units
{
    public class PlayerUnitSelector : SelectedTileProcessor, ITurnAddicted
    {
        private PlayerUnitAbilitiesGUI _gui;
        public PlayerUnit PlayerUnit { get; private set; }

        public event Action<PlayerUnit> OnPlayerUnitSelected; 

        [Inject]
        private void Construct(PlayerUnitAbilitiesGUI gui)
            => _gui = gui;

        public void OnPlayerTurn(PlayerTurn turn) => Enable();
        public void OnEnemyTurn(EnemyTurn turn) => Disable();

        public override void Disable()
        {
            base.Disable();

            if(PlayerUnit is IOnTileSelected callback)
                callback.OnUnpointed();

            PlayerUnit = null;
            OnPlayerUnitSelected?.Invoke(null);
            _gui.OnUnitSelected(null);
        }

        public override void ProcessPointedTile(GridTile tile) { }

        public override void ProcessSelectedTile(GridTile tile)
        {
            if(tile.TileStuff == null || !Enabled)
                return;
            
            if (tile.TileStuff.TryGetComponent(out PlayerUnit playerUnit))
            {
                PlayerUnit = playerUnit;
                
                _gui.OnUnitSelected(PlayerUnit);
                OnPlayerUnitSelected?.Invoke(playerUnit);
            }
            // else
            // {
            //     PlayerUnit = null;
            //     _gui.OnUnitSelected(null);
            // }
        }
    }
}