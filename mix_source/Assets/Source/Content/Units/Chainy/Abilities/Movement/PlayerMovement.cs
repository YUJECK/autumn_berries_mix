using System;
using System.Collections.Generic;
using System.Linq;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public class PlayerMovement : PlayerAbility
    {
        private readonly MovementAbilityData _typedData;
        private readonly List<GridTile> _currentOverlayPull = new();

        private const string OverlayKey = "MoveArrow";
        private bool _currentMoving;

        private EntityFlipper _flipper;
        
        public PlayerMovement(Unit owner, MovementAbilityData data) 
            : base(owner, data)
        {
            _typedData = data;
            _flipper = Owner.Master.Get<EntityFlipper>();
        }
        

        private void DrawMoveArrows()
        {
            ClearArrows();

            foreach (var tile in Owner.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y))
            {
                if(!tile.Empty || !tile.Walkable)
                    continue;
                
                float angle = Mathf.Atan2(tile.Position.y - Owner.Position2Int.y, tile.Position.x - Owner.Position2Int.x) 
                    * Mathf.Rad2Deg - 90;
                
                tile.Overlay.PushPrefabOverlay(new PrefabTileOverlayData(_typedData.ArrowPrefab, OverlayKey, Vector2.zero, angle));
                _currentOverlayPull.Add(tile);
            }
        }

        private void ClearArrows()
        {
            foreach (var tile in _currentOverlayPull)
            {
                tile.Overlay.RemovePrefabOverlay(OverlayKey);
            }

            _currentOverlayPull.Clear();
        }

        public override void OnEmptyTilePointed(GridTile tile, bool withClick)
        {
            if (tile.Empty && tile.Walkable)
            {
                if (Owner.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y).Contains(tile))
                {
                    if (withClick && !_currentMoving)
                    {
                        Move(tile.Position);
                    }
                }    
            }
        }

        private async void Move(Vector2Int to, float speed = 8, Action onStarted = null, Action onFinished = null)
        {
            _typedData.Animator.PlayWalk();
            Vector2Int startPosition = Owner.Position2Int;
            
            onStarted?.Invoke();
            _flipper.FlipToDirection(to.x - startPosition.x);
            
            while (Owner.transform.position != new Vector3(to.x, to.y, 0))
            {
                Owner.transform.position = Vector3.MoveTowards(Owner.transform.position,
                    new Vector3(to.x, to.y, 0), speed * Time.deltaTime);

                await UniTask.WaitForFixedUpdate();
            }

            Owner.Grid.ReplaceEntity(Owner, startPosition, Owner.Position2Int);
            Owner.OnUsedAbility(this);
            
            
            DrawMoveArrows();

            _typedData.Animator.StopWalk();
            onFinished?.Invoke();
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