using System;
using RayProcesssor.Lib;

namespace RayProcesssor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Screen screen = new(50, 100, (1, 1), new(1, 1, 0), new(0, 0, 0));
            Console.WriteLine(screen.GetPixelXYZPoint(50, 100));
        }
    }
}