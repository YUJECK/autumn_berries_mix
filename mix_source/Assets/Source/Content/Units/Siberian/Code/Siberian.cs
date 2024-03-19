using autumn_berries_mix.Helpers;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    [RequireComponent(typeof(UnitHealth))]
    public class Siberian : PlayerUnit
    {
        [SerializeField] private MovementAbilityConfig movement;
        [SerializeField] private SiberianHammerAttackData hammerAttack;
        [SerializeField] private SiberianHammerAttackData pushAttack;
        
        public override UnitHealth UnitHealth { get; protected set; }

        private void PlayHammerAudio()
        {
            AudioPlayer.Play("HammerAttack");
        }
        
        protected override void ConfigureComponents()
        {
            base.ConfigureComponents();
            
            UnitHealth = GetComponent<UnitHealth>();

            Master.Add(new EntityFlipper());
            Master.Add<PlayerUnitAnimator>(new SiberianAnimator());
        }

        protected override void ConfigureAbilities()
        {
            base.ConfigureAbilities();
            
            AbilitiesPull.Add(new PlayerUnitStepMovement(this, movement.Data));
            AbilitiesPull.Add(new SiberianHammer(this, hammerAttack));
            AbilitiesPull.Add(new SiberianPush(this, pushAttack));
        }
    }
}
