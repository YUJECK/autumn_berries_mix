using System;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Turns
{
    public class EnemyTurn : Turn
    {
        public EnemyUnit CurrentEnemy => CurrentScene.EnemyUnitsPull[currentEnemy];
        private int currentEnemy = 0;

        private Action onCompletedCached;
        
        public override void Start(Action onCompleted)
        {
            CheckCounter();
            
            Completed = false;
            
            onCompletedCached = onCompleted;
            
            CurrentScene.EnemyUnitsPull[currentEnemy].UsedAbility += OnUsedAbility;
            CurrentScene.EnemyUnitsPull[currentEnemy].OnUnitTurn();
        }

        private void CheckCounter()
        {
            if (currentEnemy >= CurrentScene.EnemyUnitsPull.Length)
                currentEnemy = 0;
        }

        private async void OnUsedAbility(UnitAbility ability)
        {
            CurrentScene.EnemyUnitsPull[currentEnemy].UsedAbility -= OnUsedAbility;

            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            Complete();
        }

        public override void Complete()
        {
            Completed = true;
            currentEnemy++;

            if (currentEnemy >= CurrentScene.EnemyUnitsPull.Length)
                currentEnemy = 0;
            
            onCompletedCached?.Invoke();
        }
    }
}