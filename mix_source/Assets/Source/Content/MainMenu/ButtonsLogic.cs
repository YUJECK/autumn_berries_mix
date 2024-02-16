using System;
using autumn_berries_mix.Scenes;
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
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            SceneSwitcher.SwitchTo(new DemoLevel());
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void EnableDisable(GameObject go)
        {
            go.SetActive(!go.activeSelf);
        }
    }
}
