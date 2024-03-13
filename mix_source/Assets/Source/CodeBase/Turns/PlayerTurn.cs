using System;
using System.Threading.Tasks;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Turns
{
    public class PlayerTurn : Turn
    {
        private int actionsRange = 2;
        
        private int currentUsed;

        private void OnUnitUsedAbility(UnitAbility ability)
        {
            currentUsed += ability.Data.Cost;
        }

        public override async void Start(Action onCompleted)
        {
            SubscribeOnAbilitiesCallbacks();
            Completed = false;
            
            while (currentUsed < actionsRange && !Completed)
            {
                await UniTask.WaitForEndOfFrame();
            }
            
            Complete();
            onCompleted?.Invoke();
        }

        public override void Complete()
        {    
            UnsubscribeOnAbilitiesCallbacks();
            
            currentUsed = 0;
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