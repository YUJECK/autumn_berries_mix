using System;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.Grid;
using autumn_berries_mix.PrefabTags.CodeBase.GUI;
using autumn_berries_mix.Turns;
using Zenject;

namespace autumn_berries_mix.Units
{
    public class UnitSelector : SelectedTileProcessor
    {
        private UnitAbilitiesGUIController _guiController;
        public PlayerUnit PlayerUnit { get; private set; }

        public event Action<PlayerUnit> OnPlayerUnitSelected; 

        [Inject]
        private void Construct(UnitAbilitiesGUIController guiController)
            => _guiController = guiController;

        public override void Disable()
        {
            base.Disable();

            if(PlayerUnit is IOnTileSelected callback)
                callback.OnUnpointed();

            PlayerUnit = null;
            OnPlayerUnitSelected?.Invoke(null);
            _guiController.OnUnitSelected(null);
        }

        public override void ProcessPointedTile(GridTile tile) { }

        public override void ProcessSelectedTile(GridTile tile)
        {
            if(tile.TileStuff == null || !Enabled)
                return;

            if (tile.TileStuff.TryGetComponent(out Unit unit))
            {
                if (unit is PlayerUnit playerUnit)
                {
                    if(PlayerUnit != null) PlayerUnit.SelectedAbility?.OnAbilityDeselected();
                    PlayerUnit = playerUnit;
                    
                    _guiController.OnUnitSelected(PlayerUnit);
                    OnPlayerUnitSelected?.Invoke(playerUnit);
                }
                
                SignalManager.PushSignal(new UnitSelectedSignal(unit));
            }
        }
    }
}