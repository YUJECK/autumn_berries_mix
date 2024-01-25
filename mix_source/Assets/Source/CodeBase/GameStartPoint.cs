using autumn_berries_mix.Scenes;
using autumn_berry_mixВ;
using Source.Content;
using UnityEngine;
using Zenject;

namespace autumn_berries_mix
{
    public sealed class GameStartPoint: MonoBehaviour
    {
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
            
            _container.Inject(Resolver.Instance());
        }
        
        private void Start()
        {
            SceneSwitcher.SwitchTo(new TestLevelScene());
        }
    }
}