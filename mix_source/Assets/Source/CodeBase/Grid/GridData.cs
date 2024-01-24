using System.Collections.Generic;
using UnityEngine;

namespace autumn_berry_mix.Grid
{
    public class GridData
    {
        private readonly Dictionary<Vector2Int, GridTile> _grid;
    
        public GridData(GridTile[] tiles)
        {
            _grid = new Dictionary<Vector2Int, GridTile>();
            ProcessTiles(tiles);
        }

        public TCell Get<TCell>(int x, int y)
            where TCell : GridTile
        {
            Vector2Int position = new Vector2Int(x, y);
        
            return (TCell)_grid[position];
        }
    
        public GridTile Get(int x, int y)
        {
            Vector2Int position = new Vector2Int(x, y);
            return _grid[position];
        }

        public void ProcessTiles(GridTile[] tiles)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                Edit((int)tiles[i].transform.position.x, (int)tiles[i].transform.position.y, tiles[i]);
            }
        }
    
        public void Edit(int x, int y, GridTile tile)
        {
            Vector2Int position = new Vector2Int(x, y);
        
            if(_grid.TryGetValue(position, out var prev_tile))
                GameObject.Destroy(prev_tile.gameObject);
     
            _grid[position] = tile;
            tile.transform.position = new Vector3(x, y, 0);
        }
    }
}