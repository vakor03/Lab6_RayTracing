using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcesssor.Lib
{
    class Screen
    {
        // colors of each pixel
        // (0, 0) - bottom left
        char[,] pixels;
        private (int x, int y) pixelSize;

        // a normal to the screen, facing the direction we are looking at
        private Point normal;
        private Point screenCenter;

        public Screen(int width, int height, (int x, int y) pixelSize, Point normal, Point screenCenter)
        {
            pixels = new char[width, height];
            this.pixelSize = pixelSize;
            this.normal = normal;
            this.screenCenter = screenCenter;
        }

        public void SetPixel(char color, int x, int y)
        {
            pixels[y, x] = color;
        }
    }
}
