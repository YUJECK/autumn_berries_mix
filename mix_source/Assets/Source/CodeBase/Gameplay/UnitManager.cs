using System;
using System.Collections.Generic;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.Units;

namespace autumn_berries_mix.Source.CodeBase.Gameplay
{
    public class UnitManager
    {
        public PlayerUnit[] PlayerUnitsPull => _playerUnitsPull.ToArray();
        public EnemyUnit[] EnemyUnitsPull => _enemyUnitsPull.ToArray();

        private readonly List<PlayerUnit> _playerUnitsPull = new();
        private readonly List<EnemyUnit> _enemyUnitsPull = new();

        public event Action<Unit> OnAppeared;
        public event Action<Unit> OnDisappear;

        public void AddUnits(Unit[] units)
        {
            for (int i = 0; i < units.Length; i++)
            {
                AddUnit(units[i]);
            }
        }
        
        public void AddUnit(Unit unit)
        {
            if(unit == null)
                return;
            
            switch (unit)
            {
                case PlayerUnit playerUnit:
                    _playerUnitsPull.Add(playerUnit);
                    break;
                case EnemyUnit enemyUnit:
                    _enemyUnitsPull.Add(enemyUnit);
                    break;
            }
            
            SignalManager.PushSignal(new UnitSpawned(unit));
            OnAppeared?.Invoke(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            if(unit == null)
                return;
            
            switch (unit)
            {
                case PlayerUnit playerUnit:
                    _playerUnitsPull.Remove(playerUnit);
                    break;
                case EnemyUnit enemyUnit:
                    _enemyUnitsPull.Remove(enemyUnit);
                    break;
            }
            
            SignalManager.PushSignal(new UnitDead(unit));
            OnDisappear?.Invoke(unit);
        }
    }
}