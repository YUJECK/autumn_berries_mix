using autumn_berry_mix.Scenes;
using Source.Content;
using UnityEngine;

namespace Source.CodeBase
{
    public sealed class GameStartPoint: MonoBehaviour
    {
        private void Start()
        {
            SceneSwitcher.SwitchTo(new TestLevelScene());
        }
    }
}