using autumn_berries_mix.Sounds;
using UnityEngine;

namespace autumn_berries_mix.Units
{
    public sealed class ChainyAnimator
    {
        private const string GrassWalk = "GrassWalk";
        private const string WalkBool = "Walk";
        
        private readonly Animator _animator;
        private static readonly int Walk = Animator.StringToHash(WalkBool);

        public ChainyAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void PlayWalk()
        {
            _animator.SetBool(Walk, true);
            AudioPlayer.Play(GrassWalk);
        }

        public void StopWalk()
        {
            _animator.SetBool(Walk, false);
            AudioPlayer.Stop(GrassWalk);
        }

        public void PlayAttack()
        {
            _animator.Play("ChainyChainsawAttack");
            AudioPlayer.Play("Chainsaw");
        }
    }
}