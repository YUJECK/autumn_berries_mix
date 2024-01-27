using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.EC
{
    public abstract class Entity : MonoBehaviour
    {
        public ComponentsMaster Master { get; private set; }

        public UnitAbility[] GetActionsPull;

        public Vector3 Position3 => transform.position;
        public Vector2 Position2 => new Vector2(transform.position.x, transform.position.y);
        public Quaternion Rotation => transform.rotation;
        
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