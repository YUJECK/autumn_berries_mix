using System;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;

namespace autumn_berries_mix.Turns
{
    public class EnemyTurn : Turn
    {
        public EnemyUnit CurrentEnemy => CurrentScene.Units.EnemyUnitsPull[currentEnemy];
        private int currentEnemy = 0;

        private bool currentEnemyFinished;
        
        public override async void Start(Action onCompleted)
        {
            Completed = false;
            
            for (currentEnemy = 0; currentEnemy < CurrentScene.Units.EnemyUnitsPull.Length; currentEnemy++)
            {
                await StartEnemy();
            }

            currentEnemy = 0;

            Complete();
            onCompleted?.Invoke();
        }

        private async UniTask StartEnemy()
        {
            currentEnemyFinished = false;
            CurrentEnemy.OnFinished += OnFinished;
            
            CurrentScene.Units.EnemyUnitsPull[currentEnemy].OnUnitTurn();
            
            while (!currentEnemyFinished)
            {
                await UniTask.WaitForEndOfFrame();
            }
        }

        private void OnFinished()
        {
            currentEnemyFinished = true;
            CurrentEnemy.OnFinished -= OnFinished;
        }

        public override void Complete()
        {
            Completed = true;
        }
    }
}