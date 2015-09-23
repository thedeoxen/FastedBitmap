using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastedBimap;

namespace winFromFastBitmap
{
    public partial class Form1 : Form
    {
        FastBitmap fastBmp;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                fastBmp = new FastBitmap(openFileDialog1.FileName);
                pictureBox1.Image = fastBmp.ToBitmap();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < fastBmp.Height; y++)
            {
                for (int x = 0; x < fastBmp.Width/2; x++)
                {
                    fastBmp.SetPixel(x, y, Color.Red);
                    
                }
            }
            pictureBox1.Image = fastBmp.ToBitmap();
        }
    }
}
