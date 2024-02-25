using autumn_berries_mix.EC;
using UnityEngine;
using Component = autumn_berries_mix.EC.Component;

namespace autumn_berries_mix.Source.Content.Units.WalkingSkull
{
    public class SkullAnimator : Component
    {
        private Animator _animator;

        public override void Start(Entity owner)
        {
            base.Start(owner);

            _animator = owner.GetComponent<Animator>();
        }

        public void PlayWalk()
        {
            _animator.SetBool("Walking", true);
        }

        public void StopWalk()
        {
            _animator.SetBool("Walking", false);
        }

        public void PlayAttack()
        {
            _animator.Play("SkullAttack");
        }
    }
}