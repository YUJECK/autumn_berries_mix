using autumn_berries_mix;
using autumn_berries_mix.Gameplay;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Grid.BasicProcessor;
using autumn_berries_mix.PrefabTags.CodeBase;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Turns;
using autumn_berries_mix.Units;
using UnityEngine;
using Zenject;

namespace Source.Content
{
    public sealed class DemoLevel : GameplayScene
    {
        public override PlayerUnit SelectedPlayerUnit => _unitSelector.PlayerUnit;
        public override GameGrid GameGrid => Map.Grid;
        public override GameObjectFabric Fabric { get; protected set; } = new GameObjectFabric();

        public GameplayMap Map { get; protected set; }
        
        private TileSelector _tileSelector;
        private GameplayResources _resources;
        private UnitSelector _unitSelector;
        
        private Camera _main;

        [Inject]
        private void Construct(GameplayResources gameplayResources)
        {
            _resources = gameplayResources;
        }

        public DemoLevel()
        {
            Fabric = new GameplayFabric(this);
        }

        public override string GetSceneName()
            => "TestLevel";

        public override Camera GetCamera()
            => _main;

        public override void Load()
        {
            CreateMap();

            CreateTurnController();
            
            CreateTileSelectorAndProcessors();
            
            Finish();
        }

        public override void Tick()
        {
            _tileSelector?.Tick();
        }

        public override void Dispose()
        {
            AudioPlayer.StopAllNonCrossScene();
        }

        private void Finish()
        {
            Map.FinishLoading();
            InvokeOnConfiguringFinished();
                
            TurnController.SwitchToNext();
            _tileSelector.Enable();
            
            AudioPlayer.Play("MainTheme");
            
            
            AudioPlayer.Play("CrowsAmbient");
        }

        private void CreateTileSelectorAndProcessors()
        {
            _unitSelector = new UnitSelector();

            var playerUnitsAbilitiesProcessor = new PlayerUnitsAbilitiesProcessor(this);
            _tileSelector = new TileSelector(GameGrid, _resources, 
                new BorderDrawer(_resources), 
                playerUnitsAbilitiesProcessor,
                _unitSelector);

            TurnController.RegisterAddiction(playerUnitsAbilitiesProcessor);
            TurnController.RegisterAddiction(_tileSelector);
            
            GameplayProcessors.Add(new UnitHealthProcessor(this));
        }

        private void CreateTurnController()
        {
            TurnController = new TurnController(this, new PlayerTurn(), new EnemyTurn());
        }

        private void CreateMap()
        {
            Map = GameObject
                .Instantiate(Resources.Load<GameplayMap>(AssetsHelper.GameplayMap));

            LoadMapData();
        }

        private void LoadMapData()
        {
            Map.LoadGrid();
            
            Units.AddUnits(Map.LoadPlayerUnits());
            Units.AddUnits(Map.LoadEnemyUnits());
            
            _main = Map.LoadCamera();
        }
    }
}