using System;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix
{
    public sealed class StepMovement : EnemyAbility
    {
        private readonly EntityFlipper _flipper;
        
        public StepMovement(Unit owner, AbilityData data) : base(owner, data)
        {
            _flipper = Owner.Master.Get<EntityFlipper>();
        }

        public async void Move(Vector2Int to, float speed = 8, Action onStarted = null, Action onFinished = null)
        {
            Vector2Int startPosition = Owner.Position2Int;
            
            onStarted?.Invoke();
            
            _flipper?.FlipToDirection(to.x - startPosition.x);
            
            while (Owner.transform.position != new Vector3(to.x, to.y, 0))
            {
                Owner.transform.position = Vector3.MoveTowards(Owner.transform.position,
                    new Vector3(to.x, to.y, 0), speed * Time.deltaTime);

                await UniTask.WaitForFixedUpdate();
            }

            Owner.Grid.ReplaceEntity(Owner, startPosition, Owner.Position2Int);
            
            Owner.OnUsedAbility(this);
            onFinished?.Invoke();
        }

    }
}