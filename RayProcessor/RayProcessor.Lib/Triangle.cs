using System;

namespace RayProcessor.Lib
{
    public class Triangle
    {
        public Point[] Points => new []{vertex1, vertex2, vertex3};
        private readonly Point vertex1;
        private readonly Point vertex2;
        private readonly Point vertex3;
        public Point Center { get; private set; }
        public Point Normal { get; private set; }

        public Triangle(Point v1, Point v2, Point v3)
        {
            vertex1 = v1;
            vertex2 = v2;
            vertex3 = v3;
            GetCenter();
        }

        public (bool intersects, Point pointOfIntersection) CrossesTriangle(Ray ray)
        {
            double epsilon = 0.00000001;
            Point e1 = vertex1 - vertex2;
            Point e2 = vertex1 - vertex3;

            Normal = e1.CrossProduct(e2);

            double tmp = Normal.DotProduct(ray.Vector); // хз як це назвати???
            if (Math.Abs(tmp) < epsilon) // промінь паралельний до трикутника
            {
                // if the first value is false, the point doesn't matter
                return (false, new(0, 0, 0));
            }

            double t = -(ray.StartPoint - vertex1).DotProduct(Normal) /
                       tmp; //відстань від початку промення до точки перетину 
            Point pointOfInters = new Point(ray.StartPoint.x + ray.Vector.x * t, ray.StartPoint.y + ray.Vector.y * t,
                ray.StartPoint.z + ray.Vector.z * t); // точка перетину

            if (!(ray.Vector.Normalize() == (pointOfInters - ray.StartPoint).Normalize()))
            {
                return (false, new(0, 0, 0));
            }

            // u, v i t1 це барицентричні координати
            double u = (pointOfInters - vertex1).CrossProduct(e1).magnitude;
            u /= Normal.magnitude;

            if (u < 0 || u > 1)
            {
                return (false, new(0, 0, 0));
            }

            double v = (pointOfInters - vertex1).CrossProduct(e2).magnitude;
            v /= Normal.magnitude;

            if (v < 0 || v > 1 || u + v - epsilon > 1)
            {
                return (false, new(0, 0, 0));
            }

            double t1 = (pointOfInters - vertex3).CrossProduct(vertex3 - vertex2).magnitude;
            t1 /= Normal.magnitude;
            if (t1 < 0 || t1 > 1 || Math.Abs(u + v + t1 - 1) > epsilon)
            {
                return (false, new(0, 0, 0));
            }

            return (true, pointOfInters);
        }

        private void GetCenter()
        {
            //vertex1 - A, vertex2-B, vertex3-C
            Point middleOfAB = (vertex1 + vertex2);
            middleOfAB = new Point(middleOfAB.x / 2, middleOfAB.y / 2, middleOfAB.z / 2);

            Point middleOfBC = (vertex2 + vertex3);
            middleOfBC = new Point(middleOfBC.x / 2, middleOfBC.y / 2, middleOfBC.z / 2);

            // t from parametric equation of the line
            double t = (vertex3.x - vertex1.x) / (middleOfBC.x - vertex1.x - middleOfAB.x + vertex3.x);
            double x = vertex3.x + t * (middleOfAB.x - vertex3.x);
            double y = vertex3.y + t * (middleOfAB.y - vertex3.y);
            double z = vertex3.z + t * (middleOfAB.z - vertex3.z);

            Center = new Point(x, y, z);
        }
    }
}