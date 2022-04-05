using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    internal class Node
    {
        public int F => G + H;
        public int X { get; }
        public int Y { get; }
        public int G = int.MaxValue, H;
        public bool Closed, Wall;
        public Node PreviousNode;
        
        public Node(int x, int y, bool wall)
        {
            X = x;
            Y = y;
            Wall = wall;
        }
        
        public void ResetNode()
        {
            Closed = false;
            G = int.MaxValue;
            H = 0;
            PreviousNode = null;
        }
        
        public List<Vector2> GetPathToRootNode()
        {
            var path = new List<Vector2>();
            path.Add(new Vector2(X + 0.5f, Y + 0.5f));

            if (PreviousNode != null)
                path.AddRange(PreviousNode.GetPathToRootNode());
        
            return path;
        }
    }
}