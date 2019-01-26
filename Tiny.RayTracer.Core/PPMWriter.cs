using System.IO;
using System.Text;

namespace Tiny.RayTracer.Core
{
    public class PPMWriter
    {
        public void Write(FrameBuffer frameBuffer, string filePath)
        {
            var width = frameBuffer.Width;
            var height = frameBuffer.Height;
            var header = Encoding.ASCII.GetBytes($"P6\n{width} {height}\n255\n");
            var pixels = frameBuffer.GetBytes();

            using (var fs = File.OpenWrite(filePath))
            {
                fs.Write(header, 0, header.Length);
                fs.Write(pixels, 0, pixels.Length);
            }
        }
    }
}
