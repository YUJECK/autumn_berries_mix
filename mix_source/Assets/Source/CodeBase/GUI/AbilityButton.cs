using System;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Turns;
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

        private Turn _currentTurn;

        private void Start()
        {
            _button = GetComponent<Button>();
            _abilityIcon = GetComponent<Image>();
            
            _button.onClick.AddListener(SelectAbility);
            
            SceneSwitcher.OnSceneLoaded += OnLoaded;

            Disable();
        }

        private void OnLoaded(Scene scene, Scene scene1)
        {
            _currentTurn = SceneSwitcher.TryGetGameplayScene().TurnController.CurrentTurn;
            SceneSwitcher.TryGetGameplayScene().TurnController.OnTurnSwitched += OnTurnSwitched;

            SceneSwitcher.OnSceneLoaded -= OnLoaded;
        }

        private void OnTurnSwitched(Turn turn)
        {
            _currentTurn = turn; 
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            CurrentAbility = null;
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
            if (_currentTurn is PlayerTurn playerTurn && playerTurn.Available >= CurrentAbility.Data.Cost)
            {
                _controller.SelectAbility(CurrentAbility, this);
                _abilityIcon.sprite = CurrentAbility.Data.SelectedIcon;    
                
                CurrentAbility.Owner.UsedAbility += OnUsedAbility;
            }
        }

        private void OnUsedAbility(UnitAbility ability)
        {
            if (CurrentAbility is PlayerAbility playerAbility && ability == CurrentAbility && _currentTurn is PlayerTurn playerTurn && CurrentAbility.Data.Cost > playerTurn.Available)
            {
                DeselectAbility();
                playerAbility.OnAbilityDeselected();
            }
        }

        public void DeselectAbility()
        {
            if (CurrentAbility != null)
            {
                _abilityIcon.sprite = CurrentAbility.Data.DefaultIcon;
            }
        }
    }
}