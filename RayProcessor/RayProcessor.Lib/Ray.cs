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

        public Ray(Point point1, Point point2)
        {
            vector = new(point2.x - point1.x, point2.y - point1.y, point2.z - point1.z);
            point = point1;
        }
    }
}
