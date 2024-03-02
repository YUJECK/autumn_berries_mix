using System;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Sounds;
using autumn_berry_mixВ;
using Internal.Codebase.Infrastructure.Services.SceneLoader;    

namespace autumn_berries_mix.Scenes
{
    public static class SceneSwitcher
    {
        public static Scene CurrentScene { get; private set; }
 
        public static event Action<Scene> OnSceneStartedLoading; //prev
        public static event Action<Scene, Scene> OnSceneLoaded; //prev/new

        private static readonly AsyncSceneLoader Loader = new();

        public static GameplayScene TryGetGameplayScene()
        {
            if(CurrentScene is GameplayScene gameplayScene)
                return CurrentScene as GameplayScene;

            throw new TypeAccessException("CURRENT SCENE NOT GAMEPLAY");
        }
        
        public static void SwitchTo<TScene>(TScene scene)
            where TScene : Scene
        {
            OnSceneStartedLoading?.Invoke(CurrentScene);
            
            CurrentScene?.Dispose();
            CurrentScene = scene;
            
            Loader.LoadScene(scene.GetSceneName(), () => Complete(scene));
        }

        private static void Complete<TScene>(TScene scene) 
            where TScene : Scene
        {
            Resolver.Instance().InjectScene(scene);

            Scene prevScene = CurrentScene;

            scene.Load();

            OnSceneLoaded?.Invoke(prevScene, scene);
        }

        public static void Tick()
        {
            CurrentScene?.Tick();
        }
    }
}