using System;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Sounds;
using Cysharp.Threading.Tasks;
using Source.Content;
using UnityEngine;

namespace autumn_berries_mix
{
    public class ButtonsLogic : MonoBehaviour
    {
        public async void LoadDemoLevel()
        {
            Curtain.Instance().Down();
            AudioPlayer.Play("PlayButton");
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
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
