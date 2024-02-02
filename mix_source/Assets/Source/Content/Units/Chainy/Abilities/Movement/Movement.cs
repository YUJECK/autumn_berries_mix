using System.Collections.Generic;
using System.Linq;
using autumn_berries_mix.Grid;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public class Movement : PlayerAbility
    {
        private readonly MovementAbilityData _typedData;
        private readonly List<GridTile> _currentOverlayPull = new();

        private const string OverlayKey = "MoveArrow";
        
        public Movement(Unit owner, MovementAbilityData data) 
            : base(owner, data)
        {
            _typedData = data;
        }
        

        private void DrawMoveArrows()
        {
            ClearArrows();

            foreach (var tile in _typedData.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y))
            {
                if(!tile.Empty || !tile.Walkable)
                    continue;
                
                float angle = Mathf.Atan2(tile.Position.y - Owner.Position2Int.y, tile.Position.x - Owner.Position2Int.x) 
                    * Mathf.Rad2Deg - 90;
                
                tile.Overlay.PushOverlay(new TileOverlayData(OverlayKey, _typedData.arrowSprite, Vector2.zero, angle));
                _currentOverlayPull.Add(tile);
            }
        }

        private void ClearArrows()
        {
            foreach (var tile in _currentOverlayPull)
            {
                tile.Overlay.RemoveOverlay(OverlayKey);
            }

            _currentOverlayPull.Clear();
        }

        public override void OnEmptyTilePointed(GridTile tile, bool withClick)
        {
            if (tile.Empty && tile.Walkable)
            {
                if (_typedData.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y).Contains(tile))
                {
                    if (withClick)
                    {
                        _typedData.Grid
                            .SwapEntities(Owner.Position2Int.x, Owner.Position2Int.y, tile.Position.x, tile.Position.y);
                    
                        DrawMoveArrows();    
                        Owner.OnUsedAbility(this);
                    }
                }    
            }
        }
        
        public override void OnAbilitySelected()
        {
            DrawMoveArrows();
        }

        public override void OnAbilityDeselected()
        {
            ClearArrows();
        }
    }
}