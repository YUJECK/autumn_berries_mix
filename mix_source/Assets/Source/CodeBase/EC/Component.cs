namespace autumn_berries_mix.EC
{
    public abstract class Component
    {
        public Entity Owner { get; private set; }
        
        public virtual void Start(Entity owner)
            => Owner = owner;

        public virtual void Enable() { }
        public virtual void Disable() { }

        public virtual void Update() {}
        public virtual void Dispose() {}
    }
}