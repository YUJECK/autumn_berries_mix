using System.Collections.Generic;

namespace autumn_berries_mix.Units
{
    public abstract class PlayerUnit : Unit
    {
        public UnitAbility SelectedNonTypedAbility { get; set; }
        public PlayerAbility SelectedAbility => SelectedNonTypedAbility as PlayerAbility;
        public override UnitAbility[] NonTypedAbilitiesPull => AbilitiesPull.ToArray();
        public PlayerAbility[] PlayerAbilitiesPull => AbilitiesPull.ToArray();

        protected readonly List<PlayerAbility> AbilitiesPull = new();
    }
}