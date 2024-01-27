namespace autumn_berries_mix.Units
{
    public abstract class UnitAbility
    {
        public readonly Unit Owner;

        protected UnitAbility(Unit owner)
        {
            Owner = owner;
        }

        public abstract void Use();
    }
}