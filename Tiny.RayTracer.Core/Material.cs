using System;
using System.Numerics;

namespace Tiny.RayTracer.Core
{
    public class Material
    {
        public Material(Vector3 diffuseColor, Vector3 albedo, float specularExponent)
        {
            DiffuseColor = diffuseColor;
            Albedo = albedo;
            SpecularExponent = specularExponent;
        }

        public Vector3 DiffuseColor { get; }
        public Vector3 Albedo { get; }
        public float SpecularExponent { get; }
    }
}
