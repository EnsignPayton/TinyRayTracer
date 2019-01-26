using System.Numerics;

namespace Tiny.RayTracer.Core.Rendering
{
    public class TestPatternRenderer : Renderer
    {
        public override void Render(FrameBuffer frameBuffer)
        {
            for (int j = 0; j < frameBuffer.Height; j++)
            {
                for (int i = 0; i < frameBuffer.Width; i++)
                {
                    var red = j / (float) frameBuffer.Height;
                    var green = i / (float) frameBuffer.Width;
                    var color = new Vector3(red, green, 0.0f);
                    frameBuffer.Fill(i, j, color);
                }
            }
        }
    }
}
