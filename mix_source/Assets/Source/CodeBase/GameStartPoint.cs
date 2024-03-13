using autumn_berries_mix.Helpers;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Source.CodeBase;
using autumn_berry_mixВ;
using UnityEngine;
using Zenject;

namespace autumn_berries_mix
{
    public sealed class GameStartPoint: MonoBehaviour
    {
        public AudioPlayerPreset globalPreset;
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
            
            _container.Inject(Resolver.Instance());
        }
        
        private void Start()
        {
            AudioPlayer.CreateGlobalFromPreset(globalPreset);
            CursorLogic.Enable();
            
            SceneSwitcher.SwitchTo(new StupidScene("MainMenu"));
        }
    }
}