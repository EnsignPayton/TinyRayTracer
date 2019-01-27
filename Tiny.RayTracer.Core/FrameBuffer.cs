using System;
using System.Linq;
using System.Numerics;

namespace Tiny.RayTracer.Core
{
    public class FrameBuffer
    {
        private readonly Vector3[] _buffer;

        public FrameBuffer(int width, int height)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

            Width = width;
            Height = height;

            _buffer = new Vector3[width * height];
        }

        public int Width { get; }
        public int Height { get; }

        public Vector3[] GetBuffer()
        {
            return _buffer
                .Select(AdjustBrightness)
                .ToArray();
        }

        private static Vector3 AdjustBrightness(Vector3 vector)
        {
            var max1 = MathF.Max(vector.X, vector.Y);
            var max2 = MathF.Max(vector.Z, max1);
            var scaled = max2 > 1.0f ? vector / max2 : vector;
            return Vector3.Clamp(scaled, Vector3.Zero, Vector3.One);
        }

        public void Fill(int x, int y, Vector3 pixel)
        {
            if (x < 0 || x >= Width) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= Height) throw new ArgumentOutOfRangeException(nameof(y));

            _buffer[x + y * Width] = pixel;
        }
    }
}
