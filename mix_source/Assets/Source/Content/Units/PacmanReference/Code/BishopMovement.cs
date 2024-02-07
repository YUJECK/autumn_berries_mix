using System;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
            bool reverse = false;
            
            while(Owner.gameObject.activeSelf)
            {
                int movedX = Owner.Position2Int.x + direction.x;
                int movedY = Owner.Position2Int.y + direction.y;

                var shotCell = Owner.Grid.Get(movedX, movedY);
                var nextCell = Owner.Grid.Get(movedX + direction.x, movedY + direction.y);

                PlayerUnit unitToHit = null;
                
                if(shotCell == null)
                    return;
                
                if (!shotCell.Empty)
                {
                    if (shotCell.TileStuff is PlayerUnit playerUnit)
                    {
                        unitToHit = playerUnit;

                        if (!nextCell.Empty && nextCell.TileStuff is not PlayerUnit)
                            reverse = true;
                    }
                    
                    else
                    {            
                        Owner.Grid.SwapEntities(startPosition.x, startPosition.y, Owner.Position2Int.x, Owner.Position2Int.y);
                        Owner.OnUsedAbility(this);
            
                        onFinished?.Invoke();
                        return;
                    }
                }
                
                while (Owner.transform.position != new Vector3(movedX, movedY, 0))
                {
                    Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, 
                        new Vector3(movedX, movedY, 0), _typedData.speed * Time.deltaTime);
                    
                    await UniTask.WaitForFixedUpdate();

                    if (unitToHit != null)
                    {
                        unitToHit.UnitHealth.Hit(_typedData.damage);
                        unitToHit = null;
                    }

                    if (reverse)
                    {
                        Move(direction * -1, onStarted, onFinished);
                        return;
                    }
                }
            }
            
            Owner.Grid.SwapEntities(startPosition.x, startPosition.y, Owner.Position2Int.x, Owner.Position2Int.y);
            Owner.OnUsedAbility(this);
            
            onFinished?.Invoke();
        }
    }
}