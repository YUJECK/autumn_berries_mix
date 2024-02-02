using autumn_berries_mix.PrefabTags.CodeBase.GUI;
using autumn_berry_mixВ;
using UnityEngine;
using Zenject;

namespace autumn_berries_mix
{
    public class GUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var instance = SpawnGUI();
            
            Container
                .Bind<PlayerUnitAbilitiesGUI>()
                .FromInstance(instance)
                .AsSingle();
            
            Container.Inject(Resolver.Instance());
        }

        private PlayerUnitAbilitiesGUI SpawnGUI()
        {
            var prefab = Resources.Load<GameObject>("GUI");
            return Container.InstantiatePrefab(prefab, Vector2.zero, Quaternion.identity, null).GetComponentInChildren<PlayerUnitAbilitiesGUI>();
        }
    }
}
