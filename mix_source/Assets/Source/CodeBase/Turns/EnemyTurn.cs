using System;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Turns
{
    public class EnemyTurn : Turn
    {
        private int currentEnemy = 0;

        private Action onCompletedCached;
        
        public override void Start(Action onCompleted)
        {
            Completed = false;
            
            onCompletedCached = onCompleted;
            
            CurrentScene.EnemyUnitsPull[currentEnemy].UsedAbility += OnUsedAbility;
            CurrentScene.EnemyUnitsPull[currentEnemy].OnUnitTurn();
        }

        private void OnUsedAbility(UnitAbility ability)
        {
            CurrentScene.EnemyUnitsPull[currentEnemy].UsedAbility -= OnUsedAbility;
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