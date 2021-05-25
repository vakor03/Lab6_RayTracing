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
        private Tree tree;

        public Renderer(Camera camera, Point light, Tree tree)
        {
            this.camera = camera;
            this.light = light;
            this.tree = tree;
        }


        private double GetPixelByHitInfo(HitInfo info)
        {
            if (info.hit)
            {
                Ray RayL = new Ray(light - info.hitPoint, info.hitPoint);
                HitInfo closesHitToLight = tree.GetClosestHit(RayL, tree.Root);
                if (closesHitToLight.hit && closesHitToLight.triangle != info.triangle &&
                    (light-closesHitToLight.hitPoint).magnitude<RayL.Vector.magnitude)
                {
                    return 0;
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
                if (i % 20 == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"{(double)i / camera.screenPixelSize.height * 100:F3} %");
                }
                for (int j = 0; j < camera.screenPixelSize.width; j++)
                {
                    Ray rayToFire = camera.GetPixelRay(j, i);
                    HitInfo closestHit =tree.GetClosestHit(rayToFire, tree.Root);
                    camera.SetPixel(GetPixelByHitInfo(closestHit), j, i);
                }
            }
        }
    }
}
