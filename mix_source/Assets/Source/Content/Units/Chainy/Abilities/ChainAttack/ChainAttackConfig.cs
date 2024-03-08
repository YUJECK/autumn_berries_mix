using autumn_berries_mix.Helpers;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Abilities + nameof(ChainAttackConfig))]
    public sealed class ChainAttackConfig : AbilityConfig
    {
        public ChainAttackData Data;
    }
}