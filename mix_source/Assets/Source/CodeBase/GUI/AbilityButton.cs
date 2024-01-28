using autumn_berries_mix.Scenes;
using autumn_berries_mix.Source.CodeBase.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;
using UnityEngine.UI;

namespace autumn_berries_mix.Source.CodeBase.GUI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class AbilityButton : MonoBehaviour
    {
        private PlayerAbility _currentAbility;
        
        private Image _abilityIcon;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _abilityIcon = GetComponent<Image>();
            
            _button.onClick.AddListener(SelectAbility);
            Disable();
        }

        public void Disable()
        {
            gameObject.SetActive(false);   
        }
        
        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public void UpdateAbilityData(PlayerAbility connectedAbility)
        {
            _currentAbility = connectedAbility;

            _abilityIcon.sprite = connectedAbility.Data.Icon;
            gameObject.name = connectedAbility.Data.Name;
        }
        
        public void SelectAbility()
        {
            if(_currentAbility == null)
                return;

            PlayerUnit unit = _currentAbility.Owner as PlayerUnit; 
            unit.SelectedAbility?.OnAbilityDeselected();
            
            unit .SelectedNonTypedAbility = _currentAbility;
            _currentAbility.OnAbilitySelected();
        }
    }
}