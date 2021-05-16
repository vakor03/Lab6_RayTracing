using System;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Face testFace = new Face(new[] {new Point(0, 0, 0), new Point(0, 2, 0), new Point(2, 0, 0)});

            Triangle testTriangle = new Triangle (new Point(0.15, 0.1, -0.05), new Point(-0.14, 0.15, 0.08), new Point(0.5, -0.1, 0.2));
            //Ray ray1 = new(new(-1, 1.7000000000000002, 1.4000000000000001), new(5, 0, 0));
            //Ray ray2 = new(new(-1, 1.8, 1.4000000000000001), new(5, 0, 0));
            //Console.WriteLine(testTriangle.IsCrossesTriangle(ray1));
            //Console.WriteLine(testTriangle.IsCrossesTriangle(ray2));

            Point camera = new(0, 2, 0);
            Screen screen = new(100, 100, .005, camera, new(0, -1, 0));
            Point light = new(1, 2, 1);

            Renderer renderer = new(camera, screen, light);
            

            //screen.OutputToConsole();

            FileManager manager = new();
            renderer.Render(manager.ReadObj("cow.obj"));
            manager.WriteBMP("output.bmp", screen);

        }
    }
}