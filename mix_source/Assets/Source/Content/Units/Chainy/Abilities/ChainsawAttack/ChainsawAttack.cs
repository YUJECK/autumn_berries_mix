using System.Linq;

namespace autumn_berries_mix.Units
{
    public class ChainsawAttack : PlayerAbility
    {
        private readonly ChainsawAttackData _typedData;

        public ChainsawAttack(Unit owner, ChainsawAttackData data) : base(owner, data)
        {
            _typedData = data;
        }

        public override bool Use()
        {
            return false;
        }

        public override void OnUnitPointed(Unit unit, bool withClick)
        {
            if (Data.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y).Contains(Data.Grid.Get(unit.Position2Int.x, unit.Position2Int.y)))
            {
                if (withClick)
                {
                    unit.UnitHealth.Hit(_typedData.Damage);                    
                }
            }    
        }
    }
}