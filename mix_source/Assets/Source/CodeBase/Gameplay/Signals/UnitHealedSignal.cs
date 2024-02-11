using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay.Signals
{
    public class UnitHealedSignal : Signal
    {
        public readonly Unit Unit;
        public readonly int HealPoints;

        public UnitHealedSignal(Unit unit, int heal)
        {
            Unit = unit;
            HealPoints = heal;
        }
    }
}