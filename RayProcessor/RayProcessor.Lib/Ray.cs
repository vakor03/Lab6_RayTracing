using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcesssor.Lib
{
    public class Ray
    {
        public Point Vector { get; }

        // camera point
        public Point StartPoint { get; }

        public Ray(Point vector, Point origin)
        {
            Vector = vector;
            StartPoint = point;
        }

        public override string ToString()
        {
            return $"Ray(vector: {vector}, origin {origin})";
        }

    }
}
