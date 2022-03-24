using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    public partial class Form2 : Form
    {
        Image image;
        Bitmap bitmap;
        float contrast;
        float gamma = 1;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
            this.image = image;
            bitmap = new Bitmap(image);
            BrightnesstrackBar.Visible = false;
            ContrasttrackBar.Visible = false;
            GammatrackBar.Visible = false;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // we pull the bitmap from the image
            Bitmap bmp = (Bitmap)pictureBox1.Image;

            // we change some picels
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255, 255, c.G, c.B));
                }
            // we need to re-assign the changed bitmap
            pictureBox1.Image = (Bitmap)bmp;
        }

        Image ZoomPlus(Image image,Size size)
        {
            try
            {
                Bitmap bmp = new Bitmap(image, Convert.ToInt32(image.Width + size.Width), Convert.ToInt32(image.Height + size.Height));
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return image;
            }
        }

        Image ZoomMinus(Image image, Size size)
        {
            try
            {
                Bitmap bmp = new Bitmap(image, Convert.ToInt32(image.Width - size.Width), Convert.ToInt32(image.Height - size.Height));
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return image;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.BackgroundImage);
            pictureBox1.BackgroundImage = ZoomPlus(bmp, new Size(100, 100));
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.BackgroundImage);
            pictureBox1.BackgroundImage = ZoomPlus(bmp, new Size(75, 75));
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.BackgroundImage);
            pictureBox1.BackgroundImage = ZoomPlus(bmp, new Size(50, 50));
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.BackgroundImage);
            pictureBox1.BackgroundImage = ZoomMinus(bmp, new Size(100, 100));
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.BackgroundImage);
            pictureBox1.BackgroundImage = ZoomMinus(bmp, new Size(75, 75));
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.BackgroundImage);
            pictureBox1.BackgroundImage = ZoomMinus(bmp, new Size(50, 50));
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(pictureBox1.Image);
            for (int y = 0; (y <= (pic.Height - 1)); y++)
            {
                for (int x = 0; (x <= (pic.Width - 1)); x++)
                {
                    Color inv = pic.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    pic.SetPixel(x, y, inv);
                }
            }
            pictureBox1.Image = pic;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255, c.R, 255, c.B));
                }
            pictureBox1.Image = (Bitmap)bmp;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255, c.R, c.G, 255));
                }
            pictureBox1.Image = (Bitmap)bmp;
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrightnesstrackBar.Visible = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float value = BrightnesstrackBar.Value * 0.01f;

            float[][] colorMatrixElements = {
                new float[] {1,0,0,0,0},
                new float[] {0,1,0,0,0},
                new float[] {0,0,1,0,0},
                new float[] {0,0,0,1,0},
                new float[] {value,value,value,0,1}
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Image _img = image;
            Graphics _g = default(Graphics);
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(_img.Width), Convert.ToInt32(_img.Height));
            _g = Graphics.FromImage(bm_dest);
            _g.DrawImage(_img, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);
            pictureBox1.Image = bm_dest;
        }

        private void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x=0;x<bitmap.Width;x++)
                for (int y=0;y<bitmap.Height;y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int greyscale = (int)((color.R * .3) + (color.G * .59) + (color.B * .11));
                    Color newcolor = Color.FromArgb(greyscale, greyscale, greyscale);
                    bitmap.SetPixel(x, y, newcolor);
                }
            pictureBox1.Image = bitmap;
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContrasttrackBar.Visible = true;
        }

        private void gammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GammatrackBar.Visible = true;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            contrast = 0.04f * ContrasttrackBar.Value;

            Bitmap bit = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics g = Graphics.FromImage(bit);
            ImageAttributes imageAttributes = new ImageAttributes();

            float[][] colorMatrixElements = {
                new float[] {contrast,0f,0f,0f,0f},
                new float[] {0f,contrast,0f,0f,0f},
                new float[] {0f,0f,contrast,0f,0f},
                new float[] {0f,0f,0f,contrast,0f},
                new float[] {0.001f, 0.001f, 0.001f, 0f,1f}
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix);

            g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
            g.Dispose();
            imageAttributes.Dispose();
            pictureBox1.Image = bit;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            gamma = 0.04f * GammatrackBar.Value;
            Bitmap bit = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics g = Graphics.FromImage(bit);
            ImageAttributes imageAttributes = new ImageAttributes();

            imageAttributes.SetGamma(gamma);

            g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
            g.Dispose();
            imageAttributes.Dispose();
            pictureBox1.Image = bit;
        }
    }
}
