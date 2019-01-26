using System.Collections.Generic;
using System.Numerics;

namespace Tiny.RayTracer.Core.Rendering
{
    public class FlatRenderer : RayTracingRenderer
    {
        public IList<Sphere> Spheres { get; set; }
        public Vector3 BackgroundColor { get; set; }

        protected override Vector3 Cast(Ray ray)
        {
            return SceneIntersect(ray, out _, out var material)
                ? material.DiffuseColor
                : BackgroundColor;
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
    }
}
