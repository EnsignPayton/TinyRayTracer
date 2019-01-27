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

        // TODO: Name these nicer
        public static Vector3 Refract(this Vector3 vector, Vector3 normal, float index)
        {
            float cosi = -MathF.Max(-1.0f, MathF.Min(1.0f, Vector3.Dot(vector, normal)));
            Vector3 n;
            float eta;
            if (cosi < 0.0f)
            {
                cosi = -cosi;
                n = -normal;
                eta = index;
            }
            else
            {
                n = normal;
                eta = 1.0f / index;
            }

            float k = 1.0f - eta * eta * (1.0f - cosi * cosi);
            return k < 0.0f
                ? Vector3.Zero
                : vector * eta + n * (eta * cosi - MathF.Sqrt(k));
        }
    }
}
