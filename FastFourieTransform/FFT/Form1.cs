using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;


namespace FFT
{
    public partial class Form1 : Form
    {

        Fourie imgBPF;
        Bitmap VhodnoIzobrajenie;
        Bitmap bitmap;
        


//-------------------------------------------
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
                VhodnoIzobrajenie = (Bitmap)pictureBox1.Image.Clone();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }



        private void FFTransform_Click(object sender, EventArgs e)
        {
            int width, height;
            int rec_height;
            int rec_width;
            rec_width = rec_height = (int)(512 * ((float)25 / 100));
            

            Bitmap temp = (Bitmap)VhodnoIzobrajenie.Clone();
            width = height = (int)(256 * Convert.ToInt32(50) / 100);
            bitmap = new Bitmap(width, height, VhodnoIzobrajenie.PixelFormat);


            width = height = (int)(rec_width * (100 / (float)25));
            if (width > 256)
            {
                width = height = 256;
            }

            Rectangle area = new Rectangle(1, 1, width, height);
            bitmap = (Bitmap)VhodnoIzobrajenie.Clone(area, VhodnoIzobrajenie.PixelFormat);

            imgBPF = new Fourie(bitmap);

            imgBPF.ForwardFFT();
            imgBPF.FFTShift();
            imgBPF.FFTPlot(imgBPF.FFTShifted);
        }




        private void button2_Click(object sender, EventArgs e)
        {
             try
            {
                pictureBox3.Image = (Image)imgBPF.PhasePlot;
            }
            catch (System.NullReferenceException ex)
            {
                MessageBox.Show("Трябва да е извършено преобразованието на Фурие ! ", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }


        


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Image = (Image)imgBPF.FourierPlot;
            }
            catch (System.NullReferenceException ex)
            {
                MessageBox.Show("Трябва да е извършено преобразованието на Фурие ! ", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RFT_Click(object sender, EventArgs e)
        {
            imgBPF.InverseFFT();
            pictureBox4.Image = (Image)imgBPF.Object;
           
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            System.Drawing.Rectangle screenSize = new System.Drawing.Rectangle();

            screenSize.Width = SystemInformation.VirtualScreen.Width;
            screenSize.Height = SystemInformation.VirtualScreen.Height;

            int zoomFactor = 10;

            Image img = pictureBox2.Image;
            Bitmap bitMapImg = new Bitmap(img);


            if (bitMapImg.Width < screenSize.Width && bitMapImg.Height < screenSize.Height)
            {
                Size newSize = new Size((int)(bitMapImg.Width * zoomFactor), (int)(bitMapImg.Height * zoomFactor));
                Bitmap bmp = new Bitmap(bitMapImg, newSize);


                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Here you set your interpolation mode
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    // Scale the image, by drawing it on the larger bitmap
                    g.DrawImage(img, new Rectangle(Point.Empty, bmp.Size));
                }
                pictureBox2.Image = (Image)bmp;

            }
        }

   







    }
}
