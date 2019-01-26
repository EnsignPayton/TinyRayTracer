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
            var renderer = new TestPatternRenderer();
            renderer.Render(frameBuffer);

            var pptWriter = new PPMWriter();
            pptWriter.Write(frameBuffer, filePath);
        }
    }
}
