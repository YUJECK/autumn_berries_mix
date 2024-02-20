using System.Collections.Generic;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.Headsman.Code
{
    [RequireComponent(typeof(UnitHealth))]
    public sealed class Headsman : EnemyUnit
    {
        [SerializeField] private AbilityData movementConfig;
        [SerializeField] private AbilityData axeConfig;
        
        public int range = 2;
        
        public override UnitHealth UnitHealth { get; protected set; }

        protected override void ConfigureComponents()
        {
            Master.Add(new EntityFlipper());
            UnitHealth = GetComponent<UnitHealth>();
        }

        protected override void ConfigureAbilities()
        {
            PushAbility(new AxeAttack(this, axeConfig, range, 2));
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
                GetAbility<StepMovement>().Move(Position2Int + Vector2Int.right);
            }
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