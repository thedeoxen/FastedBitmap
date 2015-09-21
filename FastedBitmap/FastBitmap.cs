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
        private ColorInMemory[,] pixelsArray;


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

        private ColorInMemory[,] getPixelArrayFromBmp(Bitmap img)
        {
            ColorInMemory[,] colorArray = new ColorInMemory[Height, Width];

            //Lock the bitmap's bits
            BitmapData imgData = img.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, img.PixelFormat);
            //Get the address in memory first bit
            IntPtr ptr = imgData.Scan0;
            int bytesLenght = Math.Abs(imgData.Stride) * Height;
            byte[] rgbValues = new byte[bytesLenght];
            //fill bytes array
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytesLenght);
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
            for (int i = 0; i < bytesLenght - 2; i += 3)
            {

                Color color = Color.FromArgb(255, rgbValues[i + 2], rgbValues[i + 1], rgbValues[i]);
                colorArray[y, x] = new ColorInMemory(color, byteCount);
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
            img.UnlockBits(imgData);
            return colorArray;
        }

        public void SetPixel(int x, int y, Color color)
        {
            //Lock the bitmap's bits
            BitmapData imgData = imgBmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, imgBmp.PixelFormat);
            //Get the address in memory first bit
            IntPtr ptr = imgData.Scan0;
            int bytesLenght = Math.Abs(imgData.Stride) * Height;
            byte[] rgbValues = new byte[3];
            
            int byteCount = pixelsArray[y, x].positionInMemory;
            rgbValues[0] = color.B;
            rgbValues[1] = color.G;
            rgbValues[2] = color.R;
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr+byteCount, 3);

            imgBmp.UnlockBits(imgData);
            pixelsArray[y, x].color = color;

        }

        public Color GetPixel(int x, int y)
        {
            return pixelsArray[y, x].color;
        }

        public Bitmap ToBitmap()
        {
            return imgBmp;
        }

        /// <summary>
        /// class for save color and position in byte
        /// </summary>
        private class ColorInMemory
        {
            public Color color;
            public int positionInMemory;
            public ColorInMemory(Color color, int position)
            {
                this.color = color;
                this.positionInMemory = position;
            }
        }
    }
}
