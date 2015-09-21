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
                    img.SetPixel(x,y,Color.Red);
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
        }
    }
}
