using System;
using System.Diagnostics;
using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    static class Program
    {
        
        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Point cameraPos = new(0, 2, 0);
            Camera camera = new(1280, 720, .0005, cameraPos, new(0, 0, 0));
            Point light = new(0.6, 1.6, 0.6);
            Tree tree = new Tree();
            FileManager fileManager = new FileManager();
            fileManager.ReadObj(@"cow.obj", tree);
            Renderer renderer = new(camera, light, tree);
            renderer.Render();
            fileManager.WriteBMP(@"output.bmp", camera);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds/1000+" s");
        }
    }
}