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
        public override void OnUnitPointed(Unit unit, bool withClick)
        {
            if (Owner.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y).Contains(Owner.Grid.Get(unit.Position2Int.x, unit.Position2Int.y)))
            {
                if (withClick)
                {
                    _typedData.Animator.PlayAttack();
                    unit.UnitHealth.Hit(_typedData.Damage);                  
                   
                    Owner.OnUsedAbility(this);
                }
            }    
        }
    }
}