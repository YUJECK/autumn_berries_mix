using autumn_berries_mix.EC;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Source.CodeBase
{
    public class GameplayMap : MonoBehaviour
    {
        public GameGrid Grid { get; private set; }

        [SerializeField] private Transform playerUnitsContainer;
        [SerializeField] private Transform enemyUnitsContainer;
        
        public GameGrid LoadGrid()
        {
            Grid = GetComponentInChildren<GameGrid>();
            return Grid;
        }

        public void FinishLoading()
        {
            var entities = GetComponentsInChildren<Entity>();

            foreach (var entity in entities)
            {
                entity.LevelLoaded();
            }
        }
        
        public PlayerUnit[] LoadPlayerUnits()
            => playerUnitsContainer.GetComponentsInChildren<PlayerUnit>();

        public EnemyUnit[] LoadEnemyUnits()
            => enemyUnitsContainer.GetComponentsInChildren<EnemyUnit>();

        public Camera LoadCamera()
            => GetComponentInChildren<Camera>();
    }
}