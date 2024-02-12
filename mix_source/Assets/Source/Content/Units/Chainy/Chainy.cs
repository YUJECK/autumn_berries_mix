using autumn_berries_mix.Helpers;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [RequireComponent(typeof(UnitHealth))]
    [RequireComponent(typeof(Animator))]
    public sealed class Chainy : PlayerUnit
    {
        [SerializeField] private MovementAbilityConfig movementConfig;
        [SerializeField] private ChainsawAttackConfig attackConfig;

        private ChainyAnimator _animator;
        
        public override UnitHealth UnitHealth { get; protected set; }

        protected override void ConfigureComponents()
        {
            Master.Add(new EntityFlipper());
        }

        protected override void ConfigureAbilities()
        {
            movementConfig = Instantiate(movementConfig);
            attackConfig = Instantiate(attackConfig);
            
            AbilitiesPull.Add(new PlayerMovement(this, movementConfig.Data));
            AbilitiesPull.Add(new ChainsawAttack(this, attackConfig.data));
        }
        
        protected override void OnUnitAwake()
        {
            UnitHealth = GetComponent<UnitHealth>();
            
            _animator = new ChainyAnimator(GetComponent<Animator>());
            
            movementConfig.Data.Animator = _animator;
            attackConfig.data.Animator = _animator;
        }

        protected override void OnUpdate()
        {
            UpdateAbilities();
        }
    }
}
