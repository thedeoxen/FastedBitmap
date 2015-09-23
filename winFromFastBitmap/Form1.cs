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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fastBmp = new FastBitmap(openFileDialog1.FileName);
                pictureBox1.Image = fastBmp.ToBitmap();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fastBmp.EditAllPixels(toGray);
            pictureBox1.Image = fastBmp.ToBitmap();
        }

        static Color toGray(Color color)
        {
            int middle = (color.R + color.G + color.B) / 3;

            return Color.FromArgb(color.A, middle, middle, middle);
        }
    }
}
