using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcessor.Lib
{
    public class Screen
    {
        // colors of each pixel
        // (0, 0) - bottom left
        char[,] pixels;
        private double pixelSize;
        public (int width, int height) screenPixelSize;

        // a normal to the screen, facing the direction we are looking at
        private Point normal;
        private Point screenCenter;


        // width and height is the number of pixels
        //public Screen(int width, int height, double pixelSize, Point normal, Point screenCenter)
        //{
        //    pixels = new char[width, height];
        //    this.screenPixelSize = (width, height);
        //    this.pixelSize = pixelSize;
        //    this.normal = normal;
        //    this.screenCenter = screenCenter;
        //}


        // cameraVector starts in camera and goes to screen center point
        // screen is perpendicular to this vector
        // so, basically, it sets the direction the camera is facing and the distance
        // of the screen to the camera

        // (width, height) and pixelSize don't depend on each other
        // if (width, height) stays the same and pixelSize increases
        // then we divide our screen into more rectangles
        // if pixelSize stays the same and (width, height) increases
        // (provided that cameraVector stays the same)
        // we will see a bigger portion of the picture, but in a worse resolution
        public Screen(int width, int height, double pixelSize, Point camera, Point cameraVector)
        {
            pixels = new char[width, height];
            this.screenPixelSize = (width, height);
            this.pixelSize = pixelSize;
            this.normal = cameraVector;
            this.screenCenter = camera + cameraVector;
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
            return new Point(screenCenter.x + pixelX * directionalSinuses.x * pixelSize,
                screenCenter.y + pixelX * directionalSinuses.y * pixelSize,
                screenCenter.z + pixelY * directionalSinuses.z * pixelSize);
        }

        public void OutputToConsole()
        {
            for (int i = screenPixelSize.height - 1; i >= 0; i--)
            {
                for (int j = screenPixelSize.width - 1; j >= 0; j--)
                {
                    Console.Write(pixels[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
