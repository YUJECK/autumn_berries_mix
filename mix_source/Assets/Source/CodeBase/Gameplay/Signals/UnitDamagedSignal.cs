using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay.Signals
{
    public class UnitDamagedSignal : Signal
    {
        public readonly Unit Unit;
        public readonly int DamagePoints;

        public UnitDamagedSignal(Unit unit, int damagePoints)
        {
            Unit = unit;
            this.DamagePoints = damagePoints;
        }
    }
}