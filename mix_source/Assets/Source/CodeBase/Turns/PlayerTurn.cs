using System;
using System.Threading.Tasks;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Turns
{
    public class PlayerTurn : Turn
    {
        public int CurrentUsed { get; private set; }
        public int Awaible => ActionsRange - CurrentUsed;
        
        private const int ActionsRange = 2;
        
        private void OnUnitUsedAbility(UnitAbility ability)
        {
            CurrentUsed += ability.Data.Cost;
        }

        public override async void Start(Action onCompleted)
        {
            SubscribeOnAbilitiesCallbacks();
            Completed = false;
            
            while (CurrentUsed < ActionsRange && !Completed)
            {
                await UniTask.WaitForEndOfFrame();
            }
            
            Complete();
            onCompleted?.Invoke();
        }

        public override void Complete()
        {    
            UnsubscribeOnAbilitiesCallbacks();
            
            CurrentUsed = 0;
            Completed = true;
        }

        private void SubscribeOnAbilitiesCallbacks()
        {
            foreach (var unit in CurrentScene.Units.PlayerUnitsPull)
            {
                unit.UsedAbility += OnUnitUsedAbility;    
            }
        }

        private void UnsubscribeOnAbilitiesCallbacks()
        {
            foreach (var unit in CurrentScene.Units.PlayerUnitsPull)
            {
                unit.UsedAbility -= OnUnitUsedAbility;    
            }
        }
    }
}