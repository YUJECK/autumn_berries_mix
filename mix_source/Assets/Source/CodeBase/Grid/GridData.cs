using System.Collections.Generic;
using autumn_berries_mix.EC;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class GridData
    {
        private readonly Dictionary<Vector2Int, GridTile> _grid;
    
        public GridData(GridTile[] tiles, Entity[] entities)
        {
            _grid = new Dictionary<Vector2Int, GridTile>();
            CreateFrom(tiles, entities);
        }

        public TCell Get<TCell>(int x, int y)
            where TCell : GridTile
        {
            Vector2Int position = new Vector2Int(x, y);
        
            return _grid.GetValueOrDefault(position) as TCell;
        }
    
        public GridTile Get(int x, int y)
        {
            Vector2Int position = new Vector2Int(x, y);

            return _grid.GetValueOrDefault(position);
        }

        public void CreateFrom(GridTile[] tiles, Entity[] entities)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                int x = Mathf.RoundToInt(tiles[i].transform.position.x);
                int y = Mathf.RoundToInt(tiles[i].transform.position.y);
                
                Edit(x, y, tiles[i]);
            }
            
            for (int i = 0; i < entities.Length; i++)
            {
                int x = Mathf.RoundToInt(entities[i].transform.position.x);
                int y = Mathf.RoundToInt(entities[i].transform.position.y);

                if (Get(x, y) != null)
                {
                    Get(x, y).Place(entities[i]);
                }
                else
                {
                    GameObject.Destroy(entities[i]);
                }
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