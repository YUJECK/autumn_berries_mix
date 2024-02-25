using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Sounds;
using UnityEngine;

namespace autumn_berries_mix
{
    public sealed class Dumbo : EnemyUnit
    {
        [SerializeField] private BishopMovementConfig bishopMovementConfig;
        
        private GameplayScene _scene;
        private Pathfinder _pathfinder;

        public override UnitHealth UnitHealth { get; protected set; }

        protected override void ConfigureComponents()
        {
            UnitHealth = GetComponent<UnitHealth>();
            _pathfinder = new Pathfinder(Grid);
            _scene = SceneSwitcher.TryGetGameplayScene();
            
            Master.Add(new EntityFlipper());
        }

        protected override void ConfigureAbilities()
        {
            bishopMovementConfig = Instantiate(bishopMovementConfig);
            
            PushAbility(new BishopMovement(this, bishopMovementConfig.data));
        }

        public override void OnUnitTurn()
        {
            GetAbility<BishopMovement>()
                .Move(GetMoveDirection(), PlayMove, StopMove);
        }

        private void StopMove()
        {
            GetComponent<Animator>().SetBool("Moving", false);
            AudioPlayer.Stop("GhostMove");
            
            OnUsedAbility(GetAbility<BishopMovement>());
        }

        private void PlayMove()
        {
            GetComponent<Animator>().SetBool("Moving", true);
            AudioPlayer.Play("GhostMove");
        }

        private Vector2Int GetMoveDirection()
        {
            return _pathfinder.FindPath(Position2Int, _scene.FindNearestPlayerUnit(this).Position2Int) [0] - Position2Int;
        }
    }
}