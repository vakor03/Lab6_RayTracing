using System;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Screen screen = new(50, 100, (1, 1), new(1, 1, 0), new(0, 0, 0));
            Sphere sphere = new(new(0, 100, 0), 50);
            Ray ray = new(new(1, 0, 0), new(0, 0, 0));
            Console.WriteLine(sphere.RayCrossesSphere(ray));
        }
    }
}