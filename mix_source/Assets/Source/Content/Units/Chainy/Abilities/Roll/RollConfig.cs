using autumn_berries_mix.Helpers;
using UnityEngine;

namespace autumn_berries_mix.Units.Abilities.Roll
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Abilities + nameof(RollConfig))]
    public class RollConfig : ScriptableObject
    {
        public RollData Data;
    }
}