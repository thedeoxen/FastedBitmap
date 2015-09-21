using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.Drawing.Imaging;

namespace FastedBimap
{
    public class FastBitmap
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Bitmap imgBmp;

        private Color[,] pixelsArray;



        public FastBitmap(Bitmap img)
        {
            imgBmp = img;
            initializeParams(img);
        }

        public FastBitmap(string pathFile)
        {
            imgBmp = new Bitmap(pathFile);
            initializeParams(imgBmp);
        }

        private void initializeParams(Bitmap img)
        {
            Height = img.Height;
            Width = img.Width;
            pixelsArray = getPixelArrayFromBmp(img);

        }

        /// <summary>
        /// fix images bytes in map and create color array
        /// </summary>
        /// <param name="img"></param>
        /// <returns>2d color array</returns>
        private Color[,] getPixelArrayFromBmp(Bitmap img)
        {
            Color[,] colorArray = new Color[Height, Width];

            //Lock the bitmap's bits
            BitmapData imgData = img.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, img.PixelFormat);
            //Get the address in memory first bit
            IntPtr ptr = imgData.Scan0;
            int bytes = Math.Abs(imgData.Stride) * Height;
            byte[] rgbValues = new byte[bytes];
            //fill bytes array
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            /*
               because stride rounds to 4 bit
            */
            int correctStride = 0;
            int byteStringLenght = Width * 3;
            int byteCount = 0;
            if (imgData.Stride != Width * 3)
            {
                correctStride = imgData.Stride - Width * 3;
            }
            int x = 0, y = 0;
            for (int i = 0; i < bytes - 2; i += 3)
            {

                Color color = Color.FromArgb(255, rgbValues[i], rgbValues[i + 1], rgbValues[i + 2]);
                colorArray[y, x] = color;
                byteCount += 3;
                x++;
                if (byteCount == byteStringLenght)
                {
                    y++;
                    x = 0;
                    i += correctStride;
                    byteCount = 0;
                }
            }
            return colorArray;

        }

        public void SetPixel(int x, int y, Color color)
        {
            throw new NotImplementedException();
        }

        public Color GetPixel(int x, int y)
        {
            return pixelsArray[y, x];
        }
    }
}
