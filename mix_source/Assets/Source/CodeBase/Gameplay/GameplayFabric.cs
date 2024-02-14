using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Gameplay
{
    public class GameplayFabric : GameObjectFabric
    {
        private GameplayScene _scene;

        public GameplayFabric(GameplayScene scene)
        {
            _scene = scene;
        }

        public override TObject Instantiate<TObject>(TObject original, Vector3 position)
        {
            return Instantiate(original, position, Quaternion.identity, null);
        }

        public override TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation)
        {
            return Instantiate(original, position, rotation, null);
        }

        public override TObject Instantiate<TObject>(TObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            var newObject = GameObject.Instantiate(original, position, rotation, parent);
            
            if(newObject.TryGetComponent(out Unit unit))
                _scene.AddUnit(unit);

            return newObject;
        }

        public override void Destroy(GameObject gameObject)
        {
            if(gameObject.TryGetComponent(out Unit unit))
                _scene.RemoveUnit(unit);
            
            GameObject.Destroy(gameObject);
        }
    }
}