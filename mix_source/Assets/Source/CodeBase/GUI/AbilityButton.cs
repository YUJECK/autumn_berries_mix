using autumn_berries_mix.Units;
using UnityEngine;
using UnityEngine.UI;

namespace autumn_berries_mix.PrefabTags.CodeBase.GUI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class AbilityButton : MonoBehaviour
    {
        public UnitAbility CurrentAbility { get; private set; }

        private Image _abilityIcon;
        private Button _button;
        private UnitAbilitiesGUIController _controller;

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

        public void UpdateAbilityData(UnitAbility connectedAbility, UnitAbilitiesGUIController controller)
        {
            _controller = controller;
            
            CurrentAbility = connectedAbility;

            _abilityIcon.sprite = connectedAbility.Data.DefaultIcon;
            gameObject.name = connectedAbility.Data.Name;
        }

        public void SelectAbility()
        {
            _controller.SelectAbility(CurrentAbility, this);
            _abilityIcon.sprite = CurrentAbility.Data.SelectedIcon;
        }

        public void DeselectAbility()
        {
            _abilityIcon.sprite = CurrentAbility.Data.DefaultIcon;
        }
    }
}