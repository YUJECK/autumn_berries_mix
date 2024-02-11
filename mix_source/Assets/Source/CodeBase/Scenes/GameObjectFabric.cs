using UnityEngine;

namespace autumn_berries_mix.Scenes
{
    public class GameObjectFabric
    {
        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position)
            where TObject : MonoBehaviour
        {
            return GameObject.Instantiate(original, position, Quaternion.identity);
        }
        
        public virtual  TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation)
            where TObject : MonoBehaviour
        {
            return GameObject.Instantiate(original, position, rotation);
        }

        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation,
            Transform parent)
            where TObject : MonoBehaviour
        {
            return GameObject.Instantiate(original, position, rotation, parent);   
        }

        public virtual void Destroy(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }
    }
}