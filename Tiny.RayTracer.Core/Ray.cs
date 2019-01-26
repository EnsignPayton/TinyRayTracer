﻿using System;
using System.Numerics;

namespace Tiny.RayTracer.Core
{
    public class Ray
    {
        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 Origin { get; }
        public Vector3 Direction { get; }
    }
}
