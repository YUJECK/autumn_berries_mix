using System;
using autumn_berries_mix.Units;

namespace autumn_berries_mix
{
    [Serializable]
    public class BishopMovementData : AbilityData
    {
        public int range;
        public float speed;
        public int damage;
    }
}