using System;
using autumn_berries_mix.Units;

namespace autumn_berries_mix.PrefabTags.CodeBase.Inputs
{
    public sealed class GameplayCallbacks
    {
        public event Action<PlayerUnit> OnPlayerUnitSelected;

        public void SelectPlayerUnit(PlayerUnit unit)
        {
            OnPlayerUnitSelected?.Invoke(unit);
        }
    }
}