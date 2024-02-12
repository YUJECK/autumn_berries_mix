using autumn_berries_mix.Grid;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;

namespace autumn_berries_mix
{
    public sealed class BishopMovement : EnemyAbility
    {
        private readonly BishopMovementData _typedData;

        public BishopMovement(Unit owner, BishopMovementData data) : base(owner, data)
        {
            _typedData = data;
        }

        public async void Move(Vector2Int direction, Action onStarted = null, Action onFinished = null)
        {
            onStarted?.Invoke();
            
            Vector2Int startPosition = Owner.Position2Int;

            for (int i = 0; i < Owner.Grid.TilesCount; i++)
            {
                Vector2Int movedPosition = Owner.Position2Int + direction;
                
                GridTile toTile = Owner.Grid.Get(movedPosition);
                GridTile nextTile = Owner.Grid.Get(movedPosition + direction);

                PlayerUnit unitToHit = null;
                
                if(toTile == null)
                    Finish();
                
                if (IsPlayerUnit(toTile, out var playerUnit))
                {
                    unitToHit = playerUnit;

                    if ((!nextTile.Empty && nextTile.TileStuff is not PlayerUnit) || !nextTile.Walkable)
                    {
                        direction *= -1;

                        Owner.Grid.ReplaceEntity(Owner, startPosition, Owner.Position2Int);
                        startPosition = Owner.Position2Int;
                    }
                }
                else if (!CanMoveToTile(toTile))
                {
                    Finish();
                    return;
                }
                
                while (Owner.transform.position != new Vector3(movedPosition.x, movedPosition.y, 0))
                {
                    Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, 
                        new Vector3(movedPosition.x, movedPosition.y, 0), _typedData.speed * Time.deltaTime);
                    
                    await UniTask.WaitForFixedUpdate();
                }

                if (unitToHit != null)
                {
                    unitToHit.UnitHealth.Hit(_typedData.damage);
                }
            }

            Finish();
            return;
            
            void Finish()
            {                    
                Owner.Grid.ReplaceEntity(Owner, startPosition, Owner.Position2Int);
                Owner.OnUsedAbility(this);
            
                onFinished?.Invoke();
            }
        }

        private static bool IsPlayerUnit(GridTile toTile, out PlayerUnit playerUnit)
        {
            playerUnit = null;
            
            if (!toTile.Empty && toTile.TileStuff is PlayerUnit unit)
            {
                playerUnit = unit;
            }
            
            return playerUnit != null;
        }

        private bool CanMoveToTile(GridTile toTile)
            => toTile.Empty || toTile.TileStuff == Owner && toTile.Walkable;
    }
}