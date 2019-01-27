using System;
using System.Numerics;
using Tiny.RayTracer.Core;
using Xunit;

namespace Tiny.RayTracer.Test
{
    public class FrameBufferTests
    {
        #region Ctor

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void WidthSaves(int width)
        {
            const int height = 1;

            var target = new FrameBuffer(width, height);
            var actual = target.Width;

            Assert.Equal(width, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void HeightSaves(int height)
        {
            const int width = 1;

            var target = new FrameBuffer(width, height);
            var actual = target.Height;

            Assert.Equal(height, actual);
        }

        [Fact]
        public void NegativeWidthThrows()
        {
            const int width = -1;
            const int height = 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => new FrameBuffer(width, height));
        }

        [Fact]
        public void NegativeHeightThrows()
        {
            const int width = 1;
            const int height = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => new FrameBuffer(width, height));
        }

        [Fact]
        public void ZeroWidthThrows()
        {
            const int width = 0;
            const int height = 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => new FrameBuffer(width, height));
        }

        [Fact]
        public void ZeroHeightThrows()
        {
            const int width = 1;
            const int height = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => new FrameBuffer(width, height));
        }

        #endregion

        #region Fill

        [Fact]
        public void NegativeXThrows()
        {
            const int x = -1;
            const int y = 0;

            var target = new FrameBuffer(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => target.Fill(x, y, Vector3.Zero));
        }

        [Fact]
        public void NegativeYThrows()
        {
            const int x = 0;
            const int y = -1;

            var target = new FrameBuffer(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => target.Fill(x, y, Vector3.Zero));
        }

        [Fact]
        public void OutOfBoundsXThrows()
        {
            const int x = 1;
            const int y = 0;

            var target = new FrameBuffer(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => target.Fill(x, y, Vector3.Zero));
        }

        [Fact]
        public void OutOfBoundsYThrows()
        {
            const int x = 0;
            const int y = 1;

            var target = new FrameBuffer(1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => target.Fill(x, y, Vector3.Zero));
        }

        [Fact]
        public void SavesPixelInfo()
        {
            var pixel = new Vector3(0.1f, 0.2f, 0.3f);

            var target = new FrameBuffer(1, 1);
            target.Fill(0, 0, pixel);
            var actual = target.GetBuffer();

            Assert.Equal(pixel, actual[0]);
        }

        #endregion

        [Fact]
        public void UsesRowMajorOrder()
        {
            var pixels = new[]
            {
                new Vector3(0.1f),
                new Vector3(0.2f),
                new Vector3(0.3f),
                new Vector3(0.4f),
            };

            var target = new FrameBuffer(2, 2);
            target.Fill(0, 0, pixels[0]);
            target.Fill(1, 0, pixels[1]);
            target.Fill(0, 1, pixels[2]);
            target.Fill(1, 1, pixels[3]);
            var actual = target.GetBuffer();

            Assert.Equal(pixels, actual);
        }

        [Fact]
        public void ProportionallyAdjustsMaxBrightness()
        {
            var pixel = new Vector3(1.0f, 2.0f, 4.0f);

            var target = new FrameBuffer(1, 1);
            target.Fill(0, 0, pixel);
            var actual = target.GetBuffer();

            Assert.Equal(pixel, actual[0]);
        }
    }
}
