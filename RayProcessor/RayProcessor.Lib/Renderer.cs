using System;

namespace RayProcessor.Lib
{
    public class Renderer
    {
        private Camera _camera;
        private Point _light;
        private Tree _tree;

        public Renderer(Camera camera, Point light, Tree tree)
        {
            _camera = camera;
            _light = light;
            _tree = tree;
        }


        private double GetPixelByHitInfo(HitInfo info)
        {
            if (info.Hit)
            {
                Ray RayL = new Ray(_light - info.HitPoint, info.HitPoint);
                HitInfo closesHitToLight = _tree.GetClosestHit(RayL, _tree.Root);
                if (closesHitToLight.Hit && closesHitToLight.Triangle != info.Triangle &&
                    (_light-closesHitToLight.HitPoint).magnitude<RayL.Vector.magnitude)
                {
                    return 0;
                }
                double cos = RayL.Vector.DotProduct(info.Triangle.Normal);
                cos /= RayL.Vector.magnitude * info.Triangle.Normal.magnitude;
                if (cos < 0)
                {
                    return 0;
                }
                return cos / (RayL.Vector.magnitude*RayL.Vector.magnitude);
            }
            return 0.75;
        }
        
        private Point GetPointByHitInfo(HitInfo info)
        {
            if (info.Hit)
            {
                Ray RayL = new Ray(_light - info.HitPoint, info.HitPoint);
                HitInfo closesHitToLight = _tree.GetClosestHit(RayL, _tree.Root);
                if (closesHitToLight.Hit && closesHitToLight.Triangle != info.Triangle &&
                    (_light-closesHitToLight.HitPoint).magnitude<RayL.Vector.magnitude)
                {
                    return new Point(0,0,0);
                }
                double cos = RayL.Vector.DotProduct(info.Triangle.Normal);
                cos /= RayL.Vector.magnitude * info.Triangle.Normal.magnitude;
                if (cos < 0)
                {
                    return new Point(0,0,0);
                }

                var normal = info.Triangle.Normal.Normalize();
                 double k = cos / (RayL.Vector.magnitude * RayL.Vector.magnitude);
                // double k = 0.9;
                // return new Point(normal.x*k,normal.y*k, normal.z*k);
                return new Point(Math.Abs(normal.x*k),Math.Abs(normal.y*k), Math.Abs(normal.z*k));
            }
            return new Point(0,0,0);
        }

        public void Render()
        {
            // fire ray from each pixel
            for (int i = 0; i < _camera.screenPixelSize.height; i++)
            {
                if (i % 20 == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"{(double)i / _camera.screenPixelSize.height * 100:F3} %");
                }
                for (int j = 0; j < _camera.screenPixelSize.width; j++)
                {
                    Ray rayToFire = _camera.GetPixelRay(j, i);
                    HitInfo closestHit =_tree.GetClosestHit(rayToFire, _tree.Root);
                    _camera.SetPixel(GetPointByHitInfo(closestHit), j, i);
                }
            }
        }
    }
}
