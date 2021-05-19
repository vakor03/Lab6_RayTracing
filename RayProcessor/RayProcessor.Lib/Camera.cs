using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcessor.Lib
{
    // CURRENTLY WORKS ONLY ALIGNED WITH Z AXIS (i guess)
    public class Camera
    {
        // colors of each pixel
        // (0, 0) - bottom left
        public double[,] pixels { get; private set; }
        private double pixelSize;
        public (int width, int height) screenPixelSize;

        // a normal to the screen, facing the direction we are looking at
        private Point normal;
        private Point cameraPoint;
        private Point radDegrees;

        // for determining pixel points
        private Point rayShootStartingPoint;
        
        private Point xDir;
        private Point yDir;

        // cameraVector starts in camera and goes to screen center point
        // screen is perpendicular to this vector
        // so, basically, it sets the direction the camera is facing and the distance
        // of the screen to the camera

        public Camera(int width, int height, double pixelSize, Point cameraPoint, Point xyzRotationDegrees)
        {
            pixels = new double[height, width];
            screenPixelSize = (width, height);
            this.pixelSize = pixelSize;
            this.cameraPoint = cameraPoint;
            this.radDegrees = new(xyzRotationDegrees.x / 180 * Math.PI,
                xyzRotationDegrees.y / 180 * Math.PI,
                xyzRotationDegrees.z / 180 * Math.PI);


            Point defaultScreenXDir = new Point(1, 0, 0);
            Point defaultScreenYDir = new Point(0, 0, 1);
            xDir = defaultScreenXDir.RotateByAngle(radDegrees);
            yDir = defaultScreenYDir.RotateByAngle(radDegrees);

            Point defaultViewDirectionVec = new Point(0, 1, 0);
            Point rayShootBackOffset = -defaultViewDirectionVec.RotateByAngle(radDegrees);
            rayShootStartingPoint = cameraPoint + rayShootBackOffset;
        }


        public void SetPixel(double shade, int x, int y)
        {
            pixels[y, x] = shade;
        }

        private Point GetPixelXYZPoint(int pixelX, int pixelY)
        {
            pixelX -= screenPixelSize.width / 2;
            pixelY -= screenPixelSize.height / 2;

            return cameraPoint + pixelSize * (pixelX * xDir + pixelY * yDir);
        }

        public Ray GetPixelRay(int pixelX, int pixelY)
        {
            Point pixelPoint = GetPixelXYZPoint(pixelX, pixelY);
            Ray ray = new(pixelPoint - rayShootStartingPoint, rayShootStartingPoint);
            return ray;
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
