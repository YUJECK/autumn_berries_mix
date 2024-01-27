using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlay
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly GridTile _owner;
        
        private TileOverlayData _data;

        public bool Enabled => Transform.gameObject.activeSelf;
        private Transform Transform => _spriteRenderer.transform;

        public TileOverlay(SpriteRenderer spriteRenderer, GridTile owner)
        {
            _spriteRenderer = spriteRenderer;
            _owner = owner;

            Transform.position = owner.transform.position;
            
            Disable();
        }

        public virtual void ApplyData(TileOverlayData data)
        {
            _data = data;
            _spriteRenderer.sprite = _data.Sprite;
            _spriteRenderer.sortingOrder = _data.OrderInLayer;
            
            Transform.position = _owner.transform.position;
            Transform.position += _data.Offset;
            Transform.rotation = Quaternion.Euler(0,0, _data.Rotation);

            _spriteRenderer.gameObject.name = ParseOverlayName(data);
            
            Enable();
        }

        public void Enable()
        {
            if(!_spriteRenderer.gameObject.activeSelf)
                _spriteRenderer.gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            if(_spriteRenderer.gameObject.activeSelf)
                _spriteRenderer.gameObject.SetActive(false);
        }

        protected virtual string ParseOverlayName(TileOverlayData data)
        {
            if (Transform.parent != null)
            {
                return Transform.parent.name + " " + data.Name + " Overlay";
            }
            
            return data.Name + "Overlay";
        }
    }
}