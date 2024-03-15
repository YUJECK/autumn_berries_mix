using System;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix
{
    [Serializable]
    public class SiberianHammerAttackData : AbilityData
    {
        [field: SerializeField] public int Damage { get; private set; }
    }
}