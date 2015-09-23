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

        }
    }
}
