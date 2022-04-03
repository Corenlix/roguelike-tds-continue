using System;
using System.Collections.Generic;
using System.Linq;
using DelaunatorSharp;
using Random = UnityEngine.Random;

namespace LevelGeneration
{
    public class LevelGraph<T> where T : IPoint
    {
        public List<ExactEdge<T>> Edges => _edges;
    
        private readonly List<T> _mainRooms;
        private readonly float _extraConnections;
        private List<ExactEdge<T>> _edges;
        private Delaunator _delaunator;

        public LevelGraph(List<T> mainRooms, float extraConnections)
        {
            _mainRooms = mainRooms;
            _extraConnections = extraConnections;
            CreateMinimumSpanningTree();
            AddExtraEdges();
        }
    
        private void CreateMinimumSpanningTree()
        {
            CreateDelaunator();
            FillEdges(_delaunator.GetEdges());
            var graph = CreateGraph();
            var tree = new Dictionary<Graph<T>.Node, Graph<T>.Node>();
            new Prim<T>().prim(graph, graph.FindVertex(_mainRooms[0]), ref tree);

            _edges = new List<ExactEdge<T>>();
            foreach (var node in tree)
            {
                _edges.Add(new ExactEdge<T>(0, node.Key.context, node.Value.context));
            }
        }
    
        private void CreateDelaunator()
        {
            var points = new IPoint[_mainRooms.Count];
            for (var i = 0; i < _mainRooms.Count; i++)
            {
                points[i] = _mainRooms[i];
            }

            _delaunator = new Delaunator(points);
        }

        private void FillEdges(IEnumerable<IEdge> edges)
        {
            _edges = new List<ExactEdge<T>>();
            foreach (var edge in edges)
            {
                _edges.Add(new ExactEdge<T>(edge.Index, (T)edge.P, (T)edge.Q));
            }
        }
    
        private Graph<T> CreateGraph()
        {
            var graph = new Graph<T>(_mainRooms);
            foreach (var edge in _edges)
            {
                graph.SetEdge((T)edge.P, (T)edge.Q, (int)CalculateEdgeWeight(edge.P, edge.Q));
            }

            return graph;
        }

        private int CalculateEdgeWeight(IPoint firstPoint, IPoint secondPoint)
        {
            return (int)(Math.Abs(firstPoint.X - secondPoint.X) + Math.Abs(firstPoint.Y - secondPoint.Y));
        }

        private void AddExtraEdges()
        {
            foreach (var edge in _delaunator.GetEdges())
            {
                if ((Random.Range(0, 100) >= _extraConnections)) continue;
                if (_edges.FirstOrDefault(x => edge.P == x.P && edge.Q == x.Q || edge.P == x.Q && edge.Q == x.P) != null)
                    continue;
            
                _edges.Add(new ExactEdge<T>(edge.Index, (T)edge.P, (T)edge.Q));
            }
        }
    }
}
