using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay.Signals
{
    public class UnitSelectedSignal : Signal
    {
        public readonly Unit Unit;

        public UnitSelectedSignal(Unit unit)
        {
            Unit = unit;
        }
    }
}