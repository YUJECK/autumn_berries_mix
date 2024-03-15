using System;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Turns;
using autumn_berries_mix.Units;
using TMPro;
using UnityEngine;

namespace autumn_berries_mix.Source.CodeBase.GUI
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class AwaibleCurrency : MonoBehaviour, ITurnAddicted
    {
        private TMP_Text _counter;
        private PlayerTurn currentTurn;
        
        private void Start()
        {
            _counter = GetComponent<TMP_Text>();
            
            SceneSwitcher.OnSceneLoaded += OnLoaded;
        }

        private void OnLoaded(Scene arg1, Scene arg2)
        {
            SignalManager.SubscribeOnSignal<UnitAbilityUsed>(OnAbilityUsed);
            SceneSwitcher.TryGetGameplayScene().TurnController.RegisterAddiction(this);
            currentTurn = SceneSwitcher.TryGetGameplayScene().TurnController.CurrentTurn as PlayerTurn;
            
            SceneSwitcher.OnSceneLoaded -= OnLoaded;
        }

        private void OnAbilityUsed(UnitAbilityUsed ability)
        {
            if (ability.Used is PlayerAbility)
            {
                _counter.text = currentTurn.Available.ToString();
            }
        }

        public void OnPlayerTurn(PlayerTurn turn)
        {
            _counter.text = currentTurn.Available.ToString();
            currentTurn = turn;
        }

        public void OnEnemyTurn(EnemyTurn turn)
        {
            
        }
    }
}