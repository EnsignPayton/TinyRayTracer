using System;
using System.Numerics;

namespace Tiny.RayTracer.Core.Rendering
{
    public class FlatRenderer : RayTracingRenderer
    {
        public Sphere Sphere { get; set; }
        public Vector3 ForegroundColor { get; set; }
        public Vector3 BackgroundColor { get; set; }

        protected override Vector3 Cast(Ray ray)
        {
            return Sphere.Intersects(ray, out _)
                ? ForegroundColor
                : BackgroundColor;
        }
    }
}
