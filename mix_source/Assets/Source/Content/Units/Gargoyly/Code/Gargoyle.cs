using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.EC;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.Sounds;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.Gargoyly.Code
{ 
    public sealed class Gargoyle : Entity
    {
        public Vector2Int direction;
        public int damage;

        private SignalSubscription _subscription;
        
        protected override void ConfigureComponents()
        {
            _subscription = SignalManager.SubscribeOnSignal<UnitMovedSignal>(OnMoved);
        }

        protected override void OnDestroyed()
        {
            SignalManager.UnsubscribeOnSignal(_subscription);
        }

        private void OnMoved(UnitMovedSignal signal)
        {
            if (signal.Unit is PlayerUnit && signal.Unit.Position2Int == Position2Int + direction)
            {
                signal.Unit.UnitHealth.Hit(damage);
                GetComponent<Animator>().Play("GargoylyAttack");
            }
        }

        public void PlayAttackSound()
        {
            AudioPlayer.Play("GargoyleBite");
        }
    }
}