using System.Linq;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Grid.Inputs;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public class Movement : PlayerAbility
    {
        private MovementAbilityData _typedData;
        
        public Movement(Unit owner, MovementAbilityData data) 
            : base(owner, data)
        {
            _typedData = data;
        }

        public override bool Use()
        {
            return false;
        }

        public override void OnEmptyTilePointed(GridTile tile, bool withClick)
        {
            if (withClick)
            {
                if (_typedData.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y).Contains(tile))
                {
                    _typedData.Grid
                        .SwapEntities(Owner.Position2Int.x, Owner.Position2Int.y, tile.Position.x, tile.Position.y);
                }    
            }
        }

        public override void OnAbilitySelected()
        {
            
        }
    }
}