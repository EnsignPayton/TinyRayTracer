using System;
using System.Numerics;

namespace Tiny.RayTracer.Core
{
    public class Sphere
    {
        public Sphere(Vector3 center, float radius, Material material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public Vector3 Center { get; }
        public float Radius { get; }
        public Material Material { get; }

        // See http://www.lighthouse3d.com/tutorials/maths/ray-sphere-intersection/
        public bool Intersects(Ray ray, out float distance)
        {
            var translatedCenter = Center - ray.Origin;

            // Distance from the ray origin to the sphere's projection point
            var distanceToProjection = Vector3.Dot(ray.Direction, translatedCenter);

            var projectedCenter = translatedCenter.ProjectOnto(ray.Direction);

            // Distance from the sphere's center to its projection point
            var projectedDistance = Center.DistanceTo(projectedCenter);
            if (projectedDistance > Radius)
            {
                // Projection point lies outside the sphere, therefore no intersection
                distance = 0.0f;
                return false;
            }

            // Distance from projection point to the possible collision points
            var projectedHitDistance = MathF.Sqrt(projectedDistance * projectedDistance + Radius * Radius);

            var firstPoint = distanceToProjection - projectedHitDistance;
            if (firstPoint > 0)
            {
                // Sphere is in front of ray
                distance = firstPoint;
                return true;
            }

            var secondPoint = distanceToProjection + projectedHitDistance;
            if (secondPoint > 0)
            {
                // Ray is inside sphere
                distance = secondPoint;
                return true;
            }

            // Sphere is behind ray
            distance = secondPoint;
            return false;
        }
    }
}
