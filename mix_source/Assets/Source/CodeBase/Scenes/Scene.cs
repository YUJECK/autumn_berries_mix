using UnityEngine;

namespace autumn_berry_mix.Scenes
{
    public abstract class Scene
    {
        public abstract string GetSceneName();
        
        public abstract void Load();
        public virtual void Tick() {}
        public virtual void Dispose() {}
    }    
}