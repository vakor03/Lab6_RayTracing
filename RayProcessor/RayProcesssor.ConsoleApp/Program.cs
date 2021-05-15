using System;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Face testFace = new Face(new[] {new Point(0, 0, 0), new Point(0, 2, 0), new Point(2, 0, 0)});

            Triangle testTriangle = new Triangle (new Point(0, 0, 0), new Point(0, 20, 0), new Point(0, 0, 20));
            Triangle testTriangle1 = new Triangle(new Point(0, -20, 50), new Point(0, 20, 50), new Point(0, 5, 30));
            //Ray ray1 = new(new(-1, 1.7000000000000002, 1.4000000000000001), new(5, 0, 0));
            //Ray ray2 = new(new(-1, 1.8, 1.4000000000000001), new(5, 0, 0));
            //Console.WriteLine(testTriangle.IsCrossesTriangle(ray1));
            //Console.WriteLine(testTriangle.IsCrossesTriangle(ray2));

            Point camera = new(5, 0, 0);
            Screen screen = new(50, 500, .1, camera, new(-1, 0, 0));

            Renderer renderer = new(camera, screen);
            renderer.Render(new() { testTriangle, testTriangle1 });

            //screen.OutputToConsole();

            FileManager manager = new();
            manager.WriteBMP("output.bmp", screen);

        }
    }
}