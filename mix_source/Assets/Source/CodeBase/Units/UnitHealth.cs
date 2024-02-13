using System;
using System.Collections;
using System.Collections.Generic;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Sounds;
using Source.Content;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [RequireComponent(typeof(Unit))]
    public class UnitHealth : MonoBehaviour
    {
        [field: SerializeField] public int CurrentHealth { get; private set; } = 100;
        [field: SerializeField] public int MaximumHealth { get; private set; } = 100;

        [SerializeField] private string hitSound;
        [SerializeField] private Color hitColor;
        [SerializeField] private string healSound;
        [SerializeField] private Color healColor;

        private Coroutine _coloringRoutine;
        
        public Unit Owner { get; private set; }
        
        public Action<int, int> OnHealthChanged; //current/maximum
        public Action OnDied;

        private void Awake()
        {
            Owner = GetComponent<Unit>();
        }

        public void Hit(int points)
        {
            CurrentHealth -= points;
            
            if(CurrentHealth <= 0)
                Debug.Log("DIED");
            
            OnHealthChanged?.Invoke(CurrentHealth, MaximumHealth);
            SignalManager.PushSignal(new UnitDamagedSignal(Owner, points));
            
            if(hitSound != "")
                AudioPlayer.Play(hitSound);
            
            if(_coloringRoutine != null)
                StopCoroutine(_coloringRoutine);

            _coloringRoutine = StartCoroutine(SetColor(hitColor));
        }

        private IEnumerator SetColor(Color color)
        {
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void Heal(int points)
        {
            CurrentHealth += points;

            if (CurrentHealth >= MaximumHealth)
                CurrentHealth = MaximumHealth;
            
            OnHealthChanged?.Invoke(CurrentHealth, MaximumHealth);
            SignalManager.PushSignal(new UnitHealedSignal(Owner, points));
 
            if(healSound != "")
                AudioPlayer.Play(healSound);
            
            if(_coloringRoutine != null)
                StopCoroutine(_coloringRoutine);

            _coloringRoutine = StartCoroutine(SetColor(healColor));
        }

        public void Die()
        {
            OnDied?.Invoke();
        }
    }
}