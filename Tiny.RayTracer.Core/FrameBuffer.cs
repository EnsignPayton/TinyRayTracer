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

        public byte[] GetBytes()
        {
            return _buffer
                .Select(AdjustBrightness)
                .SelectMany(GetVectorBytes)
                .ToArray();
        }

        private static Vector3 AdjustBrightness(Vector3 vector)
        {
            var max1 = MathF.Max(vector.X, vector.Y);
            var max2 = MathF.Max(vector.Z, max1);
            return max2 > 1.0f ? vector / max2 : vector;
        }

        private static byte[] GetVectorBytes(Vector3 vector)
        {
            return new[]
            {
                GetFloatByte(vector.X),
                GetFloatByte(vector.Y),
                GetFloatByte(vector.Z),
            };
        }

        private static byte GetFloatByte(float value)
        {
            return (byte) (255 * Clamp(value, 0.0f, 1.0f));
        }

        private static float Clamp(float value, float lower, float upper)
        {
            return MathF.Min(upper, MathF.Max(lower, value));
        }

        public void Fill(int x, int y, Vector3 pixel)
        {
            if (x < 0 || x >= Width) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= Height) throw new ArgumentOutOfRangeException(nameof(y));

            _buffer[x + y * Width] = pixel;
        }
    }
}
