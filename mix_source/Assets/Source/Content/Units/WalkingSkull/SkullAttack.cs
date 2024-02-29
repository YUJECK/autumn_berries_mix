using System;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using Cysharp.Threading.Tasks;

namespace autumn_berries_mix.Source.Content.Units.WalkingSkull
{
    public sealed class SkullAttack : EnemyAbility
    {
        public SkullAttack(Unit owner, AbilityData data) : base(owner, data) { }

        public async void Attack(int damage, PlayerUnit unit)
        {
            Owner.Master.Get<EntityFlipper>().FlipTo(unit.transform);
         
            Owner.Master.Get<SkullAnimator>().PlayAttack();

            await UniTask.Delay(TimeSpan.FromSeconds(0.45f));
            
            unit.UnitHealth.Hit(damage);
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            
            Owner.FinishTurn();
        }
    }
}