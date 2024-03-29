using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.MisterWood
{
    [RequireComponent(typeof(UnitHealth))]
    public sealed class MisterWood : PlayerUnit
    {
        public override UnitHealth UnitHealth { get; protected set; }

        protected override void ConfigureComponents()
        {
            base.ConfigureComponents();

            UnitHealth = GetComponent<UnitHealth>();
        }

        protected override void ConfigureAbilities()
        {
            base.ConfigureAbilities();
            
            
        }
    }
}