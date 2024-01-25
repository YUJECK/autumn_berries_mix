using Zenject;

namespace autumn_berries_mix.Scenes
{
    public sealed class SceneSwitcherTicker : ITickable
    {
        public void Tick()
            => SceneSwitcher.Tick();
    }
}