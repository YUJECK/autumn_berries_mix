using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    public sealed class Dumbo : EnemyUnit
    {
        [SerializeField] private BishopMovementConfig bishopMovementConfig;
        private GameplayScene _scene;

        public override UnitHealth UnitHealth { get; protected set; }
        
        protected override void ConfigureAbilities()
        {
            _scene = SceneSwitcher.TryGetGameplayScene();
            bishopMovementConfig.Data.Grid = _scene.GameGrid;
            abilitiesPull.Add(new BishopMovement(this, bishopMovementConfig.Data));
        }

        public override void OnUnitTurn()
        {
            (abilitiesPull[0] as BishopMovement).Move(GetDirection());
        }

        private Vector2Int GetDirection()
        {
            Vector2Int direction = _scene.FindNearestPlayerUnit(this).Position2Int - Position2Int;

            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction.y = Mathf.Clamp(direction.y, -1, 1);

            return direction;
        }
    }
}