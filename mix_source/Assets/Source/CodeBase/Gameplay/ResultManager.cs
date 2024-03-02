using autumn_berries_mix.Scenes;

namespace autumn_berries_mix.PrefabTags.CodeBase.Scenes
{
    public sealed class ResultManager
    {
        public async void Lose()
        {
            await Curtain.Instance().Down();
            SceneSwitcher.SwitchTo(new LoseScene());
        }

        public void Win()
        {
            SceneSwitcher.SwitchTo(new WinScene());
        }
    }
}