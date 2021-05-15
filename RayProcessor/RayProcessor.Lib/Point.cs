using System;
namespace RayProcessor.Lib
{
    public struct Point
    {
        public double x;
        public double y;
        public double z;

        double magnitude;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            magnitude = Math.Sqrt(x * x + y * y + z * z);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
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

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }
    }
}