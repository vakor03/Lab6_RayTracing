using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcessor.Lib
{
    public struct HitInfo
    {
        public bool hit;
        public Triangle triangle;
        public Point hitPoint;

        public HitInfo(bool hit, Triangle triangle, Point point)
        {
            this.hit = hit;
            this.triangle = triangle;
            this.hitPoint = point;
        }
    }
}
