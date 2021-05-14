using System.Drawing;

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
    }
}