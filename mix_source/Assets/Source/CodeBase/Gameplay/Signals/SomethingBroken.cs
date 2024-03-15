using autumn_berries_mix.EC;

namespace autumn_berries_mix.Gameplay.Signals
{
    public sealed class SomethingBroken : Signal
    {
        public readonly Entity Entity;
        public readonly IBreakable Breakable;

        public SomethingBroken(Entity entity, IBreakable breakable)
        {
            Entity = entity;
            Breakable = breakable;
        }
    }
}