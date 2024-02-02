using System;
using autumn_berries_mix.Grid;
using autumn_berries_mix.PrefabTags.CodeBase.Inputs;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Turns;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.Scenes
{
    public abstract class GameplayScene : Scene
    {
        public abstract PlayerUnit SelectedPlayerUnit { get; }
        public abstract PlayerUnit[] PlayerUnitsPull { get; }
        public abstract EnemyUnit[] EnemyUnitsPull { get; }
        public abstract GameGrid GameGrid { get; }

        public TurnController TurnController { get; protected set; }

        public GameplayCallbacks Callbacks { get; private set; } = new GameplayCallbacks();

        public event Action OnConfiguringFinished;

        public PlayerUnit FindNearestPlayerUnit(Unit to)
        {
            PlayerUnit nearest = PlayerUnitsPull[0];
            
            foreach (var playerUnit in PlayerUnitsPull)
            {
                if (Vector2Int.Distance(playerUnit.Position2Int, to.Position2Int)
                    < Vector2Int.Distance(nearest.Position2Int, to.Position2Int))
                {
                    nearest = playerUnit;
                }
            }

            return nearest;
        }
        
        protected virtual void InvokeOnConfiguringFinished()
        {
            OnConfiguringFinished?.Invoke();
        }
    }
}