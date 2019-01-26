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
            var renderer = new FlatRenderer
            {
                FieldOfView = MathF.PI / 2.0f,
                Sphere = new Sphere(new Vector3(-3.0f, 0.0f, -16.0f), 2.0f, new Material(new Vector3(0.2f, 0.7f, 0.8f), Vector3.Zero, 0.0f)),
                BackgroundColor = new Vector3(0.4f, 0.4f, 0.3f)
            };

            renderer.Render(frameBuffer);

            var pptWriter = new PPMWriter();
            pptWriter.Write(frameBuffer, filePath);
        }
    }
}
