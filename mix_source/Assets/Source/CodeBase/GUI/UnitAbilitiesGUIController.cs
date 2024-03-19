using System.Collections.Generic;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Units;
using TMPro;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI
{
    public sealed class UnitAbilitiesGUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text infoText;
        
        private readonly List<AbilityButton> _buttons = new();
        private AbilityButton lastSelectedButton;
        
        private Unit current;

        private void Start()
        {
            _buttons.AddRange(GetComponentsInChildren<AbilityButton>());
                
            SignalManager.SubscribeOnSignal<UnitDead>(OnUnitDead);
            SignalManager.SubscribeOnSignal<UnitAbilityUsed>(OnAbilityUsed);
        }

        private void OnAbilityUsed(UnitAbilityUsed signal)
        {
            if(signal.User is PlayerUnit)
            {
                lastSelectedButton.SelectAbility();
            }
        }

        private void OnUnitDead(UnitDead unit)
        {
            if(unit.Unit == current)
                DisableAll();
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
                infoText.text = "";
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

            infoText.text = ability.Data.Description;
            
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
            
            for(int i = 0; i < _buttons.Count; i++)
            {
                if (i < current.NonTypedAbilitiesPull.Length)
                {
                    _buttons[i].UpdateAbilityData(current.NonTypedAbilitiesPull[i], this);
                    _buttons[i].Enable();    
                }
                else
                {
                    _buttons[i].Disable();
                }
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