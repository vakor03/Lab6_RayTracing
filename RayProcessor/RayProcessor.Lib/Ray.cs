﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayProcesssor.Lib
{
    public class Ray
    {
        public Point Vector { get; }

        // camera point
        public Point StartPoint { get; }

        public Ray(Point vector, Point point)
        {
            Vector = vector;
            StartPoint = point;
        }
    }
}