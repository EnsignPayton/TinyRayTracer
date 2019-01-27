using System;
using System.Numerics;

namespace Tiny.RayTracer.Core
{
    public class Material
    {
        public Material(Vector3 diffuseColor, Vector4 albedo, float specularExponent, float refractiveIndex)
        {
            DiffuseColor = diffuseColor;
            Albedo = albedo;
            SpecularExponent = specularExponent;
            RefractiveIndex = refractiveIndex;
        }

        public Vector3 DiffuseColor { get; }
        public Vector4 Albedo { get; }
        public float SpecularExponent { get; }
        public float RefractiveIndex { get; }
    }
}
