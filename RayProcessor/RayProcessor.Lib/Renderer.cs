﻿using System;
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
        private TreeQueries treeQueries;

        public Renderer(Camera camera, Point light, Tree tree)
        {
            this.camera = camera;
            this.light = light;
            this.treeQueries = new(tree);
        }


        private double GetPixelByHitInfo(HitInfo info)
        {
            if (info.hit)
            {
                Ray RayL = new Ray(light - info.hitPoint, info.hitPoint);
                HitInfo closesHitToLight = treeQueries.GetClosestHit(RayL);
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
                Console.WriteLine($"{(double)i / camera.screenPixelSize.height * 100} %");
                for (int j = 0; j < camera.screenPixelSize.width; j++)
                {
                    Ray rayToFire = camera.GetPixelRay(j, i);
                    HitInfo closestHit = treeQueries.GetClosestHit(rayToFire);
                    camera.SetPixel(GetPixelByHitInfo(closestHit), j, i);
                }
            }
        }
    }
}
