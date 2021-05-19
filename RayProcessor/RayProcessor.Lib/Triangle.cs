using System;
using System.Text.RegularExpressions;

namespace RayProcessor.Lib
{
    public class Triangle
    {
        private Point vertex1;
        private Point vertex2;
        private Point vertex3;
        public Point Normal { get; private set; }

        public Triangle(Point v1, Point v2, Point v3)
        {
            vertex1 = v1;
            vertex2 = v2;
            vertex3 = v3;
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

            double t = -(ray.StartPoint - vertex1).DotProduct(Normal)/tmp; //відстань від початку промення до точки перетину 
            Point pointOfInters = new Point(ray.StartPoint.x + ray.Vector.x * t,ray.StartPoint.y + ray.Vector.y * t,
                ray.StartPoint.z + ray.Vector.z * t); // точка перетину
            
            // u, v i t1 це барицентричні координати
            double u = (pointOfInters-vertex1).CrossProduct(e1).magnitude;
            u /= Normal.magnitude;

            if (u < 0 || u > 1)
            {
                return (false, new(0, 0, 0));
            }
            
            double v = (pointOfInters-vertex1).CrossProduct(e2).magnitude;
            v /= Normal.magnitude;
            
            if (v < 0 || v > 1 || u+v-epsilon>1)
            {
                return (false, new(0, 0, 0));
            }
            
            double t1 = (pointOfInters-vertex3).CrossProduct(vertex3-vertex2).magnitude;
            t1 /= Normal.magnitude;
            if (t1 < 0 || t1 > 1 || Math.Abs(u+v+t1-1)>epsilon)
            {
                return (false, new(0, 0, 0));
            }
            
            return (true, pointOfInters);
        }
    }
}