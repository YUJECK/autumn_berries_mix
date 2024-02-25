using System;
using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.Headsman.Code
{
    [RequireComponent(typeof(UnitHealth))]
    public sealed class Headsman : EnemyUnit
    {
        [SerializeField] private AbilityData movementConfig;
        [SerializeField] private AxeAttackConfig axeConfig;
        
        public int range = 2;
        private GameplayScene _scene;
        private Pathfinder _pathfinder;
        
        public override UnitHealth UnitHealth { get; protected set; }

        public void PlayAxeSwoosh()
        {
            AudioPlayer.Play("HeadsmanAxe");
        }
        
        protected override void ConfigureComponents()
        {
            _scene = SceneSwitcher.TryGetGameplayScene();
            _pathfinder = new Pathfinder(Grid);
            
            Master.Add(new EntityFlipper());
            Master.Add(new HeadsmanAnimator());
            
            UnitHealth = GetComponent<UnitHealth>();
        }

        protected override void ConfigureAbilities()
        {
            PushAbility(new AxeAttack(this, axeConfig.AxeAttackData, range, 1));
            PushAbility(new StepMovement(this, movementConfig));
        }

        public override void OnUnitTurn()
        {
            var target = CheckArea();

            if (target != null)
            {
                GetAbility<AxeAttack>().Attack(target);
            }
            else
            {
                GetAbility<StepMovement>().Move(GetTileToMove(), 2, Master.Get<HeadsmanAnimator>().PlayWalk, OnFinished);
            }
        }

        private void OnFinished()
        {
            Master.Get<HeadsmanAnimator>().StopWalk();
            OnUsedAbility(GetAbility<StepMovement>());
        }

        private Vector2Int GetTileToMove()
        {
            return _pathfinder.FindPath(Position2Int, _scene.FindNearestPlayerUnit(this).Position2Int)[0];
        }

        private PlayerUnit CheckArea()
        {
            List<GridTile> tiles = new List<GridTile>();
            
            tiles.Add(Grid.Get(Position2Int));

            for (int i = 0; i < range; i++)
            {
                int startCount = tiles.Count;
                
                for(int tileIndex = 0; tileIndex < startCount; tileIndex++)
                {
                    foreach (var connection in Grid.GetConnections(tiles[tileIndex].Position2Int.x, tiles[tileIndex].Position2Int.y))
                    {
                        tiles.Add(connection);      
                    } 
                }
            }
            
            while (tiles.Count > 0)
            {
                if (tiles[0].TileStuff is PlayerUnit playerUnit)
                    return playerUnit;

                tiles.RemoveAt(0);
            }

            return null;
        }
    }
}