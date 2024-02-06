using System;
using System.Collections.Generic;

namespace autumn_berries_mix.Units
{
    public abstract class EnemyUnit : Unit
    {
        public UnitAbility SelectedAbility { get; protected set; }
        public override UnitAbility[] NonTypedAbilitiesPull => _abilitiesPull.ToArray();
        public EnemyAbility[] EnemyAbilitiesPull => _abilitiesPull.ToArray();

        private readonly List<EnemyAbility> _abilitiesPull = new();
        private readonly Dictionary<Type, EnemyAbility> _abilities = new();

        public void PushAbility<TAbility>(TAbility ability)
            where TAbility : EnemyAbility
        {
            _abilitiesPull.Add(ability);
            _abilities.Add(typeof(TAbility), ability);
        }
        
        public void RemoveAbility<TAbility>(TAbility ability)
            where TAbility : EnemyAbility
        {
            _abilitiesPull.Remove(ability);
            _abilities.Remove(typeof(TAbility));
        }
        
        public TAbility GetAbility<TAbility>()
            where TAbility : EnemyAbility
        {
            return _abilities[typeof(TAbility)] as TAbility;
        }

        public abstract void OnUnitTurn();

    }
}