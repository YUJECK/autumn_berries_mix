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
                .Bind<UnitAbilitiesGUIController>()
                .FromInstance(instance)
                .AsCached();
            
            Container.Inject(Resolver.Instance());
        }

        private UnitAbilitiesGUIController SpawnGUI()
        {
            var prefab = Resources.Load<GameObject>("GUI");
            return Container.InstantiatePrefab(prefab, Vector2.zero, Quaternion.identity, null).GetComponentInChildren<UnitAbilitiesGUIController>();
        }
    }
}
