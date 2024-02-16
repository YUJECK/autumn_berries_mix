using UnityEngine;

namespace autumn_berries_mix
{
    public sealed class Curtain : MonoBehaviour
    {
        private static Curtain _instance;
        private Animator _animator;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(transform.parent);
                return;
            }
            DontDestroyOnLoad(transform.parent);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public static Curtain Instance()
            => _instance;

        public void Down()
        {
            gameObject.SetActive(true);
            _animator.Play("CurtainUp");
        }
    }
}
