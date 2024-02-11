using autumn_berries_mix.PrefabTags.CodeBase.Scenes;

namespace autumn_berries_mix.Units
{
    public abstract class GameplayProcessor
    {
        protected GameplayScene Scene { get; private set; }

        protected GameplayProcessor(GameplayScene scene)
        {
            Scene = scene;
        }

        public abstract void Start();
    }
}