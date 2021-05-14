﻿using System;
using RayProcesssor.Lib;

namespace RayProcessor.Lib
{
    public class Sphere
    {
        private Point center;
        private double radius;

        public Sphere(Point center, double radius)
        {
            this.radius = radius;
            this.center = center;
        }
        
        public bool RayCrossesSphere(Ray ray)
        {
            Point s = new Point(center.x - ray.StartPoint.x, center.y - ray.StartPoint.y, center.y - ray.StartPoint.y);
            double b = 2.0 * ScalarProduct(s, ray.Vector);
            double c = ScalarProduct(ray.Vector, ray.Vector) - (radius * radius);
            
            double discriminant = b * b - 4 * c;
            
            if(discriminant < 0.0f)
                return false;

            discriminant = Math.Sqrt(discriminant);

            double s0 = (-b + discriminant) / 2;
            double s1 = (-b - discriminant) / 2;
            if(s0 >= 0 || s1 >= 0)
                return true;

            return false;
        }

        private double ScalarProduct(Point vector1, Point vector2)
        {
            return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
        }
    }
}