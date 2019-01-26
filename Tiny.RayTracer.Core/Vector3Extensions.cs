using System;
using System.Numerics;

namespace Tiny.RayTracer.Core
{
    public static class Vector3Extensions
    {
        public static Vector3 ProjectOnto(this Vector3 a, Vector3 b)
        {
            var dot = Vector3.Dot(a, b);
            var mag = b.Magnitude();
            var scale = dot / mag;

            return b * scale;
        }

        public static float Magnitude(this Vector3 vector)
        {
            return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
        }
    }
}
