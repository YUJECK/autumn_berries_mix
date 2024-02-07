using System.Collections.Generic;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI
{
    public sealed class UnitAbilitiesGUIController : MonoBehaviour
    {
        private readonly List<AbilityButton> _buttons = new();
        private AbilityButton lastSelectedButton;

        private Unit current;
        
        public void OnUnitSelected(PlayerUnit unit)
        {
            if (unit == null)
            {
                current = null;
                DisableAll();
                
                return;
            }
            
            current = unit;
            Connect();
        }

        public void SelectAbility(UnitAbility ability, AbilityButton abilityButton)
        {
            if(ability == null || current == null)
                return;
            
            if(lastSelectedButton != null)  
                lastSelectedButton.DeselectAbility();
            
            if (current is PlayerUnit playerUnit && ability is PlayerAbility playerAbility)
            {
                playerUnit.SelectedAbility?.OnAbilityDeselected();

                playerUnit.SelectedNonTypedAbility = playerAbility;
                playerAbility.OnAbilitySelected();
            }
            else
            {
                //doing some info display
            }
            
            lastSelectedButton = abilityButton;
            AudioPlayer.Play("TileLocked");
        }

        private void DisableAll()
        {
            foreach (var button in _buttons)
            {
                button.Disable();
            }
        }

        private void Connect()
        {
            if (current.NonTypedAbilitiesPull.Length > _buttons.Count)
            {
                Debug.LogError("TOO MUCH ABILITIES");
                return;
            }
            
            for(int i = 0; i < current.NonTypedAbilitiesPull.Length; i++)
            {
                _buttons[i].UpdateAbilityData(current.NonTypedAbilitiesPull[i], this);
                _buttons[i].Enable();
            }
        }

        private void Start()
        {
            _buttons.AddRange(GetComponentsInChildren<AbilityButton>());
        }
    }
}