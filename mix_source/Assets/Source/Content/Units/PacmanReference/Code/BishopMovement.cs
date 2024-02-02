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

        public async void Move(Vector2Int direction)
        {
            Vector2Int startPosition = Owner.Position2Int;
            
            for (int i = 0; i < _typedData.range; i++)
            {
                int movedX = Owner.Position2Int.x + direction.x;
                int movedY = Owner.Position2Int.y + direction.y;

                if (!Data.Grid.Get(movedX, movedY).Empty) break;
                
                while (Owner.transform.position != new Vector3(movedX, movedY, 0))
                {
                    Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, 
                        new Vector3(movedX, movedY, 0), _typedData.speed * Time.deltaTime);
                    
                    await UniTask.WaitForFixedUpdate();
                }
            }
            
            Data.Grid.SwapEntities(startPosition.x, startPosition.y, Owner.Position2Int.x, Owner.Position2Int.y);
            Owner.OnUsedAbility(this);
        }
    }
}