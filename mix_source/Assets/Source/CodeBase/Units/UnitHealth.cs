using System;
using System.Collections;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Sounds;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    [RequireComponent(typeof(Unit))]
    public class UnitHealth : MonoBehaviour
    {        
        [field: Header("Main Stats")]
        [field: SerializeField] public int CurrentHealth { get; private set; } = 15;
        [field: SerializeField] public int MaximumHealth { get; private set; } = 15;

        [Header("On Death")]
        [SerializeField] private string deathAnimation;
        [SerializeField] private float deathAnimationLenght;
        
        [Header("On Hit")] 
        [SerializeField] private string hitSound;
        [SerializeField] private Color hitColor;
        
        [Header("On Heal")]
        [SerializeField] private string healSound;
        [SerializeField] private Color healColor;

        private Coroutine _coloringRoutine;
        private Animator _animator;

        public Unit Owner { get; private set; }
        public bool Dead { get; private set; } = false;
        
        public Action<int, int> OnHealthChanged; //current/maximum
        public Action OnDied;

        private void Awake()
        {
            Owner = GetComponent<Unit>();

            if (deathAnimation != "")
            {
                _animator = GetComponent<Animator>();
            }
        }

        public void Hit(int points)
        {
            CurrentHealth -= points;

            OnHealthChanged?.Invoke(CurrentHealth, MaximumHealth);
            SignalManager.PushSignal(new UnitDamagedSignal(Owner, points));
            
            if(hitSound != "")
            {
                AudioPlayer.Play(hitSound);
            }

            if(_coloringRoutine != null)
            {
                StopCoroutine(_coloringRoutine);
            }

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

        public async UniTask Die()
        {
            Dead = true;
            
            if (_animator != null)
            {
                _animator.Play(deathAnimation);
                await UniTask.Delay(TimeSpan.FromSeconds(deathAnimationLenght));
            }
            
            OnDied?.Invoke();
        }
    }
}