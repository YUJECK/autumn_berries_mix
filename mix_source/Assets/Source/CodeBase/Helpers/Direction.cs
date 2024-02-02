using UnityEngine;

namespace autumn_berries_mix.Helpers
{ 
    public sealed class Direction
    {
        public static readonly Vector2 North = Vector2.up;
        public static readonly Vector2 South = Vector2.down;
        public static readonly Vector2 East = Vector2.left;
        public static readonly Vector2 West = Vector2.right;
        public static readonly Vector2 NE = new Vector2(1,-1);
        public static readonly Vector2 NW = new Vector2(1,1);
        public static readonly Vector2 SE = new Vector2(-1,-1);
        public static readonly Vector2 CW = new Vector2(-1, 1);
    }
}