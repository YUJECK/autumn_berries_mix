using System.Collections.Generic;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI
{
    public sealed class PlayerUnitAbilitiesGUI : MonoBehaviour
    {
        private readonly List<AbilityButton> _buttons = new();

        private PlayerUnit current;
        
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

        private void DisableAll()
        {
            foreach (var button in _buttons)
            {
                button.Disable();
            }
        }

        private void Connect()
        {
            if (current.PlayerAbilitiesPull.Length > _buttons.Count)
            {
                Debug.LogError("TOO MUCH ABILITIES");
                return;
            }
            
            for(int i = 0; i < current.PlayerAbilitiesPull.Length; i++)
            {
                _buttons[i].UpdateAbilityData(current.PlayerAbilitiesPull[i]);
                _buttons[i].Enable();
            }
        }

        private void Start()
        {
            _buttons.AddRange(GetComponentsInChildren<AbilityButton>());
        }
    }
}