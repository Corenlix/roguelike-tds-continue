using DelaunatorSharp;

namespace LevelGeneration
{
    public class ExactEdge<T> : IEdge where T : IPoint
    {
        public IPoint P { get; }
        public IPoint Q { get; }
        public int Index { get; }

        public ExactEdge(int index, T p, T q)
        {
            Index = index;
            P = p;
            Q = q;
        }
    }
}
