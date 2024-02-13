using autumn_berries_mix.Scenes;
using Source.Content;
using UnityEngine;

namespace autumn_berries_mix
{
    public class ButtonsLogic : MonoBehaviour
    {
        public void LoadDemoLevel()
        {
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
