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
        [SerializeField] private Vector2Int[] directions;
        
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
                var pos = GetTileToMove();
                
                if(Grid.Get(pos).Empty)
                {
                    GetAbility<StepMovement>()
                        .Move(GetTileToMove(), 2, Master.Get<HeadsmanAnimator>().PlayWalk, OnMoveFinished);
                }
                else
                {
                    FinishTurn();
                }
            }
        }

        private void OnMoveFinished()
        {
            Master.Get<HeadsmanAnimator>().StopWalk();
            OnUsedAbility(GetAbility<StepMovement>());
            FinishTurn();
        }

        private Vector2Int GetTileToMove()
        {
            return _pathfinder.FindPath(Position2Int, _scene.FindNearestPlayerUnit(this).Position2Int)[0];
        }

        private PlayerUnit CheckArea()
        {
            List<GridTile> availableArea = new List<GridTile>();
            
            foreach (var direction in directions)
            {
                List<GridTile> line = new List<GridTile>();
                
                //Проверяем всю линию
                for (int i = 1; i <= 2; i++)
                {
                    var tile = Grid.Get(Position2Int + direction * i);
                    
                    if (tile != null && (tile.Empty && i == 1) || tile.TileStuff is Unit)
                    {
                        line.Add(tile);    
                    }
                    else
                    {
                        break;
                    }
                }

                if (line.Count != 2) continue;
                
                //Добавляем линию в зону атаки
                foreach (var tile in line)
                {
                    availableArea.Add(tile);
                }
            }

            foreach (var tile in availableArea)
            {
                if (tile.TileStuff is PlayerUnit playerUnit)
                    return playerUnit;
            }
            
            return null;
        }
    }
}