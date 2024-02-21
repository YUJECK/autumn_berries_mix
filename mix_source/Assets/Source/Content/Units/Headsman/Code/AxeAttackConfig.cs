using autumn_berries_mix.Helpers;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.Headsman.Code
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Abilities + nameof(AxeAttackConfig))]
    public class AxeAttackConfig : ScriptableObject
    {
        public AxeAttackData AxeAttackData;
    }
}