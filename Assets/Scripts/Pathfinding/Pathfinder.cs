using System;
using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinder
    {
        private const int StraightMovingCost = 10;
        private const int DiagonalMovingCost = 14;

        private readonly Level level;
        private Node[,] _nodes;
        private List<Node> _openedNodes;
        private Vector2Int _endPoint;
        
        public Pathfinder(Level level)
        {
            this.level = level;
            GenerateNodesFromLevel();
        }
        
        public List<Vector2> FindPath(Vector2 pointA, Vector2 pointB)
        {
            pointA.x = Mathf.Clamp((int)pointA.x, 0, _nodes.GetLength(0) - 1);
            pointA.y = Mathf.Clamp((int)pointA.y, 0, _nodes.GetLength(1) - 1);
            pointB.x = Mathf.Clamp((int)pointB.x, 0, _nodes.GetLength(0) - 1);
            pointB.y = Mathf.Clamp((int)pointB.y, 0, _nodes.GetLength(1) - 1);
            
            foreach (var node in _nodes)
            {
                node.ResetNode();
            }
        
            _openedNodes = new List<Node>();
            var firstNode = OpenNode(new Vector2Int(Mathf.RoundToInt(pointB.x), Mathf.RoundToInt(pointB.y)));
            firstNode.G = 0;
        
            _endPoint = new Vector2Int(Mathf.RoundToInt(pointA.x), Mathf.RoundToInt(pointA.y));
        
            while (_openedNodes.Count > 0)
            {
                var selectedNode = GetMinFNode();

                if (selectedNode.X == _endPoint.x && selectedNode.Y == _endPoint.y)
                {
                    var path = selectedNode.GetPathToRootNode();
                    path.RemoveAt(0);
                    if (path.Count == 0)
                        return null;
                    
                    #if UNITY_EDITOR
                        DebugDrawPath(path);
                    #endif
                    
                    return path;
                }
                CheckNodesAroundNode(selectedNode);
                selectedNode.Closed = true;
                _openedNodes.Remove(selectedNode);
            }

            return null;
        }

        private Node GetMinFNode()
        {
            var minNode = _openedNodes[0];
            foreach (var node in _openedNodes)
            {
                if (node.F < minNode.F)
                    minNode = node;
            }

            return minNode;
        }
        
        private Node OpenNode(Vector2Int position)
        {
            var node = _nodes[position.x, position.y];
            if (node.F == int.MaxValue && !node.Wall)
            {
                _openedNodes.Add(node);
                node.H = GetH(position);
            }
            return node;
        }
        
        private void CheckNodesAroundNode(Node node)
        {
            var levelCells = level.LevelTable;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                
                    var checkX = node.X + i;
                    var checkY = node.Y + j;

                    //out of bounds
                    if (checkX < 0 || checkY < 0 || checkX >= levelCells.GetLength(0) || checkY >= levelCells.GetLength(1))
                        continue;
                
                    //is diagonal path available
                    if(_nodes[checkX, node.Y].Wall || _nodes[node.X, checkY].Wall)
                        continue;

                    var checkNode = OpenNode(new Vector2Int(checkX, checkY));
                    if (checkNode.Closed)
                        continue;

                    int gCost = node.G;
                    if (i == 0 || j == 0)
                        gCost += StraightMovingCost;
                    else gCost += DiagonalMovingCost;

                    if (checkNode.G > gCost)
                    {
                        checkNode.G = gCost;
                        checkNode.PreviousNode = node;
                    }
                }
            }
        }
        
        private int GetH(Vector2Int point)
        {
            var rectWidth = Mathf.Abs(point.x - _endPoint.x);
            var rectHeight = Mathf.Abs(point.y - _endPoint.y);
            var delta = Mathf.Abs(rectWidth - rectHeight);
            return DiagonalMovingCost * Mathf.Min(rectWidth, rectHeight) + StraightMovingCost * delta;
        }
        
        private void GenerateNodesFromLevel()
        {
            var levelCells = level.LevelTable;
            _nodes = new Node[levelCells.GetLength(0),levelCells.GetLength(1)];
            for(int i = 0; i < levelCells.GetLength(0); i++)
            {
                for (int j = 0; j < levelCells.GetLength(1); j++)
                {
                    bool nodeIsWall = levelCells[i, j] == CellType.Wall;
                    var newNode = new Node(i, j, nodeIsWall);
                    _nodes[i, j] = newNode;
                }
            }
        }

        private static void DebugDrawPath(List<Vector2> path)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                var a = new Vector3(path[i].x, path[i].y);
                var b = new Vector3(path[i + 1].x, path[i + 1].y);
                Debug.DrawLine(a, b, Color.green, 0.3f);
            }
        }
    }
}
