using autumn_berries_mix.EC;
using autumn_berries_mix.PrefabTags.CodeBase.Scenes;
using autumn_berries_mix.Scenes;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Gameplay
{
    public class GameplayFabric : GameObjectFabric
    {
        private readonly GameplayScene _scene;

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
            var newObject = base.Instantiate(original, position, rotation, parent);

            if (newObject.TryGetComponent(out Entity entity))
            {
                entity.InitGrid(_scene.GameGrid);
                
                if (entity is Unit unit)
                {
                    _scene.Units.AddUnit(unit);
                    unit.LoadedToLevel();    
                }

                _scene.GameGrid.Get(entity.Position2Int).Place(entity);
            }
            
            return newObject;
        }

        public override void Destroy(GameObject gameObject)
        {
            if(gameObject.TryGetComponent(out Unit unit))
                _scene.Units.RemoveUnit(unit);
            
            base.Destroy(gameObject);
        }
    }
}