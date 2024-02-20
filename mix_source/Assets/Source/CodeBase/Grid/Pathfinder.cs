using System;
using System.Collections.Generic;
using autumn_berries_mix.Grid;
using UnityEngine;

namespace autumn_berries_mix
{
    public sealed class Pathfinder
    {
        private GameGrid _grid;

        public Pathfinder(GameGrid grid)
        {
            _grid = grid;
        }

        private class PathNode : IEquatable<PathNode>
        {
            public PathNode(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            
            public PathNode PreviousPathNode { get; private set; }

            public float F => G + H;
            
            public float G { get; private set; } = 0;
            public float H { get; private set; } = 0;
            public int X { get; private set; }
            public int Y { get; private set; }

            public void SetCosts(float g, float h)
            {
                G = g; 
                H = h;
            }
            
            public void SetPreviousPoint(PathNode startingPathNode) => PreviousPathNode = startingPathNode;
            
            public void SetXY(Vector2Int point)
            {
                X = point.x;
                Y = point.y;
            }

            public bool Equals(PathNode node)
            {
                if (node == null)
                    return false;
                
                return node.X == this.X && node.Y == this.Y;
            }
        }
        
        private sealed class PointComparer : IComparer<PathNode>
        {
            public int Compare(PathNode x, PathNode y)
            {
                if (x.F > y.F) return 1;
                else if (x.F < y.F) return -1;
                return 0;
            }
        }

        private float DefineGCost(PathNode startPathNode, PathNode endPathNode)
        {
            return Vector2.Distance(new Vector2(startPathNode.X, startPathNode.Y), new Vector2(endPathNode.X, endPathNode.Y));
        }
        
        private int Heuristic(PathNode first, PathNode second) => Mathf.Abs(first.X - second.X) + Mathf.Abs(first.Y - second.Y);
        private bool CheckPointCollider(Vector2Int position)
        {
            if (_grid.Get(position.x, position.y).Walkable && _grid.Get(position.x, position.y).Empty || _grid.IsPlayerUnit(position.x, position.y))
                return true;

            return false;
        }
        private List<PathNode> GetNeighbourPoints(PathNode pathNode, List<PathNode> ignoredPoints)
        {
            List<PathNode> neighbourPoints = new List<PathNode>();

            GridTile[] pointsToCheck = _grid.GetConnections(pathNode.X, pathNode.Y);
            
            foreach (GridTile nextPoint in pointsToCheck)
            {
                var node = new PathNode(nextPoint.Position2Int.x, nextPoint.Position2Int.y);
                
                if (CheckPointCollider(nextPoint.Position2Int) && !ignoredPoints.Contains(node))
                {
                    neighbourPoints.Add(node);
                }
            }
            
            return neighbourPoints;
        }
        
        public List<Vector2Int> FindPath(Vector2 start, Vector2 end)
        {
            List<PathNode> nextPoints = new List<PathNode>();

            List<PathNode> visitedPoints = new List<PathNode>();
            
            PathNode startPathNode = new PathNode((int)start.x, (int)start.y);
            PathNode endPathNode = new PathNode((int)end.x, (int)end.y);

            PathNode currentPathNode = startPathNode;

            while (true)
            {
                if (currentPathNode.X == endPathNode.X && currentPathNode.Y == endPathNode.Y)
                    return RestorePath(currentPathNode);

                List<PathNode> neighbourPoints = GetNeighbourPoints(currentPathNode, visitedPoints);

                foreach (PathNode point in neighbourPoints)
                {
                    point.SetCosts(currentPathNode.G + DefineGCost(currentPathNode, point), Heuristic(point, endPathNode));
                    point.SetPreviousPoint(currentPathNode);
                    nextPoints.Add(point);

                    visitedPoints.Add(point);
                }
                
                nextPoints.Sort(new PointComparer());

                currentPathNode = nextPoints[0];
                nextPoints.Remove(currentPathNode);
            }
        }
        private List<Vector2Int> RestorePath(PathNode endPathNode)
        {
            PathNode current = endPathNode;
            List<Vector2Int> path = new List<Vector2Int>();

            do
            {
                path.Add(new Vector2Int(current.X, current.Y));
                current = current.PreviousPathNode;
            } while ((current.PreviousPathNode != null));

            path.Reverse();

            return path;
        }
    }
}