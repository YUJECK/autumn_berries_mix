using System.Collections.Generic;
using autumn_berries_mix.EC;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class GridData
    {
        public int TilesCount => _tiles.Count;
        
        private readonly Dictionary<Vector2Int, GridTile> _tiles = new();
        private readonly List<Entity> _entities = new();
        private Dictionary<Vector2Int, List<GridTile>> _connections;
        
        public Entity[] Entities => _entities.ToArray();
        
        public GridData(GridTile[] tiles, Entity[] entities)
        {
            CreateFrom(tiles, entities);
        }

        public TCell Get<TCell>(int x, int y)
            where TCell : GridTile
        {
            Vector2Int position = new Vector2Int(x, y);
        
            return _tiles.GetValueOrDefault(position) as TCell;
        }
    
        public GridTile Get(int x, int y)
        {
            Vector2Int position = new Vector2Int(x, y);

            return _tiles.GetValueOrDefault(position);
        }
        
        public GridTile[] GetConnections(int x, int y)
        {
            Vector2Int position = new Vector2Int(x, y);

            return _connections[position].ToArray();
        }

        public void Edit(int x, int y, GridTile tile)
        {
            Vector2Int position = new Vector2Int(x, y);

            if (tile == null)
                return;
            
            if (_tiles.TryGetValue(position, out var prevTile))
            {
                if (_connections != null)
                {
                    foreach (var connectedTile in _connections[position])
                    {
                        if (!_connections.ContainsKey(connectedTile.Position2Int)) continue;
                        _connections[connectedTile.Position2Int].Remove(prevTile);
                        _connections[connectedTile.Position2Int].Add(tile);
                    }    
                }
                
                GameObject.Destroy(prevTile.gameObject);
            }
                
     
            _tiles[position] = tile;
            tile.transform.position = new Vector3(x, y, 0);
        }

        public void CreateFrom(GridTile[] tiles, Entity[] entities)
        {
            Vector2Int minimum = new Vector2Int();
            Vector2Int maximum = new Vector2Int();
            
            //tiles
            for (int i = 0; i < tiles.Length; i++)
            {
                int x = tiles[i].Position2Int.x;
                int y = tiles[i].Position2Int.y;

                if (minimum.x > x)
                    minimum.x = x;
                
                if (minimum.y > y)
                    minimum.y = y;
                
                if (maximum.x < x)
                    maximum.x = x;
                
                if (maximum.y < y)
                    maximum.y = y;
                
                Edit(x, y, tiles[i]);
            }

            //entities
            for (int i = 0; i < entities.Length; i++)
            {
                int x = entities[i].Position2Int.x;
                int y = entities[i].Position2Int.y;

                if (Get(x, y) != null)
                {
                    Get(x, y).Place(entities[i]);
                    _entities.Add(entities[i]);
                }
                else
                {
                    GameObject.Destroy(entities[i]);
                }
            }
            
            _connections = CreateGraph(_tiles, minimum, maximum);
        }

        private Dictionary<Vector2Int, List<GridTile>> CreateGraph(Dictionary<Vector2Int, GridTile> tiles, Vector2Int minimum, Vector2Int maximum)
        {
            Dictionary<Vector2Int, List<GridTile>> graph = new Dictionary<Vector2Int,  List<GridTile>>();

            Vector2Int[] offsets = new Vector2Int[8]
            {
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
                new Vector2Int(1, 1),
                new Vector2Int(1, -1),
                new Vector2Int(-1, -1),
                new Vector2Int(-1, 1),
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0)
            };

            foreach (var positionTilePair in tiles)
            {
                var tile = positionTilePair.Value;

                for (int i = 0; i < offsets.Length; i++)
                {
                    if (!tiles.ContainsKey(tile.Position2Int + offsets[i])) continue;
                        
                    if (graph.TryGetValue(tile.Position2Int, out List<GridTile> connections))
                    {
                        connections.Add(tiles[tile.Position2Int + offsets[i]]);
                        continue;
                    }
                    graph.Add(tile.Position2Int, new List<GridTile>(){ tiles[tile.Position2Int + offsets[i]] });
                }

            }

            return graph;
        }
    }
}