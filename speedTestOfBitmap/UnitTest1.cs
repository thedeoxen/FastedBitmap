using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using FastedBimap;

namespace speedTestOfBitmap
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void originBitmapReadTest()
        {
            int redChanelSumm = 0;

            Bitmap img = new Bitmap("../../image/1.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    redChanelSumm += img.GetPixel(x, y).R;
                }
            }
        }

        [TestMethod]
        public void fastBitmapReadTest()
        {
            int redChanelSumm = 0;

            FastBitmap img = new FastBitmap("../../image/1.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    redChanelSumm += img.GetPixel(x, y).R;
                }
            }
        }


        [TestMethod]
        public void originBitmapWriteTest()
        {
            Bitmap img = new Bitmap("../../image/1.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    img.SetPixel(x, y, Color.Red);
                }
            }
        }

        [TestMethod]
        public void fastBitmapWriteTest()
        {
            FastBitmap img = new FastBitmap("../../image/1.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    img.SetPixel(x, y, Color.Red);
                }
            }
            Assert.IsTrue(img.GetPixel(0, 0).R == 255);
            Assert.IsTrue(img.GetPixel(0, 0).G == 0);
            Assert.IsTrue(img.GetPixel(0, 0).B == 0);
        }

        [TestMethod]
        public void originBitmapReadTestBigImg()
        {
            int redChanelSumm = 0;

            Bitmap img = new Bitmap("../../image/2.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    redChanelSumm += img.GetPixel(x, y).R;
                }
            }
        }


        [TestMethod]
        public void fastBitmapReadTestBigImg()
        {
            int redChanelSumm = 0;

            FastBitmap img = new FastBitmap("../../image/2.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    redChanelSumm += img.GetPixel(x, y).R;
                }
            }
        }


        [TestMethod]
        public void originBitmapWriteTestBigImg()
        {
            Bitmap img = new Bitmap("../../image/2.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    img.SetPixel(x, y, Color.Red);
                }
            }
        }

        [TestMethod]
        public void fastBitmapWriteTestBigImg()
        {
            FastBitmap img = new FastBitmap("../../image/2.jpg");
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    img.SetPixel(x, y, Color.Red);
                }
            }
            Assert.IsTrue(img.GetPixel(0, 0).R == 255);
            Assert.IsTrue(img.GetPixel(0, 0).G == 0);
            Assert.IsTrue(img.GetPixel(0, 0).B == 0);
        }

        [TestMethod]
        public void testConvertFastBitmapToBitmap()
        {
            FastBitmap img = new FastBitmap("../../image/1.jpg");
            img.SetPixel(0, 0, Color.Red);
            Bitmap bmp = img.ToBitmap();
            Assert.IsInstanceOfType(bmp, typeof(Bitmap));
            Assert.IsTrue(bmp.GetPixel(0, 0).R == 255);
            Assert.IsTrue(bmp.GetPixel(0, 0).G == 0);
            Assert.IsTrue(bmp.GetPixel(0, 0).B == 0);
        }


        [TestMethod]
        public void testConvertFastBitmapToBitmap2()
        {
            FastBitmap img = new FastBitmap("../../image/1.jpg");
            img.SetPixel(0, 0, Color.Red);
            Bitmap bmp = (Bitmap)img;
            Assert.IsInstanceOfType(bmp, typeof(Bitmap));
            Assert.IsTrue(bmp.GetPixel(0, 0).R == 255);
            Assert.IsTrue(bmp.GetPixel(0, 0).G == 0);
            Assert.IsTrue(bmp.GetPixel(0, 0).B == 0);
        }


    }
}
