using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Tiny.RayTracer.Core.Rendering
{
    public abstract class RayTracingRenderer : Renderer
    {
        public float FieldOfView { get; set; }
        public Ray Camera { get; set; } = new Ray(Vector3.Zero, Vector3.Zero);

        public override void Render(FrameBuffer frameBuffer)
        {
            Parallel.For(0, frameBuffer.Height, j =>
            {
                for (int i = 0; i < frameBuffer.Width; i++)
                {
                    RenderPixel(frameBuffer, i, j);
                }
            });
        }

        private void RenderPixel(FrameBuffer frameBuffer, int i, int j)
        {
            // Calculate ray direction from pixel position
            var halfScreenWidth = MathF.Tan(FieldOfView / 2.0f);
            var x = (2.0f * (i + 0.5f) / frameBuffer.Width - 1.0f) *
                    halfScreenWidth *
                    frameBuffer.Width / frameBuffer.Height;

            var y = -(2.0f * (j + 0.5f) / frameBuffer.Height - 1.0f) *
                    halfScreenWidth;

            // Image is projected onto the z = -1 plane
            var direction = Vector3.Normalize(new Vector3(x, y, -1.0f));

            // TODO: Factor in rotation
            var ray = new Ray(Camera.Origin, direction);

            var color = Cast(ray);

            frameBuffer.Fill(i, j, color);
        }

        protected abstract Vector3 Cast(Ray ray);
    }
}
