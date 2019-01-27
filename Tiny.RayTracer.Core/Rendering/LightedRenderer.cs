using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Tiny.RayTracer.Core.Rendering
{
    public class LightedRenderer : RayTracingRenderer
    {
        public IList<Sphere> Spheres { get; set; }
        public IList<PointLight> Lights { get; set; }
        public Vector3 BackgroundColor { get; set; }
        public int MaxReflectionDepth { get; set; }

        protected override Vector3 Cast(Ray ray)
        {
            return Cast(ray, 0);
        }

        private Vector3 Cast(Ray ray, int depth)
        {
            if (depth > MaxReflectionDepth || !SceneIntersect(ray, out var hit, out var material))
            {
                return BackgroundColor;
            }

            var reflectiveColor = Reflect(ray, hit, depth);
            var refractiveColor = Refract(ray, hit, material, depth);

            var lights = Lights
                .Where(light => !IsInShadow(light, hit))
                .ToList();

            var diffuseIntensity = lights.Sum(light => DiffuseIntensity(light, hit));
            var specularIntensity = lights.Sum(light => SpecularIntensity(light, ray, hit, material));

            var diffuseComponent = material.DiffuseColor * diffuseIntensity * material.Albedo.X;
            var specularComponent = Vector3.One * specularIntensity * material.Albedo.Y;
            var reflectiveComponent = reflectiveColor * material.Albedo.Z;
            var refractiveComponent = refractiveColor * material.Albedo.W;

            return diffuseComponent + specularComponent + reflectiveComponent + refractiveComponent;
        }

        private bool SceneIntersect(Ray ray, out Ray hit, out Material material)
        {
            hit = null;
            material = null;

            float intersectionDistance = float.MaxValue;
            foreach (var sphere in Spheres)
            {
                if (sphere.TestIntersects(ray, out float distance) && distance < intersectionDistance)
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

        private Vector3 Reflect(Ray ray, Ray hit, int depth)
        {
            var direction = Vector3.Normalize(Vector3.Reflect(ray.Direction, hit.Direction));
            var origin = LiftOff(direction, hit);
            return Cast(new Ray(origin, direction), depth + 1);
        }

        private static Vector3 LiftOff(Vector3 direction, Ray hit)
        {
            return Vector3.Dot(direction, hit.Direction) < 0.0f
                ? hit.Origin - hit.Direction * 0.001f
                : hit.Origin + hit.Direction * 0.001f;
        }

        private Vector3 Refract(Ray ray, Ray hit, Material material, int depth)
        {
            var direction = Vector3.Normalize(ray.Direction.Refract(hit.Direction, material.RefractiveIndex));
            var origin = LiftOff(direction, hit);
            return Cast(new Ray(origin, direction), depth + 1);
        }

        private bool IsInShadow(PointLight light, Ray hit)
        {
            var direction = Vector3.Normalize(light.Position - hit.Origin);
            var distance = Vector3.Distance(light.Position, hit.Origin);
            var origin = LiftOff(direction, hit);

            return SceneIntersect(new Ray(origin, direction), out var hit2, out _) &&
                   Vector3.Distance(hit2.Origin, origin) < distance;
        }

        private static float DiffuseIntensity(PointLight light, Ray hit)
        {
            var direction = Vector3.Normalize(light.Position - hit.Origin);
            return light.Intensity * MathF.Max(0.0f, Vector3.Dot(direction, hit.Direction));
        }

        private static float SpecularIntensity(PointLight light, Ray ray, Ray hit, Material material)
        {
            var direction = Vector3.Normalize(light.Position - hit.Origin);
            var lightReflection = -Vector3.Reflect(-direction, hit.Direction);
            var reflectionFactor = Vector3.Dot(lightReflection, ray.Direction);
            var specularFactor = MathF.Pow(MathF.Max(0.0f, reflectionFactor), material.SpecularExponent);
            return specularFactor * light.Intensity;
        }
    }
}
