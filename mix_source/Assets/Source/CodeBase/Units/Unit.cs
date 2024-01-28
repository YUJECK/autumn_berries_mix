using autumn_berries_mix.EC;

namespace autumn_berries_mix.Units
{
    public abstract class Unit : Entity
    {
        public abstract UnitAbility[] NonTypedAbilitiesPull { get; }
        public abstract UnitHealth UnitHealth { get; protected set; }
        
        protected abstract void ConfigureAbilities();
        protected abstract void OnUnitAwake();

        protected void UpdateAbilities()
        {
            foreach (var ability in NonTypedAbilitiesPull)
            {
                ability.Tick();
            }
        }
        
        public override void LevelLoaded()
        {
            base.LevelLoaded();
            
            ConfigureAbilities();
            OnUnitAwake();
        }
    }
}