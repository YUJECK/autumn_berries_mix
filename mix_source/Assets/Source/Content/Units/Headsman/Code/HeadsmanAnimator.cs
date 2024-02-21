using autumn_berries_mix.EC;
using UnityEngine;
using Component = autumn_berries_mix.EC.Component;

namespace autumn_berries_mix.Source.Content.Units.Headsman.Code
{
    public sealed class HeadsmanAnimator : Component
    {
        private Animator _animator;

        public override void Start(Entity owner)
        {
            base.Start(owner);

            _animator = owner.GetComponent<Animator>();
        }

        public void PlayAttack()
        {
            _animator.Play("HeadsmanAttack");
        }   
    }
}