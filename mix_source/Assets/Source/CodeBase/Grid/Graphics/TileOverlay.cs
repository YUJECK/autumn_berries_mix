using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlay
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly GridTile _owner;
        
        public TileOverlayData Data { get; private set; }

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
            Data = data;
            _spriteRenderer.sortingOrder = Data.OrderInLayer;
            
            Transform.position = _owner.transform.position;
            Transform.position += Data.Offset;
            Transform.rotation = Quaternion.Euler(0,0, Data.Rotation);

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