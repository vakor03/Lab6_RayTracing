using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcesssor.Lib
{
    class Ray
    {
        private Point vector;

        // camera point
        private Point origin;

        public Ray(Point vector, Point origin)
        {
            this.vector = vector;
            this.origin = origin;
        }

        public override string ToString()
        {
            return $"Ray(vector: {vector}, origin {origin})";
        }

    }
}
