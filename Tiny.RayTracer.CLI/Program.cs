using Tiny.RayTracer.Core;

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
            var pptWriter = new PPMWriter();
            pptWriter.Write(frameBuffer, filePath);
        }
    }
}
