using autumn_berries_mix.Grid;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.Headsman.Code
{
    public sealed class AxeAttack : EnemyAbility
    {
        private readonly int _range;
        private readonly int _damage;

        public AxeAttack(Unit owner, AbilityData data, int range, int damage) : base(owner, data)
        {
            _range = range;
            _damage = damage;
        }
        
        public void Attack(PlayerUnit unit)
        {
            Vector2Int direction = unit.Position2Int - Owner.Position2Int;

            direction = new Vector2Int(Mathf.Clamp(direction.x, -1, 1), Mathf.Clamp(direction.y, -1, 1));

            GridTile current = Owner.Grid.Get(Owner.Position2Int + direction);
            
            for (int i = 0; i < _range; i++)
            {
                current = Owner.Grid.Get(current.Position2Int + direction);

                if (current.TileStuff is PlayerUnit playerUnit)
                {
                    playerUnit.UnitHealth.Hit(_damage);
                }
            }
            
            Owner.OnUsedAbility(this);
        }
    }
}