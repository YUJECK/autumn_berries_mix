using autumn_berries_mix.Grid;

namespace autumn_berries_mix.Units
{
    public sealed class MovementAbilityData : AbilityData
    {
        public readonly GameGrid Grid;

        public MovementAbilityData(GameGrid grid)
        {
            Grid = grid;
        }
    }
}