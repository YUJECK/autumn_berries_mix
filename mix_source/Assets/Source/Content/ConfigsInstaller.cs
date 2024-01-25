using Zenject;

namespace Source.Content
{
    public sealed class ConfigsInstaller : MonoInstaller
    {
        public GameplayResources Resources;
        
        public override void InstallBindings()
        {
            Container
                .Bind<GameplayResources>()
                .FromInstance(Resources)
                .AsSingle();
        }
    }
}