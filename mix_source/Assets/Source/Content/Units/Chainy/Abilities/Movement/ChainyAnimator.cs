using autumn_berries_mix.Sounds;    
using UnityEngine;
using Component = autumn_berries_mix.EC.Component;

namespace autumn_berries_mix.Units
{
    public sealed class ChainyAnimator : Component
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

        public void PlayChainsawAttack()
        {
            _animator.Play("ChainyChainsawAttack");
            AudioPlayer.Play("Chainsaw");
        }
        
        public void PlayChainAttack()
        {
            _animator.Play("ChainyChainAttack");
        }

        public void PlayRoll()
        {
            _animator.Play("ChainyRoll");
            AudioPlayer.Play("Roll");
        }

        public void StopRoll()
        {
            _animator.Play("ChainyIdle");
            
            AudioPlayer.Stop("Roll");
        }
    }
}