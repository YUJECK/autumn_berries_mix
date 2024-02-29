using System;
using System.Threading.Tasks;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;

namespace autumn_berries_mix.Turns
{
    public class EnemyTurn : Turn
    {
        public EnemyUnit CurrentEnemy => CurrentScene.Units.EnemyUnitsPull[currentEnemy];
        private int currentEnemy = 0;

        private Action onCompletedCached;
        
        public override async void Start(Action onCompleted)
        {
            CheckCounter();
            
            Completed = false;
            
            onCompletedCached = onCompleted;
            
            CurrentScene.Units.EnemyUnitsPull[currentEnemy].OnFinished += OnFinished;
            
            await UniTask.Delay(1000);
            
            CurrentScene.Units.EnemyUnitsPull[currentEnemy].OnUnitTurn();
        }

        private void CheckCounter()
        {
            if (currentEnemy >= CurrentScene.Units.EnemyUnitsPull.Length)
                currentEnemy = 0;
        }

        private async void OnFinished()
        {
            CurrentScene.Units.EnemyUnitsPull[currentEnemy].OnFinished -= OnFinished;

            await UniTask.Delay(TimeSpan.FromSeconds(1));
            
            Complete();
        }

        public override void Complete()
        {
            Completed = true;
            currentEnemy++;

            if (currentEnemy >= CurrentScene.Units.EnemyUnitsPull.Length)
                currentEnemy = 0;
            
            onCompletedCached?.Invoke();
        }
    }
}