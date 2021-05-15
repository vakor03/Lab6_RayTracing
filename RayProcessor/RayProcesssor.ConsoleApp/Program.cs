using System;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Face testFace = new Face(new[] {new Point(0, 0, 0), new Point(0, 2, 0), new Point(2, 0, 0)});

            Triangle testTriangle = new Triangle (new Point(0, 0, 2), new Point(2, 0, 2), new Point(0, 2, 2));
            Ray testRay = new Ray(new Point(0, 0, 1), new Point(0, 0, 0));

            Console.WriteLine(testTriangle.IsCrossesTriangle(testRay));
        }
    }
}