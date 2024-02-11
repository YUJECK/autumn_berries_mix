using System;
using System.Collections.Generic;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Units;

namespace autumn_berries_mix.Turns
{
    public sealed class TurnController
    {
        public event Action<Turn> OnTurnSwitched; 
        public Turn CurrentTurn => turns[currentTurn];
        
        private readonly List<Turn> turns = new();
        private int currentTurn = -1;

        private readonly GameplayScene currentScene;
        private readonly List<ITurnAddicted> _turnAddicted = new List<ITurnAddicted>();

        public TurnController(GameplayScene scene, params Turn[] turns)
        {
            this.turns.AddRange(turns);
            currentScene = scene;

            foreach (var turn in turns)
            {
                turn.Initialize(currentScene);
            }
        }

        public void RegisterAddiction(ITurnAddicted addicted)
        {
            _turnAddicted.Add(addicted);
        }

        public void SwitchToNext()
        {
            if(currentTurn > 0 && !CurrentTurn.Completed)
                return;

            currentTurn++;
            
            if(currentTurn >= turns.Count)
                currentTurn = 0;
            
            InvokeTurnSwitched();
            CurrentTurn.Start(SwitchToNext);
        }

        private void InvokeTurnSwitched()
        {
            OnTurnSwitched?.Invoke(CurrentTurn);

            foreach (var addicted in _turnAddicted)
            {
                switch (CurrentTurn)
                {
                    case PlayerTurn playerTurn:
                        addicted.OnPlayerTurn(playerTurn);
                        break;
                    case EnemyTurn enemyTurn:
                        addicted.OnEnemyTurn(enemyTurn);
                        break;
                }
            }
        }
    }
}