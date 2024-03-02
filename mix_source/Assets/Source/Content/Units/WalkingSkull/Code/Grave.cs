using autumn_berries_mix.EC;
using autumn_berries_mix.Sounds;
using UnityEngine;

namespace autumn_berries_mix.Source.Content.Units.WalkingSkull
{
    public class Grave : Entity, IBreakable
    {
        [Header("On break data")]
        [SerializeField] private Sprite brokenSprite;
        [SerializeField] private string brokeSound = "GraveBroken";

        public bool IsBroken { get; private set; }

        public void Break()
        {
            if(IsBroken) return;
            
            IsBroken = true;
            
            GetComponent<SpriteRenderer>().sprite = brokenSprite;
            AudioPlayer.Play(brokeSound);
        }
    }
}