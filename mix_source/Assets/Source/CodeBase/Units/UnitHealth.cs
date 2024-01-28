using System;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public class UnitHealth : MonoBehaviour
    {
        [field: SerializeField] public int CurrentHealth { get; private set; } = 100;
        [field: SerializeField] public int MaximumHealth { get; private set; } = 100;

        public Action<int, int> OnHealthChanged; //current/maximum

        public void Hit(int points)
        {
            CurrentHealth -= points;
            
            if(CurrentHealth <= 0)
                Debug.Log("DIED");
            
            OnHealthChanged?.Invoke(CurrentHealth, MaximumHealth);
        }
        
        public void Heal(int points)
        {
            CurrentHealth += points;

            if (CurrentHealth >= MaximumHealth)
                CurrentHealth = MaximumHealth;
            
            OnHealthChanged?.Invoke(CurrentHealth, MaximumHealth);
        }
    }
}