using UnityEngine;

namespace autumn_berries_mix.EC
{
    public abstract class Entity : MonoBehaviour
    {
        public ComponentsMaster Master { get; private set; }

        protected void InitComponentsMaster()
            => Master = new ComponentsMaster(this);

        protected virtual void Awake()
            => InitComponentsMaster(); 
        
        protected virtual void OnEnable()
            => InitComponentsMaster(); 
        
        protected virtual void Update()
            => Master.UpdateAll();

        protected virtual void OnDestroy()
            => Master.DisposeAll();
    }
}