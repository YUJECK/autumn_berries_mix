using autumn_berries_mix;
using autumn_berries_mix.Grid;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;
using Zenject;
using Component = autumn_berries_mix.EC.Component;

namespace autumn_berry_mixВ
{
    public class Resolver
    {
        private static Resolver _instance;
        private DiContainer _diContainer;

        [Inject]
        private void GetContainer(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public static Resolver Instance()
        {
            if(_instance == null)
                _instance = new Resolver();

            return _instance;
        }
        
        public void InjectComponent(Component component)
            => Inject(component);

        public void InjectScene(Scene scene)
            => Inject(scene);
        
        public void InjectAbility(UnitAbility ability)
            => Inject(ability);
        
        public void InjectTileProcessor(SelectedTileProcessor processor)
            => Inject(processor);

        private void Inject(object obj)
        {
            if (obj == null)
            {
                Debug.LogError("NULL OBJECT TO INJECT");
                return;
            }
            
            _diContainer.Inject(obj);
        }
    }
}