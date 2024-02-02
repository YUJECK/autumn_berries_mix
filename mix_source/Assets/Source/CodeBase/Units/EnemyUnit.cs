using System.Collections.Generic;

namespace autumn_berries_mix.Units
{
    public abstract class EnemyUnit : Unit
    {
        public UnitAbility SelectedAbility { get; protected set; }
        public override UnitAbility[] NonTypedAbilitiesPull => abilitiesPull.ToArray();
        public EnemyAbility[] EnemyAbilitiesPull => abilitiesPull.ToArray();

        protected readonly List<EnemyAbility> abilitiesPull = new();

        public abstract void OnUnitTurn();

    }
}