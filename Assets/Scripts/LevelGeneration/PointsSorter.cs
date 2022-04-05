using System.Collections.Generic;
using DelaunatorSharp;
using System.Linq;
using UnityEngine;

namespace LevelGeneration
{
    public class PointsSorter<T> where T : IPoint
    {
        private List<ExactEdge<T>> _edges;
        private List<T> _checkedRooms;
        private List<T> _pointsToCheck;
        
        public List<T> Sort(List<ExactEdge<T>> edges)
        {
            _edges = edges;
            _checkedRooms = new List<T>();
            _pointsToCheck = new List<T>();

            AddFirstNode();
            FindPaths();

            return _checkedRooms;
        }

        private void AddFirstNode()
        {
            var firstRoom = ((T) _edges.First(x =>_edges.FirstOrDefault(y => y.Q == x.P) == null).P);
            _pointsToCheck.Add(firstRoom);
        }

        private void FindPaths()
        {
            while (_pointsToCheck.Count > 0)
            {
                _checkedRooms.AddRange(_pointsToCheck);
                var currentPointsToCheck = _pointsToCheck.ToList();
                _pointsToCheck = new List<T>();
                foreach (var point in currentPointsToCheck)
                {
                    OpenNode(point);
                }
            }
        }
        
        private void OpenNode(T node)
        {
            var edgesFromPoint = _edges.Where(x => x.P.Equals(node) && !_checkedRooms.Contains((T)x.Q) || x.Q.Equals(node) && !_checkedRooms.Contains((T)x.P));
            foreach (var edge in edgesFromPoint)
            {
                _pointsToCheck.Add(edge.Q.Equals(node) ? (T)edge.P : (T)edge.Q);
            }
        }
    }
}