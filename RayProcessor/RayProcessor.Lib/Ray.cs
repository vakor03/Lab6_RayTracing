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
        private Point point;

        public Ray(Point vector, Point point)
        {
            this.vector = vector;
            this.point = point;
        }
    }
}
