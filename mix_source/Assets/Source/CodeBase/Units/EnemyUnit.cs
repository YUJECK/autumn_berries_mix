using System.Collections.Generic;

namespace autumn_berries_mix.Units
{
    public abstract class EnemyUnit : Unit
    {
        public UnitAbility SelectedAbility { get; protected set; }
        public override UnitAbility[] NonTypedAbilitiesPull => abilitiesPull.ToArray();
        public PlayerAbility[] PlayerAbilitiesPull => abilitiesPull.ToArray();

        private readonly List<PlayerAbility> abilitiesPull = new();
        
    }
}