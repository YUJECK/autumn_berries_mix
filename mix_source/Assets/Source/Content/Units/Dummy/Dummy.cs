using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    [RequireComponent(typeof(UnitHealth))]
    public class Dummy : EnemyUnit
    {
        public override UnitHealth UnitHealth { get; protected set; }
        
        protected override void ConfigureAbilities()
        {
            
        }

        protected override void OnUnitAwake()
        {
            UnitHealth = GetComponent<UnitHealth>();
        }

        public override void OnUnitTurn()
        {
            
            OnUsedAbility(null);
        }
    }
}
