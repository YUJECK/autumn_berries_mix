using autumn_berries_mix.Scenes;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags.CodeBase.Scenes
{
    public class WinScene : Scene
    {
        public override string GetSceneName()
        {
            return "WinScene";
        }

        public override Camera GetCamera()
        {
            return Camera.main;
        }

        public override void Load()
        {
            
        }
    }
}