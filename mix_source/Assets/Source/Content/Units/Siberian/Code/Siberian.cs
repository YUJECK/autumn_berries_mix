using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    [RequireComponent(typeof(UnitHealth))]
    public class Siberian : PlayerUnit
    {
        public MovementAbilityConfig movement;
        public override UnitHealth UnitHealth { get; protected set; }

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
        }
    }
}
