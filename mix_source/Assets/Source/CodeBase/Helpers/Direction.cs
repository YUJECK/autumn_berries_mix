using UnityEngine;

namespace autumn_berries_mix.Source.CodeBase.Helpers
{ 
    public sealed class Direction
    {
        public readonly Vector2 North = Vector2.up;
        public readonly Vector2 South = Vector2.down;
        public readonly Vector2 East = Vector2.left;
        public readonly Vector2 West = Vector2.right;
        public readonly Vector2 NE = new Vector2(1,-1);
        public readonly Vector2 NW = new Vector2(1,1);
        public readonly Vector2 SE = new Vector2(-1,-1);
        public readonly Vector2 CW = new Vector2(-1, 1);
    }
}