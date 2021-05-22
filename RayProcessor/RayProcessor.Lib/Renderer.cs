using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcessor.Lib
{
    public class Renderer
    {
        private Camera camera;
        private Point light;
        private List<Triangle> triangles;

        public Renderer(Camera camera, Point light, List<Triangle> triangles)
        {
            this.camera = camera;
            this.light = light;
            this.triangles = triangles;
        }


        private HitInfo FireRay(int pixelX, int pixelY, List<Triangle> triangles)
        {
            Ray rayToFire = camera.GetPixelRay(pixelX, pixelY);

            HitInfo closestHit = new(false, null, new(0, 0, 0));
            double closestDistance = double.PositiveInfinity;
            
            foreach (Triangle triangle in triangles)
            {
                (bool intersected, Point intersection) = triangle.CrossesTriangle(rayToFire);
                if (intersected)
                {
                    double distance = rayToFire.StartPoint.DistanceToOtherPoint(intersection);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestHit = new(true, triangle, intersection);
                    }
                }
            }

            return closestHit;
        }


        private double GetPixelByHitInfo(HitInfo info)
        {
            if (info.hit)
            {
                Ray RayL = new Ray(light - info.hitPoint, info.hitPoint);
                foreach (Triangle triangle in triangles)
                {
                    (bool intersected, Point intersection) = triangle.CrossesTriangle(RayL);
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
            return 0.75;
        }

        public void Render()
        {
            // fire ray from each pixel
            for (int i = 0; i < camera.screenPixelSize.height; i++)
            {
                Console.WriteLine($"{(double)i / camera.screenPixelSize.height * 100} %");
                for (int j = 0; j < camera.screenPixelSize.width; j++)
                {
                    HitInfo info = FireRay(j, i, triangles);
                    camera.SetPixel(GetPixelByHitInfo(info), j, i);
                }
            }
        }
    }
}
