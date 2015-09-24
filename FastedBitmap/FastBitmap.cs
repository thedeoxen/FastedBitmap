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
        int bytePerPixel;
        int correctStride;


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

        private Color[,] getPixelArrayFromBmp(Bitmap img)
        {
            PixelFormat pixelFormat = img.PixelFormat;



            Color[,] colorArray = new Color[Height, Width];

            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    {
                        bytePerPixel = 3;
                        break;
                    }
                case PixelFormat.Format32bppArgb:
                    {
                        bytePerPixel = 4;
                        break;
                    }
                default:
                    {
                        throw new ApplicationException("Sorry, this pixelFormat don't support");
                    }
            }

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
            correctStride = 0;
            int byteStringLenght = Width * bytePerPixel;
            int byteCount = 0;
            if (imgData.Stride != Width * bytePerPixel)
            {
                correctStride = imgData.Stride - Width * bytePerPixel;
            }
            int x = 0, y = 0;
            for (int i = 0; i <= bytesLenght - bytePerPixel; i += bytePerPixel)
            {
                int alphaChanel;
                if (bytePerPixel == 4)
                {
                    alphaChanel = rgbValues[i + 3];
                }
                else
                {
                    alphaChanel = 255;
                }


                Color color = Color.FromArgb(alphaChanel, rgbValues[i + 2], rgbValues[i + 1], rgbValues[i]);
                colorArray[y, x] = color;
                byteCount += bytePerPixel;
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
            BitmapData imgData = imgBmp.LockBits(new Rectangle(x, y, 1, 1), ImageLockMode.ReadOnly, imgBmp.PixelFormat);
            //Get the address in memory first bit
            IntPtr ptr = imgData.Scan0;

            byte[] rgbValues = new byte[bytePerPixel];

            rgbValues[0] = color.B;
            rgbValues[1] = color.G;
            rgbValues[2] = color.R;

            if (bytePerPixel == 4)
            {
                rgbValues[3] = color.A;
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytePerPixel);

            imgBmp.UnlockBits(imgData);
            pixelsArray[y, x] = color;

        }


        public Color GetPixel(int x, int y)
        {
            return pixelsArray[y, x];
        }

        public void EditAllPixels(Func<Color, Color> p)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    fakeSetPixel(x, y, p(pixelsArray[y, x]));
                }
            }
            fixInBitmap();
        }


        /// <summary>
        /// this method set value of pixel in pixel array, but this not write pixel in bitmap
        /// for write in bitmap use fixInBitmap
        /// </summary>
        private void fakeSetPixel(int x, int y, Color color)
        {
            pixelsArray[y, x] = color;
        }

        /// <summary>
        /// write pixel from pixels array to bitmap
        /// </summary>
        private void fixInBitmap()
        {
            //Lock the bitmap's bits
            BitmapData imgData = imgBmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, imgBmp.PixelFormat);
            //Get the address in memory first bit
            IntPtr ptr = imgData.Scan0;

            byte[] rgbValues = new byte[bytePerPixel];
            int byteCount = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color color = pixelsArray[y, x];
                    rgbValues[0] = color.B;
                    rgbValues[1] = color.G;
                    rgbValues[2] = color.R;

                    if (bytePerPixel == 4)
                    {
                        rgbValues[3] = color.A;
                    }
                    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr+byteCount, bytePerPixel);
                    byteCount += bytePerPixel;
                }
                byteCount += correctStride;
            }
            imgBmp.UnlockBits(imgData);            
        }

        public Bitmap ToBitmap()
        {
            return imgBmp;
        }

        public static explicit operator Bitmap(FastBitmap fastBitmap)
        {
            return fastBitmap.imgBmp;
        }


    }
}
