using System;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.Headsman.Code
{
    public sealed class AxeAttack : EnemyAbility
    {
        private readonly int _range;
        private readonly int _damage;
        private HeadsmanAnimator _animator;

        private readonly AxeAttackData _typedData;

        public AxeAttack(Unit owner, AxeAttackData data, int range, int damage) : base(owner, data)
        {
            _range = range;
            _damage = damage;
            _typedData = data;
            
            _animator = Owner.Master.Get<HeadsmanAnimator>();
        }
        
        public async void Attack(PlayerUnit unit)
        {
            Vector2Int direction = unit.Position2Int - Owner.Position2Int;
            
            Owner.Master.Get<EntityFlipper>().FlipToDirection(direction.x);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            _animator.PlayAttack();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));

            direction = new Vector2Int(Mathf.Clamp(direction.x, -1, 1), Mathf.Clamp(direction.y, -1, 1));
            
            GridTile current = Owner.Grid.Get(Owner.Position2Int);
            
            for (int i = 0; i < _range; i++)
            {
                current = Owner.Grid.Get(current.Position2Int + direction);
                PushOverlay(current);

                if (current.TileStuff is PlayerUnit playerUnit)
                {
                    playerUnit.UnitHealth.Hit(_damage);
                }
                
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            }
            
            Owner.OnUsedAbility(this);
        }

        private async void PushOverlay(GridTile tile)
        {
            var overlay =
                new StaticTileOverlayData(_typedData.OverlaySprite, _typedData.OverlaySprite, "AxeAttack");
            
            tile.Overlay.PushStaticOverlay(overlay);

            await UniTask.Delay(200);
            
            tile.Overlay.RemoveStaticOverlay(overlay);
        }
    }
}