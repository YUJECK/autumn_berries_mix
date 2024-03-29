using System;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.EC;
using autumn_berries_mix.Gameplay.Signals;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public abstract class Unit : Entity
    {
        [field: Header("Info")]
        [field: SerializeField] public string UnitName { get; protected set; }
        
        public event Action<UnitAbility> UsedAbility; 
        public event Action OnFinished; 
        
        public abstract UnitAbility[] NonTypedAbilitiesPull { get; }
        public abstract UnitHealth UnitHealth { get; protected set; }
        
        protected virtual void ConfigureAbilities() { }
        protected virtual void OnUnitAwake() {}

        public void FinishTurn()
        {
            OnFinished?.Invoke();
        }
        
        protected void UpdateAbilities()
        {
            foreach (var ability in NonTypedAbilitiesPull)
            {
                ability.Tick();
            }
        }

        protected override void OnDestroyed()
        {
            foreach (var ability in NonTypedAbilitiesPull)
            {
                ability.Dispose();
            }
        }

        public sealed override void LoadedToLevel()
        {
            base.LoadedToLevel();
            
            ConfigureAbilities();
            OnUnitAwake();
        }

        public virtual void OnUsedAbility(UnitAbility ability)
        {
            UsedAbility?.Invoke(ability);
            SignalManager.PushSignal(new UnitAbilityUsed(this, ability));
        }
    }
}