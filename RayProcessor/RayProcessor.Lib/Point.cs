using System;
using Microsoft.VisualBasic.CompilerServices;

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

        public Point Normalize()
        {
            return new(x / magnitude, y / magnitude, z / magnitude);
        }

        public static bool operator ==(Point vector1, Point vector2)
        {
            if (Math.Abs(vector1.x - vector2.x) <= 0.0000001 && Math.Abs(vector1.y - vector2.y) <= 0.0000001
                                                             && Math.Abs(vector1.z - vector2.z) <= 0.0000001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Point vector1, Point vector2)
        {
            return !(vector1 == vector2);
        }

        public Point RotateByAngle(Point rotationAnglesXYZrads)
        {
            // rotate around z
            double x =  this.x * Math.Cos(rotationAnglesXYZrads.z) - this.y * Math.Sin(rotationAnglesXYZrads.z);
            double y = this.x * Math.Sin(rotationAnglesXYZrads.z) + this.y * Math.Cos(rotationAnglesXYZrads.z);

            // around y
            double z = -x * Math.Sin(rotationAnglesXYZrads.y) + this.z * Math.Cos(rotationAnglesXYZrads.y);
            x = x * Math.Cos(rotationAnglesXYZrads.y) + this.z * Math.Sin(rotationAnglesXYZrads.y);

            // around x
            double yBeforeRot = y;
            y = y * Math.Cos(rotationAnglesXYZrads.x) - z * Math.Sin(rotationAnglesXYZrads.x);
            z = yBeforeRot * Math.Sin(rotationAnglesXYZrads.x) + z * Math.Cos(rotationAnglesXYZrads.x);

            return new(x, y, z);
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

        public double AngleInRadsWithOtherVec(Point v)
        {
            return Math.Acos(DotProduct(v) / v.magnitude / magnitude);
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

        public static Point operator *(double c, Point p)
        {
            return new(p.x * c, p.y * c, p.z * c);
        }

        public static Point operator -(Point p)
        {
            return new(-p.x, -p.y, -p.z);
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