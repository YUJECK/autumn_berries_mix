using autumn_berry_mix.EC;
using UnityEngine;

namespace autumn_berry_mix.Grid
{
    public class GridTile : MonoBehaviour
    {
        public virtual bool Empty { get; protected set; }
        public virtual bool Walkable { get; protected set; }

        public Entity TileStuff { get; private set; }

        public void Place(Entity stuff)
        {
            TileStuff = stuff;
            TileStuff.transform.position = transform.position;
        }
    }
}