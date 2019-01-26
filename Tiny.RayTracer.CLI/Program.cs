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
            const int width = 1024;
            const int height = 768;
            const string filePath = "./out.ppm";

            var frameBuffer = new FrameBuffer(width, height);

            var ivory = new Material(new Vector3(0.4f, 0.4f, 0.3f), Vector3.Zero, 0.0f);
            var rubber = new Material(new Vector3(0.3f, 0.1f, 0.1f), Vector3.Zero, 0.0f);

            var spheres = new[]
            {
                new Sphere(new Vector3(-3.0f, 0.0f, -16.0f), 2.0f, ivory),
                new Sphere(new Vector3(-1.0f, -1.5f, -12.0f), 2.0f, rubber),
                new Sphere(new Vector3(1.5f, -0.5f, -18.0f), 3.0f, rubber),
                new Sphere(new Vector3(7.0f, 5.0f, -18.0f), 4.0f, ivory),
            };

            var renderer = new FlatRenderer
            {
                FieldOfView = MathF.PI / 2.0f,
                Spheres = spheres,
                BackgroundColor = new Vector3(0.2f, 0.7f, 0.8f)
            };

            renderer.Render(frameBuffer);

            var pptWriter = new PPMWriter();
            pptWriter.Write(frameBuffer, filePath);
        }
    }
}
