using autumn_berries_mix.Helpers;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Abilities + nameof(MovementAbilityConfig))]
    public class MovementAbilityConfig : AbilityConfig
    {
        public MovementAbilityData Data;
    }
}