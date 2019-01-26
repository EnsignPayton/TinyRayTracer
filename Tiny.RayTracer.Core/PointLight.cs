using System;
using System.Numerics;

namespace Tiny.RayTracer.Core
{
    public class PointLight
    {
        public PointLight(Vector3 position, float intensity)
        {
            Position = position;
            Intensity = intensity;
        }

        public Vector3 Position { get; }
        public float Intensity { get; }
    }
}
