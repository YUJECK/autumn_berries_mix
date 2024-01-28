using autumn_berries_mix.Scenes;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [RequireComponent(typeof(UnitHealth))]
    public sealed class Chainy : PlayerUnit
    {
        [SerializeField] private MovementAbilityConfig movementConfig;
        [SerializeField] private ChainsawAttackConfig attackConfig;

        public override UnitHealth UnitHealth { get; protected set; }

        protected override void ConfigureAbilities()
        {
            movementConfig.Data.Grid = attackConfig.data.Grid = SceneSwitcher.TryGetGameplayScene().GameGrid;
            AbilitiesPull.Add(new Movement(this, movementConfig.Data));
            
            attackConfig.data.Grid = SceneSwitcher.TryGetGameplayScene().GameGrid;
            AbilitiesPull.Add(new ChainsawAttack(this, attackConfig.data));
        }

        protected override void OnUnitAwake()
        {
            UnitHealth = GetComponent<UnitHealth>();
        }

        protected override void OnUpdate()
        {
            UpdateAbilities();
        }
    }
}
