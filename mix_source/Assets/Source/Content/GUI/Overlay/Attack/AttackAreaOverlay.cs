using autumn_berries_mix.Grid;
using UnityEngine;

namespace autumn_berries_mix
{
    [RequireComponent(typeof(Animator))]
    public class AttackAreaOverlay : TileOverlayPrefab
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void PlayIdle()
        {
            _animator.Play("AttackAreaOverlayIdle");
        }

        private void PlayPointed()
        {
            _animator.Play("AttackAreaOverlayPointed");
        }
        
        public override void OnPointed()
        {
            PlayPointed();
        }

        public override void OnUnpointed()
        {
            PlayIdle();     
        }
    }
}
