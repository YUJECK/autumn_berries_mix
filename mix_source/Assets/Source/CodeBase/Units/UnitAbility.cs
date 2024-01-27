using autumn_berries_mix.EC;

namespace autumn_berries_mix.Units
{
    public abstract class UnitAbility
    {
        public readonly Unit Owner;
        public readonly AbilityData Data;

        public UnitAbility(Unit owner, AbilityData data)
        {
            Owner = owner;
            Data = data;
        }

        public abstract bool Use();
    }
}