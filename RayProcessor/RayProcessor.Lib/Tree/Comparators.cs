using System.Collections.Generic;

namespace RayProcessor.Lib
{
    public class LeafComparerX : IComparer<Leaf>
    {
        public int Compare(Leaf leaf1, Leaf leaf2)
        {
            return leaf1.Triangle.Center.x.CompareTo(leaf2.Triangle.Center.x);
        }
    }
    
    public class LeafComparerY : IComparer<Leaf>
    {
        public int Compare(Leaf leaf1, Leaf leaf2)
        {
            return leaf1.Triangle.Center.y.CompareTo(leaf2.Triangle.Center.y);
        }
    }
    
    public class LeafComparerZ : IComparer<Leaf>
    {
        public int Compare(Leaf leaf1, Leaf leaf2)
        {
            return leaf1.Triangle.Center.z.CompareTo(leaf2.Triangle.Center.z);
        }
    }
}