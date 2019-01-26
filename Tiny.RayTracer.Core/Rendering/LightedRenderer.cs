using System;
using System.Collections.Generic;
using System.Numerics;

namespace Tiny.RayTracer.Core.Rendering
{
    public class LightedRenderer : RayTracingRenderer
    {
        public IList<Sphere> Spheres { get; set; }
        public IList<PointLight> Lights { get; set; }
        public Vector3 BackgroundColor { get; set; }

        protected override Vector3 Cast(Ray ray)
        {
            if (!SceneIntersect(ray, out var hit, out var material))
            {
                return BackgroundColor;
            }

            var diffuseIntensity = DiffuseIntensity(hit);

            return material.DiffuseColor * diffuseIntensity;
        }

        private bool SceneIntersect(Ray ray, out Ray hit, out Material material)
        {
            hit = null;
            material = null;

            float intersectionDistance = float.MaxValue;
            foreach (var sphere in Spheres)
            {
                if (sphere.Intersects(ray, out float distance) && distance < intersectionDistance)
                {
                    intersectionDistance = distance;
                    var position = ray.Origin + ray.Direction * distance;
                    var direction = Vector3.Normalize(position - sphere.Center);

                    hit = new Ray(position, direction);
                    material = sphere.Material;
                }
            }

            // TODO: Replace magic number
            return intersectionDistance < 1000.0f;
        }

        private float DiffuseIntensity(Ray hit)
        {
            float intensity = 0.0f;
            foreach (var light in Lights)
            {
                var direction = Vector3.Normalize(light.Position - hit.Origin);
                intensity += light.Intensity * MathF.Max(0.0f, Vector3.Dot(direction, hit.Direction));
            }

            return intensity;
        }
    }
}
