using UnityEngine;

namespace autumn_berries_mix
{
    public class Wind : MonoBehaviour
    {
        public Vector2Int LeftUp;
        public Vector2Int RightDown;
        
        public void RandomizePosition()
        {
            transform.position = new(Random.Range(LeftUp.x, RightDown.x), Random.Range(LeftUp.y, RightDown.y));
        }
    }
}