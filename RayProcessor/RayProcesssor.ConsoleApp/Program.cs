using System;
using System.Diagnostics;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        public static Triangle floor = new Triangle(new Point(10, 10, -0.5), new Point(-10, 0, -0.5), new Point(0, -10, -0.5));
        static void Main(string[] args)
        {
            Point cameraPos = new(0, 2, 0);
            Camera camera = new(100, 100, .005, cameraPos, new(0, 0, 180));
            Point light = new(1, 2, 1);
            FileManager manager = new();
            var triangles = manager.ReadObj(@"cow.obj");
            triangles.Add(floor);
            Renderer renderer = new(camera, light, triangles);

            renderer.Render();
            manager.WriteBMP(@"output.bmp", camera);

            

            Console.WriteLine(camera.GetPixelRay(0, 10));

        }
    }
}