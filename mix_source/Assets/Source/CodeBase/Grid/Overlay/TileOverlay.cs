using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class TileOverlay<TData>
        where TData : TileOverlayData
    {
        protected SpriteRenderer SpriteRenderer;
        protected readonly GridTile Owner;
        
        public TData Data { get; private set; }

        public bool Enabled => Transform.gameObject.activeSelf;
        private Transform Transform => SpriteRenderer.transform;

        public TileOverlay(GridTile owner)
        {
            Owner = owner;
        }

        protected void FinishConstructor()
        {
            Transform.position = Owner.transform.position;
            Disable();
        }

        public void Enable()
        {
            if(!SpriteRenderer.gameObject.activeSelf)
                SpriteRenderer.gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            if(SpriteRenderer.gameObject.activeSelf)
                SpriteRenderer.gameObject.SetActive(false);
        }

        public virtual void OnPointed()
        {
            
        }
        
        public virtual void OnUnpointed()
        {
            
        }
        
        public virtual void ApplyData(TData data)
        {
            Data = data;
            SpriteRenderer.sortingOrder = Data.OrderInLayer;
            
            Transform.position = Owner.transform.position;
            Transform.position += Data.Offset;
            Transform.rotation = Quaternion.Euler(0,0, Data.Rotation);

            SpriteRenderer.gameObject.name = ParseOverlayName(data);
            
            Enable();
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