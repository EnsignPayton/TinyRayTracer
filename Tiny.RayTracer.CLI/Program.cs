using System;
using System.Numerics;
using Tiny.RayTracer.Core;
using Tiny.RayTracer.Core.Rendering;

namespace Tiny.RayTracer.CLI
{
    internal class Program
    {
        private static void Main()
        {
            const int width = 768;
            const int height = 768;
            const string filePath = "./out.ppm";

            var frameBuffer = new FrameBuffer(width, height);

            var ivory = new Material(new Vector3(0.4f, 0.4f, 0.3f), new Vector4(0.6f, 0.3f, 0.1f, 0.0f), 50.0f, 1.0f);
            var glass = new Material(new Vector3( 0.6f, 0.7f, 0.8f), new Vector4(0.0f, 0.5f, 0.1f, 0.8f), 125.0f, 1.5f);
            var rubber = new Material(new Vector3(0.3f, 0.1f, 0.1f), new Vector4(0.9f, 0.1f, 0.0f, 0.0f), 10.0f, 1.0f);
            var mirror = new Material(Vector3.One, new Vector4(0.0f, 10.0f, 0.8f, 0.0f), 1425.0f, 1.0f);

            var spheres = new[]
            {
                new Sphere(new Vector3(-3.0f, 0.0f, -16.0f), 2.0f, ivory),
                new Sphere(new Vector3(-1.0f, -1.5f, -12.0f), 2.0f, glass),
                new Sphere(new Vector3(1.5f, -0.5f, -18.0f), 3.0f, rubber),
                new Sphere(new Vector3(7.0f, 5.0f, -18.0f), 4.0f, mirror),
            };

            var lights = new[]
            {
                new PointLight(new Vector3(-20.0f, 20.0f, 20.0f), 1.5f),
                new PointLight(new Vector3(30.0f, 50.0f, -25.0f), 1.8f),
                new PointLight(new Vector3(30.0f, 20.0f, 30.0f), 1.7f),
            };

            var renderer = new LightedRenderer
            {
                FieldOfView = MathF.PI / 2.0f,
                Spheres = spheres,
                Lights = lights,
                BackgroundColor = new Vector3(0.2f, 0.7f, 0.8f),
                MaxReflectionDepth = 4,
            };

            renderer.Render(frameBuffer);

            var pptWriter = new PPMWriter();
            pptWriter.Write(frameBuffer, filePath);
        }
    }
}
