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
            Ray ray = new(new(-1, 0, -.1), new(1, 0, 0));

            Point camera = new(5, 0, 0);
            Screen screen = new(50, 50, .5, camera, new(-1, 0, 0));

            Renderer renderer = new(camera, screen);
            renderer.Render(new() { testTriangle , testTriangle1});
            screen.OutputToConsole();

            
        }
    }
}