using autumn_berries_mix.Scenes;
using Zenject;

namespace autumn_berries_mix.PrefabTags.CodeBase.Scenes
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