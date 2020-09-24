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
using System.IO;

namespace Podstawowy_foto_edytor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       
        void openImage()  //funkcja otwierająca obraz 
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = file;
                opened = true;
            }
        }

        void saveImage()  //funkcja zapisująca obraz
        {
            if (opened)
            {
                SaveFileDialog sfd = new SaveFileDialog(); 
                sfd.Filter = "Images|*.png;*.bmp;*.jpg";
                ImageFormat format = ImageFormat.Png;    // z góry ustalony format to png gdyż nie tracimy wtedy na jakości
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string ext = Path.GetExtension(sfd.FileName);
                    switch (ext)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }
                    pictureBox1.Image.Save(sfd.FileName, format);
                }
            }
            else { MessageBox.Show("No image loaded, first upload image "); }
        }

        void hue()
        {
            float changered = trackBar1.Value * 0.1f;
            float changegreen = trackBar2.Value * 0.1f;
            float changeblue = trackBar3.Value * 0.1f;

            label7.Text = (changered*10).ToString();
            label9.Text = (changeblue*10).ToString();
            label8.Text = (changegreen*10).ToString();

            reload();
            if (!opened)
            {
            }
            else
            {
                Image img = pictureBox1.Image;                            
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);  

                ImageAttributes ia = new ImageAttributes();                
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       
                {
                    new float[]{1+changered, 0, 0, 0, 0},
                    new float[]{0, 1+changegreen, 0, 0, 0},
                    new float[]{0, 0, 1+changeblue, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                }
              );
                ia.SetColorMatrix(cmPicture);           
                Graphics g = Graphics.FromImage(bmpInverted);  

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();                          
                pictureBox1.Image = bmpInverted;


            }
        }

        void brightness()
        {
            float changebrightness = trackBar6.Value / 255.0f;

            int val = (int)(changebrightness * 255);

            label10.Text = val.ToString();

            reload();
            if (!opened)
            {
            }
            else
            {
                Image img = pictureBox1.Image;
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);

                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cmPicture = new ColorMatrix(new float[][]
                {
                    new float[]{1, 0, 0, 0, 0},
                    new float[]{0, 1, 0, 0, 0},
                    new float[]{0, 0, 1, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{changebrightness, changebrightness, changebrightness, 1, 1 }
                }
              );
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();
                pictureBox1.Image = bmpInverted;


            }
        }

        void contrast()
        {
            float changecontrast = trackBar5.Value * 0.041f;

            int val = (int)(changecontrast * 24.4);

            label11.Text = val.ToString();

            reload();
            if (!opened)
            {
            }
            else
            {
                Image img = pictureBox1.Image;
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);

                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cmPicture = new ColorMatrix(new float[][]
                {
                    new float[]{ changecontrast, 0, 0, 0, 0},
                    new float[]{0, changecontrast, 0, 0, 0},
                    new float[]{0, 0, changecontrast, 0, 0},
                    new float[]{0, 0, 0, 1f, 0},
                    new float[]{0.001f, 0.001f, 0.001f, 1, 1f }
                }
              );
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();
                pictureBox1.Image = bmpInverted;


            }
        }

        void reload()   //funkcja ładująca ponownie oryginalny obraz
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                if (opened)
                {
                    file = Image.FromFile(openFileDialog1.FileName);
                    newBitmap = new Bitmap(openFileDialog1.FileName);
                    pictureBox1.Image = file;
                    opened = true;
                }
            }
        }

        void grayscale()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                trackBar1.Value = 0;
                trackBar2.Value = 0;
                trackBar3.Value = 0;
                Image img = pictureBox1.Image;                             
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   

                ImageAttributes ia = new ImageAttributes();                 
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       
                {
                    new float[]{0.299f, 0.299f, 0.299f, 0, 0},
                    new float[]{0.587f, 0.587f, 0.587f, 0, 0},
                    new float[]{0.114f, 0.114f, 0.114f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 0}
                });
                ia.SetColorMatrix(cmPicture);          
                Graphics g = Graphics.FromImage(bmpInverted);   

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                

                g.Dispose();                            
                pictureBox1.Image = bmpInverted;
            }
        }

        void blur()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int x = 1; x < newBitmap.Width; x++)
                {
                    for (int y = 1; y < newBitmap.Height; y++)
                    {
                        try
                        {
                            Color prevX = newBitmap.GetPixel(x - 1, y);
                            Color nextX = newBitmap.GetPixel(x + 1, y);
                            Color prevY = newBitmap.GetPixel(x, y - 1);
                            Color nextY = newBitmap.GetPixel(x, y + 1);

                            int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                            int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                            int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);

                            newBitmap.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox1.Image = newBitmap;
            }
        }

        void invert()
        {
            for (int x = 1; x < newBitmap.Width; x++)
            {
                for (int y = 1; y < newBitmap.Height; y++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, y);

                        int red = pixel.R;
                        int green = pixel.G;
                        int blue = pixel.B;

                        newBitmap.SetPixel(x, y, Color.FromArgb(255 - red, 255 - green, 255 - blue));
                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = newBitmap;
        }

        void edge()
        {
            Bitmap nB = new Bitmap(newBitmap.Width, newBitmap.Height);

            for (int x = 1; x <= newBitmap.Width - 1; x++)
            {
                for (int y = 1; y <= newBitmap.Height - 1; y++)
                {  
                    nB.SetPixel(x, y, Color.DarkGray); 
                }
            }
            for (int x = 1; x <= newBitmap.Width - 1; x++)
            {
                for (int y = 1; y <= newBitmap.Height - 1; y++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, y);

                        int colVal = (pixel.R + pixel.G + pixel.B);

                        if(lastCol == 0) lastCol = (pixel.R + pixel.G + pixel.B);

                        int diff;

                        if(colVal > lastCol)
                        {
                            diff = colVal - lastCol;
                        }
                        else
                        {
                            diff = lastCol - colVal;
                        }

                        if(diff > 100)
                        {
                            nB.SetPixel(x, y, Color.Gray);
                            lastCol = colVal;
                        }

                    }
                    catch (Exception) { }
                }

                for (int y = 1; y <= newBitmap.Height - 1; y++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, y);

                        int colVal = (pixel.R + pixel.G + pixel.B);

                        if (lastCol == 0) lastCol = (pixel.R + pixel.G + pixel.B);

                        int diff;

                        if (colVal > lastCol)
                        {
                            diff = colVal - lastCol;
                        }
                        else
                        {
                            diff = lastCol - colVal;
                        }

                        if (diff > 100)
                        {
                            nB.SetPixel(x, y, Color.Gray);
                            lastCol = colVal;
                        }

                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = nB;
        }

        void mirror_l()
        {
            for (int xl = 1, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
            {
                for (int y = 1; y < newBitmap.Height; y++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(xl, y);

                        newBitmap.SetPixel(xr, y, pixel);

                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = newBitmap;
        }

        void mirror_r()
        {
            for (int xl = 1, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
            {
                for (int y = 1; y < newBitmap.Height; y++)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(xr, y);

                        newBitmap.SetPixel(xl, y, pixel);

                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = newBitmap;
        }
        void mirror_t()
        {
            for (int x = 1; x < newBitmap.Width; x++)
            {
                for (int yt = 1, yb = newBitmap.Height; yt < newBitmap.Height; yt++, yb--)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, yt);

                        newBitmap.SetPixel(x, yb, pixel);
                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = newBitmap;
        }

        void mirror_b()
        {
            for (int x = 1; x < newBitmap.Width; x++)
            {
                for (int yt = 1, yb = newBitmap.Height; yt < newBitmap.Height; yt++, yb--)
                {
                    try
                    {
                        Color pixel = newBitmap.GetPixel(x, yb);

                        newBitmap.SetPixel(x, yt, pixel);

                    }
                    catch (Exception) { }
                }
            }
            pictureBox1.Image = newBitmap;
        }

        Bitmap newBitmap;
        Image file;
        int lastCol = 0;
        Boolean opened = false;  //zmienna do sprawdzenia czy jakiekolwiek zdjęcie zostało otwarte
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reload();
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar5.Value = 0;
            trackBar6.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reload();  
            grayscale();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            hue();
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            contrast();
        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            brightness();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            blur();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            invert();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            edge();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mirror_l();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mirror_r();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mirror_t();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mirror_b();
        }
    }
}
