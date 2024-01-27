using autumn_berries_mix.EC;

namespace autumn_berries_mix.Units
{
    public abstract class Unit : Entity
    {
        public abstract UnitAbility[] NonTypedAbilitiesPull { get; }    
        
        protected abstract void ConfigureAbilities();
        protected abstract void OnUnitAwake();

        public override void LevelLoaded()
        {
            base.LevelLoaded();
            
            ConfigureAbilities();
            OnUnitAwake();
        }
    }
}