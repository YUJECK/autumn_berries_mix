using autumn_berries_mix.Units;

namespace autumn_berries_mix.Source.Content.Units.WalkingSkull
{
    public sealed class Skull : EnemyUnit
    {
        public override UnitHealth UnitHealth { get; protected set; }
        public override void OnUnitTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}