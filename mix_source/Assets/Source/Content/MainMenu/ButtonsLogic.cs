using autumn_berries_mix.Scenes;
using autumn_berries_mix.Sounds;
using Source.Content;
using UnityEngine;

namespace autumn_berries_mix
{
    public class ButtonsLogic : MonoBehaviour
    {
        public async void LoadDemoLevel()
        {
            AudioPlayer.Play("PlayButton");
            await Curtain.Instance().Down();
            SceneSwitcher.SwitchTo(new DemoLevel());
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void EnableDisable(GameObject go)
        {
            AudioPlayer.Play("DefaultButton");
            go.SetActive(!go.activeSelf);
        }
    }
}
