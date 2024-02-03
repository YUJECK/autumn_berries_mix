using autumn_berries_mix.Grid;
using UnityEngine;

namespace autumn_berries_mix.PrefabTags
{
    public sealed class MovementArrow : TileOverlayPrefab
    {
        public override void OnPointed()
        {
            GetComponent<Animator>().SetBool("Selected", true);
        }
        
        public override void OnUnpointed()
        {
            GetComponent<Animator>().SetBool("Selected", false);
        }
    }
}