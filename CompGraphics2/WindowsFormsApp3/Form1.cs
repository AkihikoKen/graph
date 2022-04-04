using Filters_Andrich;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Stack<Bitmap> bitmaps;
        public int[,] mask = new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

        

        public Form1()
        {
            InitializeComponent();
            bitmaps = new Stack<Bitmap>();
            

        }

        private void openImageToolStrip_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*.bmp|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
                Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
                if (backgroundWorker1.CancellationPending != true)
                    image = newImage;
            }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void razmytyeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void gaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);

        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Median(1);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Opening(mask);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Closing(mask);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void gradToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Grad(mask);
            backgroundWorker1.RunWorkerAsync(filter);
        }


        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                _ = new GrayScaleFilter();
                int[][] histogram = new int[3][];
                histogram[0] = new int[256];
                histogram[1] = new int[256];
                histogram[2] = new int[256];
                for (int x = 0; x < image.Width; x++)
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color sourceColor = image.GetPixel(x, y);
                        histogram[0][sourceColor.R]++;
                        histogram[1][sourceColor.G]++;
                        histogram[2][sourceColor.B]++;
                    }
                Form2 histogramView = new Form2();
                histogramView.ShowRGBHistogram(histogram);
        }

        private void sharpnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private Color GetColorAt(Point point)
        {
            return ((Bitmap)pictureBox1.Image).GetPixel(point.X, point.Y);
        }

        private void waveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new WaveFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void pruitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PruitFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void strechingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new linearStretchingFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void glassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new glassFilter();
            backgroundWorker1.RunWorkerAsync(filter);


        }

        private void sharraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharraFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
    
   

}