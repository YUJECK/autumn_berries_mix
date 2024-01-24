using autumn_berry_mix.Scenes;
using Source.Content.Tiles;
using UnityEngine;
using Grid = autumn_berry_mix.Grid.Grid;

namespace Source.Content
{
    public sealed class TestLevelScene : Scene
    {
        private Grid _grid;
        private Chainy _chainy;
        private TreeEntity tree;
        
        public override string GetSceneName()
            => "TestLevel";

        public override void Load()
        {
            _grid = GameObject
                .Instantiate<Grid>(Resources.Load<Grid>(AssetsHelper.TestGrid));
            
            _chainy = GameObject
                .Instantiate<Chainy>(Resources.Load<Chainy>(AssetsHelper.Chainy));

            tree = GameObject
                    .Instantiate(Resources.Load<TreeEntity>("Tree"));
            
            _grid.GridData.Get(2, 4).Place(_chainy);
            _grid.GridData.Get(0, 6).Place(tree);
        }

        public override void Tick()
        {
            
        }

        public override void Dispose() { }
    }
}