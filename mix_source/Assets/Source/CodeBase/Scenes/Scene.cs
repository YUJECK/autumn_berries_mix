using UnityEngine;

namespace autumn_berries_mix.Scenes
{
    public abstract class Scene
    {
        public abstract string GetSceneName();
        public abstract Camera GetCamera();

        public virtual GameObjectFabric Fabric { get; protected set; }
        
        public abstract void Load();
        public virtual void Tick() {}
        public virtual void Dispose() {}
    }    
}