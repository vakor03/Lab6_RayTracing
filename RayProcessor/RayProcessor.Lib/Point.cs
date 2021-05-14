﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcesssor.Lib
{
    struct Point
    {
        double x;
        double y;
        double z;

        double magnitude;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            magnitude = Math.Sqrt(x * x + y * y + z * z);
        }

        public Point GetDirectionalСosinuses()
        {

            return new Point(x / magnitude, y / magnitude, z / magnitude);
        }

        public Point GetDirectionalSinuses()
        {
            Point directionalCosinuses = GetDirectionalSinuses();
            return new Point(Math.Sqrt(1-Math.Pow(directionalCosinuses.x, 2)),
                Math.Sqrt(1 - Math.Pow(directionalCosinuses.y, 2)),
                Math.Sqrt(1 - Math.Pow(directionalCosinuses.z, 2)));
        }
    }
}
