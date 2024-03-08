using System;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [Serializable]
    public class ChainsawAttackData : AbilityData
    {
        public int Damage;
        public ChainyAnimator Animator; 
       
        [Header("Overlay Data")]
        public Sprite rangeCell;
        public Sprite selectedRangeCell;
    }
}