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
            //Point cameraPos = new(0, 2, 0);
            //Camera camera = new(100, 100, .005, cameraPos, new(0, 0, 180));
            //Point light = new(1, 2, 1);

            //
            // 
            // 
            //
            //
            //
            // Console.WriteLine(camera.GetPixelRay(0, 10));
            //Tree tree = new Tree();
            //FileManager fileManager = new FileManager();
            //fileManager.ReadObj(@"cow.obj", tree);
            //Renderer renderer = new(camera, light, tree);
            //renderer.Render();
            //fileManager.WriteBMP(@"output.bmp", camera);

            MBB box = new(new(0, 0, 2), new(1, 0, 0), new(0, 1, 0));
            Console.WriteLine(box.Intersects(new(new(0, 0, -1), new(0.5, 0.5, -22))));
        }
    }
}