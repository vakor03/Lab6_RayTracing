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
        char[,] pixels;

        public Screen(int width, int height)
        {
            pixels = new char[width, height];
        }
    }
}
