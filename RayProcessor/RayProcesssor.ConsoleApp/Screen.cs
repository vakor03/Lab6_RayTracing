using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcesssor.ConsoleApp
{
    class Screen
    {
        // colors of each pixel
        // (0, 0) - bottom left
        char[,] pixels;

        public Screen(int width, int height)
        {
            pixels = new char[width, height];
        }

        public void SetPixel(char color, int x, int y)
        {
            pixels[y, x] = color;
        }
    }
}
