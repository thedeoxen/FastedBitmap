using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FastedBimap;
using System.Drawing;


namespace speedTestOfBitmap
{
    [TestClass]
    public class EditAllPixelTest
    {
        [TestMethod]
        public void EditAllPixel()
        {
            FastBitmap img = new FastBitmap("../../image/1.jpg");
            img.EditAllPixels((color) => (Color.FromArgb(color.A, color.R, color.G, 0)));

            Assert.IsTrue(img.ToBitmap().GetPixel(1, 1).B == 0);
            Assert.IsTrue(img.ToBitmap().GetPixel(img.Width-1, img.Height-1).B == 0);
        }


        [TestMethod()]
        public void EditAllPixelsEditAllPixel2()
        {
            FastBitmap img = new FastBitmap("../../image/1.jpg");
            img.EditAllPixels(threshold, (Object) 240);
            Assert.IsTrue(img.ToBitmap().GetPixel(0, 0) == Color.FromArgb(255,0,0,0));
            Assert.IsTrue(img.ToBitmap().GetPixel(img.Width - 1, img.Height - 1) == Color.FromArgb(255, 255, 255, 255));
        }

        private Color threshold(Color color, Object threshold)
        {
            int intThreshold= Convert.ToInt32(threshold);
            if (((color.R+color.G + color.B) /3) > intThreshold)
            {
                return Color.White;
            }
            else
            {
                return Color.Black;
            }
        }
    }
}
