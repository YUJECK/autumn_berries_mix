using autumn_berries_mix.Scenes;

namespace autumn_berries_mix.PrefabTags.CodeBase.Scenes
{
    public sealed class ResultManager
    {
        public void Lose()
        {
            SceneSwitcher.SwitchTo(new LoseScene());
        }

        public void Win()
        {
            SceneSwitcher.SwitchTo(new WinScene());
        }
    }
}