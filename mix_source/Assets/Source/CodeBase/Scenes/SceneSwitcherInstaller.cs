using autumn_berries_mix.Scenes;
using Zenject;

namespace autumn_berries_mix.Source.CodeBase.Scenes
{
    public sealed class SceneSwitcherInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<SceneSwitcherTicker>()
                .AsSingle();
        }
    }
}