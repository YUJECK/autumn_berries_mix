using System;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;

namespace autumn_berries_mix.Turns
{
    public abstract class Turn
    {
        public bool Completed { get; protected set; } = true;

        protected GameplayScene CurrentScene;
        
        public virtual void Initialize(GameplayScene scene)
        {
            CurrentScene = scene;
        }
        
        public abstract void Start(Action onCompleted);
        public abstract void Complete();
    }
}