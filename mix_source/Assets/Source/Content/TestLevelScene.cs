using autumn_berries_mix.Scenes;
using Cinemachine;
using Source.Content.Tiles;
using UnityEngine;
using Zenject;

using Grid = autumn_berries_mix.Grid.Grid;

namespace Source.Content
{
    public sealed class TestLevelScene : Scene
    {
        public Grid Grid { get; private set; }
        public Chainy Chainy { get; private set; }
        public Camera main { get; private set; }

        private CharacterMovement _movement;
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
            
            Grid = GameObject
                .Instantiate<Grid>(Resources.Load<Grid>(AssetsHelper.TestGrid)); ;
            
            Chainy = Grid.PlaceEntityToEmptyFromPrefab(2, -1, GetChainyPrefab());

            _movement = new CharacterMovement(this, _resources);
            
            CinemachineVirtualCamera camera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            camera.Follow = Chainy.transform;
        }

        private Chainy GetChainyPrefab()
            => Resources.Load<Chainy>(AssetsHelper.Chainy);
        
        private StaticDecorationEntity GetTreePrefab()
            => Resources.Load<StaticDecorationEntity>("Tree");

        public override void Tick()
        {
            _movement.Tick();
        }

        public override void Dispose() { }
    }
}