using autumn_berries_mix.PrefabTags;
using UnityEngine;

namespace Source.Content
{
    [CreateAssetMenu(menuName = "Gameplay/ResourcesConfig")]
    public sealed class GameplayResources : ScriptableObject
    {
        public Sprite borderSprite;
        public MovementArrow MovementArrowPrefab;
    }
}