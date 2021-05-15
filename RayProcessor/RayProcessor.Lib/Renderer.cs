using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcessor.Lib
{
    public class Renderer
    {
        private Point camera;
        private Screen screen;

        public Renderer(Point camera, Screen screen)
        {
            this.screen = screen;
            this.camera = camera;
        }

        private HitInfo FireRay(int pixelX, int pixelY, List<Triangle> triangles)
        {
            Point pointOnScreen = screen.GetPixelXYZPoint(pixelX, pixelY);
            Ray rayToFire = new(pointOnScreen - camera, camera);
            //Console.WriteLine(rayToFire);

            HitInfo closestHit = new(false, null, new(0, 0, 0));
            double closestDistance = double.PositiveInfinity;

            foreach (Triangle triangle in triangles)
            {
                (bool intersected, Point intersection) = triangle.IsCrossesTriangle(rayToFire);
                if (intersected)
                {
                    //Console.WriteLine("HIT");
                    double distance = camera.DistanceToOtherPoint(intersection);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestHit = new(true, triangle, intersection);
                    }
                }
            }

            return closestHit;
        }


        private char GetPixelByHitInfo(HitInfo info)
        {
            return info.hit ? '1' : '0';
        }

        public void Render(List<Triangle> triangles)
        {
            // fire ray from each pixel
            for (int i = 0; i < screen.screenPixelSize.height; i++)
            {
                for (int j = 0; j < screen.screenPixelSize.width; j++)
                {
                    HitInfo info = FireRay(j, i, triangles);
                    screen.SetPixel(GetPixelByHitInfo(info), j, i);
                }
            }
        }
    }
}
