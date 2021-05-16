using System;
namespace RayProcessor.Lib
{
    public struct Point
    {
        public readonly double x;
        public readonly double y;
        public readonly double z;
        public readonly double magnitude;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            magnitude = Math.Sqrt(x * x + y * y + z * z);
        }

        public double DistanceToOtherPoint(Point point)
        {
            return Math.Sqrt(Math.Pow(x - point.x, 2) +
                Math.Pow(y - point.y, 2) +
                Math.Pow(z - point.z, 2));
        }
        
        public Point CrossProduct(Point vector) //векторний добуток
        {
            double x = this.y * vector.z - vector.y * this.z;
            double y = (-1)*(this.x * vector.z - vector.x * this.z);
            double z = this.x * vector.y - vector.x * this.y;
            return new Point(x, y, z);
        }
        
        public double DotProduct(Point vector1) //скалярний добуток
        {
            return vector1.x * this.x + vector1.y * this.y + vector1.z * this.z;
        }

        public Point GetDirectionalСosinuses()
        {

            return new Point(x / magnitude, y / magnitude, z / magnitude);
        }

        public Point GetDirectionalSinuses()
        {
            Point directionalCosinuses = GetDirectionalСosinuses();
            return new Point(Math.Sqrt(1-Math.Pow(directionalCosinuses.x, 2)),
                Math.Sqrt(1 - Math.Pow(directionalCosinuses.y, 2)),
                Math.Sqrt(1 - Math.Pow(directionalCosinuses.z, 2)));
        }

        public static Point operator+(Point a, Point b)
        {
            return new(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
    }
}