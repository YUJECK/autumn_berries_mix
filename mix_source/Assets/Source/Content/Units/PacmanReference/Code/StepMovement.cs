using System;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix
{
    public sealed class StepMovement : EnemyAbility
    {
        public StepMovement(Unit owner, AbilityData data) : base(owner, data)
        {
            
        }

        public async void Move(Vector2Int to, float speed = 5, Action onStarted = null, Action onFinished = null)
        {
            Vector2Int startPosition = Owner.Position2Int;
            
            onStarted?.Invoke();
            
            while (Owner.transform.position != new Vector3(to.x, to.y, 0))
            {
                Owner.transform.position = Vector3.MoveTowards(Owner.transform.position,
                    new Vector3(to.x, to.y, 0), speed * Time.deltaTime);

                await UniTask.WaitForFixedUpdate();
            }

            Owner.Grid.SwapEntities(startPosition.x, startPosition.y, Owner.Position2Int.x, Owner.Position2Int.y);
            Owner.OnUsedAbility(this);
            
            onFinished?.Invoke();
        }
    }
}