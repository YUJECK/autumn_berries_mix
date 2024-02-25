using System;
using UnityEngine;

namespace autumn_berries_mix.Scenes
{
    public class GameObjectFabric
    {
        public event Action<GameObject> OnNewGameObject;
        public event Action<GameObject> OnGameObjectDestroyed;
        
        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position)
            where TObject : MonoBehaviour
        {
            return Instantiate(original, position, Quaternion.identity, null);
        }
        
        public virtual  TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation)
            where TObject : MonoBehaviour
        {
            return Instantiate(original, position, rotation, null);
        }

        public virtual TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation,
            Transform parent)
            where TObject : MonoBehaviour
        {
            var newGO = GameObject.Instantiate(original, position, rotation, parent);
            
            OnNewGameObject?.Invoke(newGO.gameObject);
            
            return newGO;   
        }

        public virtual void Destroy(GameObject gameObject)
        {
            OnGameObjectDestroyed?.Invoke(gameObject);
            GameObject.Destroy(gameObject);
        }
    }
}