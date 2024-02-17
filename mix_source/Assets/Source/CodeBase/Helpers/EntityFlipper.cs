using autumn_berries_mix.EC;
using UnityEngine;
using Component = autumn_berries_mix.EC.Component;

namespace autumn_berries_mix.Helpers
{
    public sealed class EntityFlipper : Component
    {
        public bool FaceToRight { get; private set; } = true;
        private SpriteRenderer _spriteRenderer;

        public override void Start(Entity owner)
        {
            base.Start(owner);
            _spriteRenderer = Owner.GetComponent<SpriteRenderer>();
        }
        
        public void FlipToDirection(float x)
        {
            if(x > 0)
                FlipToRight();
            else 
                FlipToLeft();
        }
        
        public void FlipTo(Transform transform)
        {
            if (transform.position.x > Owner.transform.position.x)
            {
                FlipToRight();   
            }
            else
            {
                FlipToLeft();
            }
        }
        
        public void FlipToOpposite()
        {
            if(FaceToRight)
                FlipToLeft();
            else
                FlipToRight();
        }

        public void FlipToRight()
        {
            if (!FaceToRight)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
                FaceToRight = true;
            }
        }
        
        public void FlipToLeft()
        {
            if (FaceToRight)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
                FaceToRight = false;
            }
        }
    }
}