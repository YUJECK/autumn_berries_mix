using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay.Signals
{
    public class UnitAbilityUsed : Signal
    {
        public readonly Unit User;
        public readonly UnitAbility Used;

        public UnitAbilityUsed(Unit user, UnitAbility used)
        {
            User = user;
            Used = used;
        }
    }
}