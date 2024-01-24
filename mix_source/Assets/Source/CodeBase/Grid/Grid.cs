using System;
using UnityEngine;

namespace autumn_berry_mix.Grid
{
    [Serializable]
    public class Grid : MonoBehaviour
    {
        public GridData GridData { get; private set; }

        private void Awake()
        {
            if (GridData == null)
            {
                GridData = new GridData(GetChildrenTiles());
            }
        }

        private GridTile[] GetChildrenTiles()
        {
            return GetComponentsInChildren<GridTile>();
        }
    }
}