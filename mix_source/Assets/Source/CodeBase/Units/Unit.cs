using autumn_berries_mix.EC;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public abstract class Unit : Entity
    {
        [field: SerializeField] public string UnitName { get; protected set; }
        
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