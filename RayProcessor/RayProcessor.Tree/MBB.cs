using System;
using RayProcessor.Lib;

namespace RayProcessor.Tree
{
    public class MBB
    {
        public double Volume => (_xMax - _xMin) * (_yMax - _yMin) * (_zMax - _zMin);

        public double Margin => ((_xMax - _xMin)*(_yMax - _yMin) + (_xMax - _xMin)*(_zMax - _zMin) +(_zMax - _zMin)*(_yMax - _yMin) ) * 2;

        public double _xMin = double.MaxValue;
        public double _xMax = double.MinValue;
        public double _yMin = double.MaxValue;
        public double _yMax = double.MinValue;
        public double _zMin = double.MaxValue;
        public double _zMax = double.MinValue;


        public MBB(params Point[] points)
        {
            foreach (var t in points)
            {
                AddPoint(t);
            }
        }

        public MBB(Triangle triangle)
        {
            AddTriangle(triangle);
        }

        public MBB()
        {
        }

        public void AddTriangle(Triangle triangle)
        {
            for (int i = 0; i < 3; i++)
            {
                AddPoint(triangle.Points[i]);
            }
        }

        public void AddPoint(Point point)
        {
            if (point.x > _xMax)
            {
                _xMax = point.x;
            }

            if (point.x < _xMin)
            {
                _xMin = point.x;
            }

            if (point.y > _yMax)
            {
                _yMax = point.y;
            }

            if (point.y < _yMin)
            {
                _yMin = point.y;
            }

            if (point.z > _zMax)
            {
                _zMax = point.z;
            }

            if (point.z < _zMin)
            {
                _zMin = point.z;
            }
        }


        public static double CheckOverlapChange(MBB mbb1, MBB mbb2, Point trCenter)
        {
            MBB[] mbbs = {mbb1, mbb2};
            double[] overlapChanges = new double[2];
            for (int i = 0; i < 2; i++)
            {
                double volume1 = CheckOverlapVolume(mbbs[i], mbbs[(i + 1) % 2]);

                double xMax = trCenter.x > mbbs[i]._xMax ? trCenter.x : mbbs[i]._xMax;
                double yMax = trCenter.y > mbbs[i]._yMax ? trCenter.y : mbbs[i]._yMax;
                double zMax = trCenter.z > mbbs[i]._zMax ? trCenter.z : mbbs[i]._zMax;

                double xMin = trCenter.x < mbbs[i]._xMin ? trCenter.x : mbbs[i]._xMin;
                double yMin = trCenter.y < mbbs[i]._yMin ? trCenter.y : mbbs[i]._yMin;
                double zMin = trCenter.z < mbbs[i]._zMin ? trCenter.z : mbbs[i]._zMin;

                MBB newMbb = new MBB();
                newMbb.AddPoint(new Point(xMax, yMax, zMax));
                newMbb.AddPoint(new Point(xMin, yMin, zMin));


                double volume2 = CheckOverlapVolume(newMbb, mbbs[(i + 1) % 2]);

                overlapChanges[i] = volume2 - volume1;
            }

            return overlapChanges[1] - overlapChanges[0];
        }

        public static double CheckOverlapVolume(MBB mbb1, MBB mbb2)
        {
            double xMin = Math.Max(mbb1._xMin, mbb2._xMin);
            double xMax = Math.Min(mbb1._xMax, mbb2._xMax);
            double yMin = Math.Max(mbb1._yMin, mbb2._yMin);
            double yMax = Math.Min(mbb1._yMax, mbb2._yMax);
            double zMin = Math.Max(mbb1._zMin, mbb2._zMin);
            double zMax = Math.Min(mbb1._zMax, mbb2._zMax);

            if (mbb1._xMax <= mbb2._xMin || mbb2._xMax <= mbb1._xMin ||
                mbb1._yMax <= mbb2._yMin || mbb2._yMax <= mbb1._yMin ||
                mbb1._zMax <= mbb2._zMin || mbb2._zMax <= mbb1._zMin)
            {
                return 0;
            }

            MBB newMbb = new MBB(new(xMin, yMin, zMin), new(xMax, yMax, zMax));

            return newMbb.Volume;
        }

        public static double CheckVolumeChange(MBB mbb1, MBB mbb2, Point trCenter)
        {
            return CheckVolumeChange(mbb2, trCenter) - CheckVolumeChange(mbb1, trCenter);
        }

        private static double CheckVolumeChange(MBB mbb, Point trCenter)
        {
            double xMax = trCenter.x > mbb._xMax ? trCenter.x : mbb._xMax;
            double yMax = trCenter.y > mbb._yMax ? trCenter.y : mbb._yMax;
            double zMax = trCenter.z > mbb._zMax ? trCenter.z : mbb._zMax;

            double xMin = trCenter.x < mbb._xMin ? trCenter.x : mbb._xMin;
            double yMin = trCenter.y < mbb._yMin ? trCenter.y : mbb._yMin;
            double zMin = trCenter.z < mbb._zMin ? trCenter.z : mbb._zMin;

            double volumeChange = (xMax - xMin) * (yMax - yMin) * (zMax - zMin) - mbb.Volume;
            return volumeChange;
        }
    }
}