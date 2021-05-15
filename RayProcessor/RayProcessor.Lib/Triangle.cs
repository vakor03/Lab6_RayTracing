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

        public (bool intersects, Point pointOfIntersection) IsCrossesTriangle(Ray ray)
        {
            double epsilon = 0.00000001;
            Point e1 = vertex1 - vertex2;
            Point e2 = vertex1 - vertex3;

            Normal = CrossProduct(e1, e2);
            
            double tmp = DotProduct(Normal, ray.Vector); // хз як це назвати???
            if (Math.Abs(tmp) < epsilon) // промінь паралельний до трикутника
            {
                // if the first value is false, the point doesn't matter
                return (false, new(0, 0, 0));
            }

            Point vectorTmp = ray.StartPoint - vertex1 ;
            double t = -DotProduct(vectorTmp, Normal)/tmp; //відстань від початку промення до точки перетину 
            Point pointOfInters = new Point(ray.StartPoint.x + ray.Vector.x * t,ray.StartPoint.y + ray.Vector.y * t,
                ray.StartPoint.z + ray.Vector.z * t); // точка перетину
            
            // u, v i t1 це барицентричні координати
            double u = CrossProduct(pointOfInters-vertex1, e1).magnitude;
            u /= Normal.magnitude;

            if (u < 0 || u > 1)
            {
                return (false, new(0, 0, 0));
            }
            
            double v = CrossProduct(pointOfInters-vertex1, e2).magnitude;
            v /= Normal.magnitude;
            
            if (v < 0 || v > 1 || u+v-epsilon>1)
            {
                return (false, new(0, 0, 0));
            }
            
            double t1 = CrossProduct(pointOfInters-vertex3, vertex3-vertex2).magnitude;
            t1 /= Normal.magnitude;
            if (t1 < 0 || t1 > 1 || Math.Abs(u+v+t1-1)>epsilon)
            {
                return (false, new(0, 0, 0));
            }
            
            return (true, pointOfInters);
        }

        private Point CrossProduct(Point vector1, Point vector2) //векторний добуток
        {
            double x = vector1.y * vector2.z - vector2.y * vector1.z;
            double y = (-1)*(vector1.x * vector2.z - vector2.x * vector1.z);
            double z = vector1.x * vector2.y - vector2.x * vector1.y;
            return new Point(x, y, z);
        }

        private double DotProduct(Point vector1, Point vector2) //скалярний добуток
        {
            return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
        }
    }
}