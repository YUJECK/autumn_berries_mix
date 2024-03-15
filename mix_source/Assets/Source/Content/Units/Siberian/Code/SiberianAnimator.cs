using autumn_berries_mix.EC;
using autumn_berries_mix.Sounds;
using UnityEngine;

namespace autumn_berries_mix
{
    public class SiberianAnimator : PlayerUnitAnimator
    {
        private Animator _animator;
        
        public override void Start(Entity owner)
        {
            base.Start(owner);

            _animator = owner.GetComponent<Animator>();
        }

        public override void PlayWalk()
        {
            _animator.Play("SiberianRun");
        }

        public override void StopWalk()
        {
            _animator.Play("SiberianIdle");
        }

        public void PlayHammerAttack()
        {
            _animator.Play("SiberianAttack");
        }

        public void PlayPush()
        {
            _animator.Play("SiberianAttack");
        }
    }
}