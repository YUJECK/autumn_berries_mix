using autumn_berries_mix;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Grid.BasicProcessor;
using autumn_berries_mix.Scenes;
using Cinemachine;
using Source.Content.Tiles;
using UnityEngine;
using Zenject;

namespace Source.Content
{
    public sealed class TestLevelScene : Scene
    {
        public GameGrid GameGrid { get; private set; }
        public Chainy Chainy { get; private set; }
        public Camera main { get; private set; }

        private CharacterMovement _movement;
        private TileSelector _tileSelector;
        private GameplayResources _resources;

        [Inject]
        private void Construct(GameplayResources gameplayResources)
        {
            _resources = gameplayResources;
        }
        
        public override string GetSceneName()
            => "TestLevel";

        public override Camera GetCamera()
            => main;

        public override void Load()
        {
            main = Camera.main;
            
            GameGrid = GameObject
                .Instantiate<GameGrid>(Resources.Load<GameGrid>(AssetsHelper.TestGrid)); ;
            
            Chainy = GameGrid.PlaceEntityToEmptyFromPrefab(2, -1, GetChainyPrefab());
            
            _tileSelector = new TileSelector(GameGrid, _resources, new BorderDrawer(_resources));
            
            CinemachineVirtualCamera camera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = Chainy.transform;
            
            _tileSelector.Enable();
        }

        private Chainy GetChainyPrefab()
            => Resources.Load<Chainy>(AssetsHelper.Chainy);
        
        private StaticDecorationEntity GetTreePrefab()
            => Resources.Load<StaticDecorationEntity>("Tree");

        public override void Tick()
        {
            _tileSelector.Tick();
        }

        public override void Dispose() { }
    }
}