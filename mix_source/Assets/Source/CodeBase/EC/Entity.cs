using System;
using System.Collections;
using autumn_berries_mix.Grid;
using UnityEngine;

namespace autumn_berries_mix.EC
{
    public abstract class Entity : MonoBehaviour
    {
        public ComponentsMaster Master { get; private set; }
        public GameGrid Grid { get; private set; }

        public Vector3 Position3 => transform.position;
        public Vector2Int Position2Int => new Vector2Int((int)transform.position.x, (int)transform.position.y);
        public Quaternion Rotation => transform.rotation;

        public void InitGrid(GameGrid grid)
        {
            if (Grid == null)
                Grid = grid;
        }

        private void InitComponentsMaster()
            => Master = new ComponentsMaster(this);

        protected virtual void ConfigureComponents() { }
        public virtual void LoadedToLevel()
        {
            InitComponentsMaster();
            ConfigureComponents();
        }

        protected void OnEnable()
        {
            if (Master != null)
            {
                return;
            }
            
            InitComponentsMaster();
            ConfigureComponents();
        }

        private void Update()
        {
            Master.UpdateAll();
            OnUpdate();
        }

        protected virtual void OnUpdate() { }

        protected void OnDestroy()
        {
            Master?.DisposeAll();
            OnDestroyed();
        }

        protected virtual void OnDestroyed() { }

        public void MoveTo(Vector2Int to, float speed, Action onMoved)
            => StartCoroutine(MoveToRoutine(to, speed, onMoved));

        private IEnumerator MoveToRoutine(Vector2Int to, float speed, Action onMoved)
        {
            while (transform.position != new Vector3(to.x, to.y, 0))
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    new Vector3(to.x, to.y, 0), speed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
            
            onMoved?.Invoke();
        }
    }
}