using System;
using System.Collections.Generic;
using autumn_berries_mix.CallbackSystem.Signals;
using autumn_berries_mix.EC;
using autumn_berries_mix.Gameplay.Signals;
using autumn_berries_mix.Units;
using UnityEngine;

namespace autumn_berries_mix.Grid
{
    public class GameGrid : MonoBehaviour
    {
        public int TilesCount => GridData.TilesCount;
        private GridData GridData { get; set; }

        private void Awake()
        {
            if (GridData == null)
            {
                GridData = new GridData(GetChildrenTiles(), GetChildrenEntities());

                foreach (var entity in GridData.Entities)
                {
                    entity.InitGrid(this);
                }
            }
        }

        public bool IsPlayerUnit(int x, int y)
            => GridData.Get(x, y) != null && GridData.Get(x, y).TileStuff is PlayerUnit;

        public bool IsTileEmpty(int x, int y)
            => GridData.Get(x, y) != null && GridData.Get(x, y).Empty;
        
        public TCell Get<TCell>(int x, int y)
            where TCell : GridTile
        {
            return GridData.Get<TCell>(x, y);
        }
    
        public GridTile Get(Vector2Int pos)
            => GridData.Get(pos.x, pos.y);
        
        public GridTile Get(int x, int y)
            => GridData.Get(x, y);
        
        public GridTile[] GetConnections(int x, int y)
            => GridData.GetConnections(x, y);
        
        public GridTile[] GetConnections(int x, int y, int range)
        {
            List<GridTile> result = new();
            Queue<GridTile> next = new();
            
            next.Enqueue(Get(x, y));

            for (int i = 0; i < range; i++)
            {
                List<GridTile> onThisTurn = new();
                
                while(next.Count > 0)
                {
                    var connections = GridData.GetConnections(next.Peek().Position2Int.x, next.Peek().Position2Int.y);

                    foreach (var tile in connections)
                    {
                        if (!result.Contains(tile))
                        {
                            onThisTurn.Add(tile);
                            result.Add(tile);        
                        }
                    }

                    next.Dequeue();
                }

                foreach (var tile in onThisTurn)
                {
                    next.Enqueue(tile);
                }
            }
            
            return result.ToArray();
        }

        public TEntity PlaceEntityToEmptyFromPrefab<TEntity>(int x, int y, TEntity prefab)
            where TEntity : Entity
        {
            if(prefab == null || GridData.Get(x, y) == null || !GridData.Get(x, y).Empty)
                return null;

            var instance = Instantiate<TEntity>(prefab, transform);

            GridData.Get(x, y).Place(instance);

            return instance;
        }

        public TEntity ReplaceEntityFromPrefab<TEntity>(int x, int y, TEntity prefab)
            where TEntity : Entity
        {
            if(prefab == null || GridData.Get(x, y) == null)
                return null;

            var instance = GameObject.Instantiate<TEntity>(prefab, transform);

            GridData.Get(x, y).Place(instance);

            return instance;
        }

        public void SwapEntities(int x1, int y1, int x2, int y2)
        {
            if(x1 == x2 && y1 == y2)
                return;
            
            Entity first = GridData.Get(x1, y1).TileStuff;
            
            GridData.Get(x1, y1).Place(GridData.Get(x2, y2).TileStuff);
            GridData.Get(x2, y2).Place(first);
        }

        public void ReplaceEntity(Entity entity, Vector2Int to)
            => ReplaceEntity(entity, entity.Position2Int, to);

        public void ReplaceEntity(Entity entity, Vector2Int from, Vector2Int to)
        {
            int x1 = from.x;
            int y1 = from.y;

            if(Get(x1, y1).TileStuff != entity)
                return;
            
            int x2 = to.x;
            int y2 = to.y;
            
            if(!Get(x2, y2).Empty)
                return;

            GridData.Get(x2, y2).Place(entity);
            GridData.Get(x1, y1).Place(null);
            
            if(entity is Unit unit)
                SignalManager.PushSignal(new UnitMovedSignal(unit));
        }

        public void Edit(Vector2Int position, GridTile toTile)
        {
            GridData.Edit(position.x, position.y, toTile);
        }
        
        
        public void ClearTile(int x, int y)
        {
            if (!IsTileEmpty(x, y))
                GridData.Get(x, y).Clear();
        }

        private GridTile[] GetChildrenTiles()
            => GetComponentsInChildren<GridTile>();
        
        private Entity[] GetChildrenEntities()
            => GetComponentsInChildren<Entity>();
    }
}