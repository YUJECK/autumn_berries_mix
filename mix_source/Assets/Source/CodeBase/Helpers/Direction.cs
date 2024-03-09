using autumn_berries_mix.EC;
using UnityEngine;

namespace autumn_berries_mix.Helpers
{ 
    public static class Direction
    {
        public static readonly Vector2 North = Vector2.up;
        public static readonly Vector2 South = Vector2.down;
        public static readonly Vector2 East = Vector2.left;
        public static readonly Vector2 West = Vector2.right;
        public static readonly Vector2 NE = new Vector2(1,-1);
        public static readonly Vector2 NW = new Vector2(1,1);
        public static readonly Vector2 SE = new Vector2(-1,-1);
        public static readonly Vector2 CW = new Vector2(-1, 1);

        public static Vector2Int GetDirection(Entity entity1, Entity entity2)
        {
            var direction = entity2.Position2Int - entity1.Position2Int;

            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction.y = Mathf.Clamp(direction.y, -1, 1);

            return direction;
        }
        
        public static Vector2Int GetDirection(Vector2Int position1, Vector2Int position2)
        {
            var direction = position2 - position1;

            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction.y = Mathf.Clamp(direction.y, -1, 1);

            return direction;
        }
    }
}