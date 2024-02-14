using autumn_berries_mix.Scenes;
using UnityEngine;

namespace autumn_berries_mix.Helpers
{
    public sealed class StupidScene : Scene
    {
        private readonly string sceneName;

        public StupidScene(string sceneName)
        {
            this.sceneName = sceneName;
        }

        public override string GetSceneName()
        {
            return sceneName;
        }

        public override Camera GetCamera()
        {
            return Camera.main;
        }

        public override void Load()
        {
        }
    }
}