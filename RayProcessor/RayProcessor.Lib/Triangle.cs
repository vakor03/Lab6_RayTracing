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
            vertex2 = v3;
        }

        public bool IsCrossesTriangle(Ray ray)
        {
            Point e1 = vertex1 - vertex2;
            Point e2 = vertex1 - vertex3;

            Normal = CrossProduct(e1, e2);

            double tmp = DotProduct(Normal, ray.Vector); // хз як це назвати???
            if (tmp < 0.000001) // промінь паралельний до трикутника
            {
                return false;
            }

            Point vectorTmp = ray.StartPoint - vertex1;
            double t = -DotProduct(vectorTmp, Normal); //відстань від початку промення до точки перетину 
            
            // u i v це барицентричні координати
            double u = VectorLenght(CrossProduct(e1, ray.Vector)) + VectorLenght(CrossProduct(e1, vectorTmp)); 
            u /= VectorLenght(Normal);

            if (u < 0 || u > 1)
            {
                return false;
            }
            
            double v = VectorLenght(CrossProduct(e2, ray.Vector)) + VectorLenght(CrossProduct(e2, vectorTmp));
            v /= VectorLenght(Normal);
            
            if (v < 0 || v > 1|| u+v>1)
            {
                return false;
            }
            
            return true;
        }

        private Point CrossProduct(Point vector1, Point vector2) //векторний добуток
        {
            double x = vector1.y * vector2.z - vector2.z * vector1.y;
            double y = (-1)*(vector1.x * vector2.z - vector2.z * vector1.x);
            double z = vector1.x * vector2.y - vector2.y * vector1.x;
            return new Point(x, y, z);
        }

        private double DotProduct(Point vector1, Point vector2) //скалярний добуток
        {
            return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
        }

        private double VectorLenght(Point vector)
        {
            return Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
    }
}