using System;
using System.Diagnostics;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Point camera = new(0, 2, 0);
            Screen screen = new(100, 100, .005, camera, new Point(0.25, 1, -0.25), 
                new Point(0.25, 1, 0.25), new Point(-0.25, 1, 0.25));
            Point light = new(1, 2, 1);
            FileManager manager = new();
            Renderer renderer = new(camera, screen, light, manager.ReadObj(@"C:\Users\user\Desktop\cow.obj"));

            renderer.Render();
            manager.WriteBMP(@"C:\Users\user\Desktop\output.bmp", screen);

        }
    }
}