using autumn_berries_mix.Helpers;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Units.Abilities.Roll;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [RequireComponent(typeof(UnitHealth))]
    [RequireComponent(typeof(Animator))]
    public sealed class Chainy : PlayerUnit
    {
        [field: Header("")]
        [SerializeField] private MovementAbilityConfig movementConfig;
        [SerializeField] private ChainsawAttackConfig attackConfig;
        [SerializeField] private ChainAttackConfig chainAttackConfig;
        [SerializeField] private RollConfig rollConfig;

        private ChainyAnimator _animator;
        
        public override UnitHealth UnitHealth { get; protected set; }

        protected override void ConfigureComponents()
        {
            Master.Add(new EntityFlipper());
            Master.Add(new ChainyAnimator(GetComponent<Animator>()));
        }

        protected override void ConfigureAbilities()
        {
            movementConfig = Instantiate(movementConfig);
            attackConfig = Instantiate(attackConfig);
            
            AbilitiesPull.Add(new PlayerMovement(this, movementConfig.Data));
            AbilitiesPull.Add(new ChainsawAttack(this, attackConfig.data));
            AbilitiesPull.Add(new ChainAttack(this, chainAttackConfig.Data));
            AbilitiesPull.Add(new ChainyRoll(this, rollConfig.Data));
        }
        
        protected override void OnUnitAwake()
        {
            UnitHealth = GetComponent<UnitHealth>();
        }

        public void PlayChainWhipAudio()
        {
            AudioPlayer.Play("ChainWhip");
        }
        
        protected override void OnUpdate()
        {
            UpdateAbilities();
        }
    }
}
