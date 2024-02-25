using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay.Signals
{
    public class UnitSpawned : Signal
    { 
        public readonly Unit Unit;

        public UnitSpawned(Unit unit)
        {
            Unit = unit;
        }
    }
}