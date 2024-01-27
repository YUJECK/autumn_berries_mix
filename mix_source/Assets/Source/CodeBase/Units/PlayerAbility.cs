using autumn_berries_mix.EC;
using autumn_berries_mix.Grid;

namespace autumn_berries_mix.Units
{
    public abstract class PlayerAbility : UnitAbility
    {
        protected PlayerAbility(Unit owner, AbilityData data) : base(owner, data)
        {
            
        }
        
        public virtual void OnAbilitySelected() {}
        public virtual void OnEmptyTilePointed(GridTile tile, bool withClick) {}
        public virtual void OnTilePointed(GridTile tile, bool withClick) {}
        public virtual void OnEnemyUnitPointed(EnemyUnit enemyUnit, bool withClick) {}
        public virtual void OnUnitPointed(Unit unit, bool withClick) {}
        public virtual void OnPlayerUnitPointed(PlayerUnit playerUnit, bool withClick) {}
        public virtual void OnEntityPointed(Entity tileTileStuff, bool withClick) {}
    }
}