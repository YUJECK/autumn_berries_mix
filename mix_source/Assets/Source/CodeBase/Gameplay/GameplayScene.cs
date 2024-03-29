using System;
using System.Collections.Generic;
using autumn_berries_mix.Gameplay;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Source.CodeBase.Gameplay;
using autumn_berries_mix.Turns;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.Scenes
{
    public abstract class GameplayScene : Scene
    {
        public abstract PlayerUnit SelectedPlayerUnit { get; }

        public readonly UnitManager Units = new UnitManager();
        public abstract GameGrid GameGrid { get; }
        public override GameObjectFabric Fabric { get; protected set; }

        public TurnController TurnController { get; protected set; }
        public ResultManager ResultManager { get; protected set; } = new ResultManager();

        protected readonly List<GameplayProcessor> GameplayProcessors = new();

        public event Action OnConfiguringFinished;
        
        public PlayerUnit FindNearestPlayerUnit(Unit to)
        {
            PlayerUnit nearest = Units.PlayerUnitsPull[0];
            
            foreach (var playerUnit in Units.PlayerUnitsPull)
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
            GameplayProcessors.ForEach((processor) => processor.Start());
            OnConfiguringFinished?.Invoke();
        }
    }
}