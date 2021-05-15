using System;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Face testFace = new Face(new[] {new Point(0, 0, 0), new Point(0, 2, 0), new Point(2, 0, 0)});

            Triangle testTriangle = new Triangle (new Point(0, 0, 0), new Point(2, 0, 0), new Point(0, 2, 0));
            Ray ray = new Ray(new(0, 0, -1), new(0.1, 0.1, 2));
            Console.WriteLine(testTriangle.IsCrossesTriangle(ray));

            Point camera = new(5, 5, 5);
            Screen screen = new(50, 50, 1, camera, new(0, 0, -1));

            Renderer renderer = new(camera, screen);
            renderer.Render(new() { testTriangle });
            screen.OutputToConsole();

            
        }
    }
}