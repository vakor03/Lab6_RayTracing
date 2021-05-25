using RayProcessor.Lib;

namespace RayProcesssor.ConsoleApp
{
    static class Program
    {
        
        static void Main()
        {
            Point cameraPos = new(2, 0, 0);
            Camera camera = new(1280, 720, .0005, cameraPos, new(0, 0, 90));
            Point light = new(1, 2, 1);
            Tree tree = new Tree();
            FileManager fileManager = new FileManager();
            fileManager.ReadObj(@"cow.obj", tree);
            Renderer renderer = new(camera, light, tree);
            renderer.Render();
            fileManager.WriteBMP(@"output.bmp", camera);
        }
    }
}