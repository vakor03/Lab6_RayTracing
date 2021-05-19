using System;
using System.Diagnostics;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Point cameraPos = new(0, 3, 3);
            Camera camera = new(100, 100, .005, cameraPos, new(45, 0, 180));
            Point light = new(1, 2, 1);
            FileManager manager = new();
            var triangles = manager.ReadObj(@"cow.obj");
            //triangles.Add(new Triangle(new Point(10, 10, -0.05), new Point(0, -10, -0.05), new Point(-10, 0, -0.05)));
            Renderer renderer = new(camera, light, triangles);

            renderer.Render();
            manager.WriteBMP(@"output.bmp", camera);

            

            Console.WriteLine(camera.GetPixelRay(0, 10));

        }
    }
}