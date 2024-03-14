using System;
using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public class PlayerUnitStepMovement : PlayerAbility
    {
        private readonly MovementAbilityData _typedData;

        private const string OverlayKey = "MoveArrow";
        
        private readonly List<GridTile> _availableArea = new();
        private bool _currentMoving;

        private readonly EntityFlipper _flipper;
        
        public PlayerUnitStepMovement(Unit owner, MovementAbilityData data) 
            : base(owner, data)
        {
            _typedData = data;
            _flipper = Owner.Master.Get<EntityFlipper>();
        }
        
        //callbacks
        public override void OnAbilitySelected() => CreateNewMovementArea();

        public override void OnAbilityDeselected() => ClearArea();

        public override void Dispose() => ClearArea();

        public override void OnEmptyTilePointed(GridTile tile, bool withClick)
        {
            if (!_availableArea.Contains(tile)) return;
            
            if (withClick && !_currentMoving)
            {
                Move(tile.Position2Int);
            }
        }

        //movement logic
        private async void Move(Vector2Int to, float speed = 8, Action onStarted = null, Action onFinished = null)
        {
            ClearArea();
            
            Vector2Int startPosition = Owner.Position2Int;
            
            onStarted?.Invoke();
            _currentMoving = true;
            {
                _flipper.FlipAtDirection(to.x - startPosition.x); //flip unit at walk direction
                
                Owner.Master.Get<PlayerUnitAnimator>().PlayWalk(); //play walk animation
                while (Owner.transform.position != new Vector3(to.x, to.y, 0))
                {
                    Owner.transform.position = Vector3.MoveTowards(Owner.transform.position,
                        new Vector3(to.x, to.y, 0), _typedData.Speed * Time.deltaTime);

                    await UniTask.WaitForFixedUpdate();
                }
                Owner.Master.Get<PlayerUnitAnimator>().StopWalk();//stop walk animation
                
                Owner.Grid.ReplaceEntity(Owner, startPosition, Owner.Position2Int); //update unit position in grid
                Owner.OnUsedAbility(this);

                CreateNewMovementArea();

            }
            _currentMoving = false;
            onFinished?.Invoke();
        }
        
        //area generating
        private void CreateNewMovementArea()
        {
            ClearArea();

            if (_typedData.Directions.Length == 0)
            {
                CreateAreaFrom(GetFromConnected());
            }
            else
            {
                CreateAreaFrom(GetFromDirections());
            }
        }

        private void CreateAreaFrom(GridTile[] area)
        {
            foreach (var tile in area)
            {
                if(!tile.Empty || !tile.Walkable)
                    continue;
                
                float angle = Mathf.Atan2(tile.Position2Int.y - Owner.Position2Int.y, tile.Position2Int.x - Owner.Position2Int.x) 
                    * Mathf.Rad2Deg - 90;
                
                tile.Overlay.PushPrefabOverlay(new PrefabTileOverlayData(_typedData.ArrowPrefab, OverlayKey, angle));
                _availableArea.Add(tile);
            }
        }

        private GridTile[] GetFromDirections()
        {
            var result = new List<GridTile>();
            
            foreach (var direction in _typedData.Directions)
            {
                var tile = Owner.Grid.Get(Owner.Position2Int + direction);

                if (tile != null)
                {
                    result.Add(tile);
                }
            }

            return result.ToArray();
        }

        private GridTile[] GetFromConnected()
            => Owner.Grid.GetConnections(Owner.Position2Int.x, Owner.Position2Int.y);

        private void ClearArea()
        {
            foreach (var tile in _availableArea)
            {
                tile.Overlay.RemovePrefabOverlay(OverlayKey);
            }

            _availableArea.Clear();
        }
    }
}