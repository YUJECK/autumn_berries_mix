using System;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;

namespace autumn_berries_mix.Source.CodeBase.Scenes
{
    public abstract class GameplayScene : Scene
    {
        public abstract PlayerUnit SelectedPlayerUnit { get; }
        public abstract PlayerUnit[] PlayerUnitsPull { get; }
        public abstract EnemyUnit[] EnemyUnitsPull { get; }
        public abstract GameGrid GameGrid { get; }

        public event Action OnConfiguringFinished;

        protected virtual void InvokeOnConfiguringFinished()
        {
            OnConfiguringFinished?.Invoke();
        }
    }
}