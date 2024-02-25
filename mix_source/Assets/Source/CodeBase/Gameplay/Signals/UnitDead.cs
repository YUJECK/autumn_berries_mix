using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay.Signals
{
    public class UnitDead : Signal
    {
        public readonly Unit Unit;

        public UnitDead(Unit unit)
        {
            Unit = unit;
        }
    }
}