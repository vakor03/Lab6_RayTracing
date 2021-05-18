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
        private Point light;
        private List<Triangle> triangles;

        public Renderer(Point camera, Screen screen, Point light, List<Triangle> triangles)
        {
            this.screen = screen;
            this.camera = camera;
            this.light = light;
            this.triangles = triangles;
        }


        private bool previousHit = false;
        private Ray prray = null;
        private bool prePreviousHit = false;
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
                    double distance = camera.DistanceToOtherPoint(intersection);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestHit = new(true, triangle, intersection);
                    }
                }
            }

            //if (prePreviousHit && !previousHit && closestHit.hit)
            //{
            //    Console.WriteLine(prray);
            //    Console.WriteLine(rayToFire);
            //    Console.WriteLine();
            //}

            prePreviousHit = previousHit;
            previousHit = closestHit.hit;
            prray = rayToFire;

            return closestHit;
        }


        private double GetPixelByHitInfo(HitInfo info)
        {
            if (info.hit)
            {
                Ray RayL = new Ray(light - info.hitPoint, info.hitPoint);
                foreach (Triangle triangle in triangles)
                {
                    (bool intersected, Point intersection) = triangle.IsCrossesTriangle(RayL);
                    if (intersected && !triangle.Equals(info.triangle) && (light-intersection).magnitude<RayL.Vector.magnitude)
                    {
                        return 0;
                    }
                }
                double cos = RayL.Vector.DotProduct(info.triangle.Normal);
                cos /= RayL.Vector.magnitude * info.triangle.Normal.magnitude;
                if (cos < 0)
                {
                    return 0;
                }
                return cos / (RayL.Vector.magnitude*RayL.Vector.magnitude);
            }
            return 0;
        }

        public void Render()
        {
            // fire ray from each pixel
            for (int i = 0; i < screen.screenPixelSize.height; i++)
            {
                Console.WriteLine($"{(double)i / screen.screenPixelSize.height * 100} %");
                for (int j = 0; j < screen.screenPixelSize.width; j++)
                {
                    HitInfo info = FireRay(j, i, triangles);
                    screen.SetPixel(GetPixelByHitInfo(info), j, i);
                }
            }
        }
    }
}
