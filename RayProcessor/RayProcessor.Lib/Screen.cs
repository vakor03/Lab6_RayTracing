using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcessor.Lib
{
    // CURRENTLY WORKS ONLY ALIGNED WITH Z AXIS (i guess)
    public class Screen
    {
        // colors of each pixel
        // (0, 0) - bottom left
        public double[,] pixels { get; private set; }
        private double pixelSize;
        public (int width, int height) screenPixelSize;

        // a normal to the screen, facing the direction we are looking at
        private Point normal;
        private Point screenCenter;
        private Point[] screenCoordinates;


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
        
        public Screen(int width, int height, double pixelSize, Point camera, Point a, Point b, Point c)
        {
            pixels = new double[height, width];
            this.screenPixelSize = (width, height);
            this.pixelSize = pixelSize;
            this.normal = (a-b).CrossProduct(c-b);
            this.screenCenter = camera + normal;
            screenCoordinates = new Point[4];
            screenCoordinates[0] = a;
            screenCoordinates[1] = b;
            screenCoordinates[2] = c;
            screenCoordinates[3] = screenCenter + screenCenter - b;
        }
        
        public void SetPixel(double shade, int x, int y)
        {
            pixels[y, x] = shade;
        }

        public Point GetPixelXYZPoint(int pixelX, int pixelY)
        {
            //Point directionalSinuses = normal.GetDirectionalSinuses();

            // pixelX -= screenPixelSize.width / 2;
            // pixelY -= screenPixelSize.height / 2;
            Point vectorBC = screenCoordinates[2] - screenCoordinates[1];
            Point shiftToRight = new Point(vectorBC.x * pixelX / screenPixelSize.width  + screenCoordinates[1].x,
                vectorBC.y  * pixelX / screenPixelSize.width + screenCoordinates[1].y,
                vectorBC.z  * pixelX / screenPixelSize.width + screenCoordinates[1].z);
            
            Point vectorBA = screenCoordinates[0] - screenCoordinates[1];
            Point shiftDown = new Point(vectorBA.x* pixelY / screenPixelSize.height  + shiftToRight.x,
                vectorBA.y * pixelY/ screenPixelSize.height  + shiftToRight.y,
                vectorBA.z * pixelY / screenPixelSize.height + shiftToRight.z);
            return shiftDown;
        }

        public void OutputToConsole()
        {
            for (int i = screenPixelSize.height - 1; i >= 0; i--)
            {
                for (int j = screenPixelSize.width - 1; j >= 0; j--)
                {
                    if (i == screenPixelSize.height / 2 || j == screenPixelSize.width / 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(pixels[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
    }
}
