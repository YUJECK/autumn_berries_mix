using autumn_berries_mix.EC;

namespace autumn_berries_mix.Units
{
    public abstract class Unit : Entity
    {
        public abstract UnitAbility[] AbilitiesPull { get; }
    }
}