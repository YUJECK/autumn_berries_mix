using System.Collections.Generic;

namespace autumn_berries_mix.Units
{
    public abstract class PlayerUnit : Unit
    {
        public UnitAbility SelectedAbility { get; protected set; }
        public override UnitAbility[] NonTypedAbilitiesPull => abilitiesPull.ToArray();
        public PlayerAbility[] PlayerAbilitiesPull => abilitiesPull.ToArray();

        protected readonly List<PlayerAbility> abilitiesPull = new();
    }
}