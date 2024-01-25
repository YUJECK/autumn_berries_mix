using System;
using autumn_berry_mixВ;
using Internal.Codebase.Infrastructure.Services.SceneLoader;    

namespace autumn_berries_mix.Scenes
{
    public static class SceneSwitcher
    {
        public static Scene CurrentScene { get; private set; }
 
        public static event Action<Scene, Scene> OnSceneLoaded; //prev/new

        private static readonly AsyncSceneLoader Loader = new();

        public static void SwitchTo<TScene>(TScene scene)
            where TScene : Scene
        {
            CurrentScene?.Dispose();
            
            Loader.LoadScene(scene.GetSceneName(), () => Complete(scene));
        }

        private static void Complete<TScene>(TScene scene) 
            where TScene : Scene
        {
            Resolver.Instance().InjectScene(scene);
            
            scene.Load();

            OnSceneLoaded?.Invoke(CurrentScene, scene);
            
            CurrentScene = scene;
        }

        public static void Tick()
        {
            CurrentScene?.Tick();
        }
    }
}