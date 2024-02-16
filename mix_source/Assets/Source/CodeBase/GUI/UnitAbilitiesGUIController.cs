using System.Collections.Generic;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Units;
using TMPro;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI
{
    public sealed class UnitAbilitiesGUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _infoText;
        
        private readonly List<AbilityButton> _buttons = new();
        private AbilityButton lastSelectedButton;
        
        private Unit current;

        private void Start()
        {
            _buttons.AddRange(GetComponentsInChildren<AbilityButton>());
        }

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
            if (ability == null || current == null)
            {
                _infoText.text = "";
                return;
            }
            
            if(lastSelectedButton != null)  
                lastSelectedButton.DeselectAbility();
            
            if (current is PlayerUnit playerUnit && ability is PlayerAbility playerAbility)
            {
                playerUnit.SelectedAbility?.OnAbilityDeselected();

                playerUnit.SelectedNonTypedAbility = playerAbility;
                playerAbility.OnAbilitySelected();
            }

            _infoText.text = ability.Data.Description;
            
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

            if (current is PlayerUnit playerUnit && playerUnit.SelectedAbility != null)
            {
                foreach (var button in _buttons)
                {
                    if(button.CurrentAbility == playerUnit.SelectedAbility)
                        button.SelectAbility();
                }    
            }
        }
    }
}