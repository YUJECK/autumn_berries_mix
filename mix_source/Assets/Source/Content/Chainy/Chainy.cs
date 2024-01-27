using autumn_berries_mix.Grid;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Source.CodeBase.Scenes;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public sealed class Chainy : PlayerUnit, IOnTileSelected
    {
        private MovementAbilityData _data;
        
        public void OnSelected()
        {
            Debug.Log("select");
        }

        public void OnDeselected()
        {
            Debug.Log("deselect");
        }

        protected override void ConfigureAbilities()
        {
            _data = new MovementAbilityData((SceneSwitcher.CurrentScene as GameplayScene).GameGrid);
            _data.Name = "Movement";
            
            abilitiesPull.Add(new Movement(this, _data));
            SelectedAbility = abilitiesPull[0];
        }

        protected override void OnUnitAwake()
        {
            
        }
    }
}
