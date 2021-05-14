using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcesssor.Lib
{
    public class Screen
    {
        // colors of each pixel
        // (0, 0) - bottom left
        char[,] pixels;
        private (int width, int height) pixelSize;
        private (int width, int height) screenPixelSize;

        // a normal to the screen, facing the direction we are looking at
        private Point normal;
        private Point screenCenter;

        // width and height is the number of pixels
        public Screen(int width, int height, (int width, int height) pixelSize, Point normal, Point screenCenter)
        {
            pixels = new char[width, height];
            this.screenPixelSize = (width, height);
            this.pixelSize = pixelSize;
            this.normal = normal;
            this.screenCenter = screenCenter;
        }

        public void SetPixel(char color, int x, int y)
        {
            pixels[y, x] = color;
        }

        public Point GetPixelXYZPoint(int pixelX, int pixelY)
        {
            Point directionalSinuses = normal.GetDirectionalSinuses();

            pixelX -= screenPixelSize.width / 2;
            pixelY -= screenPixelSize.height / 2;
            return new Point(screenCenter.x + pixelX * directionalSinuses.x * pixelSize.width,
                screenCenter.y + pixelX * directionalSinuses.y * pixelSize.width,
                screenCenter.z + pixelY * directionalSinuses.z * pixelSize.height);
        }
    }
}
