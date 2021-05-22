using System.Collections.Generic;

namespace RayProcessor.Lib
{
    public class TriangleComparerX : IComparer<Triangle>
    {
        public int Compare(Triangle triangle1, Triangle triangle2)
        {
            return triangle1.Center.x.CompareTo(triangle2.Center.x);
        }
    }
    
    public class TriangleComparerY : IComparer<Triangle>
    {
        public int Compare(Triangle triangle1, Triangle triangle2)
        {
            return triangle1.Center.y.CompareTo(triangle2.Center.y);
        }
    }
    
    public class TriangleComparerZ : IComparer<Triangle>
    {
        public int Compare(Triangle triangle1, Triangle triangle2)
        {
            return triangle1.Center.z.CompareTo(triangle2.Center.z);
        }
    }
}