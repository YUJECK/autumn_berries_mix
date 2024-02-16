using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.Units;

namespace autumn_berries_mix.Gameplay
{
    public class UnitHealthProcessor : GameplayProcessor
    {
        private SignalSubscription _subscription;
        
        public UnitHealthProcessor(GameplayScene scene) : base(scene) { }

        public override void Start()
        {
            _subscription = SignalManager.SubscribeOnSignal<UnitDamagedSignal>(OnDamaged);
        }

        ~UnitHealthProcessor()
        {
            SignalManager.UnsubscribeOnSignal(_subscription);
        }

        public void OnDamaged(UnitDamagedSignal signal)
        {
            if (signal.Unit.UnitHealth.CurrentHealth <= 0)
            {
                signal.Unit.UnitHealth.Die();
                Scene.Fabric.Destroy(signal.Unit.gameObject);
            }

            if (Scene.PlayerUnitsPull.Length == 0)
            {
                Scene.ResultManager.Lose();
            }
            
            if (Scene.EnemyUnitsPull.Length == 0)
            {
                Scene.ResultManager.Win();
            }
        }
    }
}









