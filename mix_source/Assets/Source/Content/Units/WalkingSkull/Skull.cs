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
        [Header("Abilities Configs")]
        [SerializeField] private AbilityData movementData;
        [SerializeField] private AbilityData attackData;
        
        private Pathfinder _pathfinder;
        private GameplayScene _scene;
        
        public override UnitHealth UnitHealth { get; protected set; }
        
        protected override void ConfigureComponents()
        {
            UnitHealth = GetComponent<UnitHealth>();
            Master.Add(new EntityFlipper());
            _pathfinder = new Pathfinder(Grid);
            _scene = SceneSwitcher.TryGetGameplayScene();
            Master.Add(new SkullAnimator());
        }
        
        protected override void ConfigureAbilities()
        {
            PushAbility(new StepMovement(this, movementData));
            PushAbility(new SkullAttack(this, movementData));
        }

        public override void OnUnitTurn()
        {
            var target = CheckForPlayerUnit();
            
            if(target == null)
            {
                GetAbility<StepMovement>().Move(GetStep(), 2, PlayWalk, FinishMove);
            }
            else
            {
                Attack(target);
            }
        }
        
        private void PlayWalk()
        {
            Master.Get<SkullAnimator>().PlayWalk();
        }

        private void FinishMove()
        {
            Master.Get<SkullAnimator>().StopWalk();

            Attack(CheckForPlayerUnit());
        }

        private  void Attack(PlayerUnit unit)
        {
            if (unit == null)
            {
                FinishTurn();
                return;
            }
            
            GetAbility<SkullAttack>().Attack(1, unit);
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
