using System.Collections.Generic;
using autumn_berries_mix;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Grid.BasicProcessor;
using autumn_berries_mix.Source.CodeBase;
using autumn_berries_mix.Source.CodeBase.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;
using Zenject;

namespace Source.Content
{
    public sealed class RottenBerriesLevelScene : GameplayScene
    {
        public override PlayerUnit[] PlayerUnitsPull => _playerUnitsPull.ToArray();
        public override EnemyUnit[] EnemyUnitsPull => _enemyUnitsPull.ToArray();

        public override PlayerUnit SelectedPlayerUnit => _playerUnitSelector.PlayerUnit;
        public override GameGrid GameGrid => Map.Grid;
        public GameplayMap Map { get; protected set; }
        
        private TileSelector _tileSelector;
        private GameplayResources _resources;
        private PlayerUnitSelector _playerUnitSelector;

        private readonly List<PlayerUnit> _playerUnitsPull = new();
        private readonly List<EnemyUnit> _enemyUnitsPull = new();
        
        private Camera _main;

        [Inject]
        private void Construct(GameplayResources gameplayResources)
        {
            _resources = gameplayResources;
        }
        
        public override string GetSceneName()
            => "TestLevel";

        public override Camera GetCamera()
            => _main;

        public override void Load()
        {
            Map = GameObject
                .Instantiate(Resources.Load<GameplayMap>(AssetsHelper.GameplayMap)); 
            
            LoadMapData();

            _playerUnitSelector = new PlayerUnitSelector();
            
            _tileSelector = new TileSelector(GameGrid, _resources, 
                    new BorderDrawer(_resources), 
                                    new PlayerUnitsAbilitiesProcessor(this),
                                    _playerUnitSelector);
            
            _tileSelector.Enable();
            
            Map.FinishLoading();
        }

        public override void Tick()
        {
            _tileSelector?.Tick();
        }

        private void LoadMapData()
        {
            Map.LoadGrid();
            _playerUnitsPull.AddRange(Map.LoadPlayerUnits());
            _enemyUnitsPull.AddRange(Map.LoadEnemyUnits());
            _main = Map.LoadCamera();
        }

        public override void Dispose() { }
    }
}