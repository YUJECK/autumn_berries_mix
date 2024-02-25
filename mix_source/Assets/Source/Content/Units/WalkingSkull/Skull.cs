using autumn_berries_mix.Helpers;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.WalkingSkull
{
    [RequireComponent(typeof(UnitHealth))]
    public sealed class Skull : EnemyUnit
    {
        private AbilityData movementData;
        private Pathfinder _pathfinder;
        private GameplayScene _scene;

        private Animator _animator;
        
        public override UnitHealth UnitHealth { get; protected set; }
        
        protected override void ConfigureComponents()
        {
            UnitHealth = GetComponent<UnitHealth>();
            Master.Add(new EntityFlipper());
            _pathfinder = new Pathfinder(Grid);
            _scene = SceneSwitcher.TryGetGameplayScene();
            _animator = GetComponent<Animator>();
        }

        protected override void ConfigureAbilities()
        {
            PushAbility(new StepMovement(this, movementData));
        }

        public override void OnUnitTurn()
        {
            var target = CheckForPlayerUnit();
            
            if(target == null)
            {
                GetAbility<StepMovement>().Move(GetStep(), 4, PlayWalk, Finish);
            }
            else
            {
                Attack(target);
            }
            
            OnUsedAbility(GetAbility<StepMovement>());
        }

        private void PlayWalk()
        {
            _animator.Play("SkullWalk");
        }

        private void Finish()
        {
            _animator.Play("SkullIdle");

            Attack(CheckForPlayerUnit());
        }

        private  void Attack(PlayerUnit unit)
        {
            if (unit == null)
            {
                return;
            }

            unit.UnitHealth.Hit(1);
        }

        private PlayerUnit CheckForPlayerUnit()
        {
            foreach (var tile in Grid.GetConnections(Position2Int.x, Position2Int.y))
            {
                if (tile.TileStuff is PlayerUnit playerUnit)
                    return playerUnit;
            }

            return null;
        }

        private Vector2Int GetStep()
        {
            return _pathfinder.FindPath(Position2Int, _scene.FindNearestPlayerUnit(this).Position2Int) [0];
        }
    }
}
