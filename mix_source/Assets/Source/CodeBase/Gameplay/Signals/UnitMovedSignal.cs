using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay.Signals
{
    public class UnitMovedSignal : Signal
    {
        public readonly Unit Unit;

        public UnitMovedSignal(Unit unit)
        {
            Unit = unit;
        }
    }
}