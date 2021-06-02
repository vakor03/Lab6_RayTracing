using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcessor.Lib
{
    public struct HitInfo
    {
        public bool Hit;
        public Triangle Triangle;
        public Point HitPoint;

        public HitInfo(bool hit, Triangle triangle, Point point)
        {
            Hit = hit;
            Triangle = triangle;
            HitPoint = point;
        }
    }
}
