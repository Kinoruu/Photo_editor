using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Printing;

namespace Podstawowy_foto_edytor
{
    public partial class Form1 : Form
    {
        private Histogram histogramWin = new Histogram();
        private Extraction ex;
        public Form1()
        {
            InitializeComponent();

            //this.ex = extr;

            //MessageBox.Show("Please read 'About' and 'Help' to now how this program works" + "\n" + "or what can still not work well." + "\n" + "\n" + "Enjoy!!!");

            //histogramWin.DockStateChanged += new EventHandler(histogram_DockStateChanged);

            //histogramWin.VisibleChanged += new EventHandler(histogram_VisibleChanged);

            Pixelate_Bar.Enabled = false;
            drawingToolStripMenuItem.Enabled = false;
            statisticsToolStripMenuItem.Enabled = false;
            biCubicToolStripMenuItem.Enabled = false;
            yCbCrToolStripMenuItem.Enabled = false;
            //extractionToolStripMenuItem.Enabled = false;
            //stichDifferentImagesToolStripMenuItem.Enabled = false;
        }

        void openImage()  //funkcja otwierająca obraz 
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                //original = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                newBitmapTemp = new Bitmap(openFileDialog1.FileName);
                newBitmapTemp2 = new Bitmap(openFileDialog1.FileName);
                newBitmapSize2 = new Bitmap(openFileDialog1.FileName);
                pictureBox.Image = file;
                pictureBox1.Image = file;
                opened = true;
                Contrast_Bar.Value = 25;
                Brightness_Bar.Value = 1;
                Gamma_Bar.Value = 25;
                Gamma_Value_label.Text = Gamma_Bar.Value.ToString();
                Contrast_Value_label.Text = Contrast_Bar.Value.ToString();
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
                    pictureBox.Image.Save(sfd.FileName, format);
                }
            }
            else { MessageBox.Show("No image loaded, first upload image "); }
        }

        /*private void ShowHistogram(bool show)  //funkcja odpowiadająca za wyświetlanie histogamu w nowym oknie lub przypiętym oknie
        {
            config.histogramVisible = show;

            histogramViewItem.Checked = show;
            histogramButton.Pushed = show;

            if (show)
            {
                histogramWin.Show(dockManager);
            }
            else
            {
                histogramWin.Hide();
            }
        }*/

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)  //funkcja obsługi myszki oraz wyświetlania rozmiaru obrazu oraz informacji na temat pixeli
        {
            /*   //nadal coś nie działa później ogarnę
            if (!opened)
            {
               // MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int w = newBitmap.Width;
                int h = newBitmap.Height;

                int wi = pictureBox.Width;
                int he = pictureBox.Height;

                if (((e.Location.X >= 0) && (e.Location.X <= wi )) && ((e.Location.Y >= 0) && (e.Location.Y <= he)))
                {
                    if((e.Location.X >= ((wi - w) / 2)) && (e.Location.X <= (wi - ((wi - w) / 2))) && ((e.Location.Y >= ((he - h) / 2))) && (e.Location.Y <= (he - ((he - h) / 2))))
                    {
                        Color pixel = newBitmap.GetPixel(e.X, e.Y);
                        int R = (int)pixel.R;
                        int G = (int)pixel.G;
                        int B = (int)pixel.B;

                        int xx = e.X + ((wi - w) / 2);
                        int yy = e.Y + ((he - h) / 2);

                        resolution.Text = " | " + w + " x " + h + " | ";
                        position.Text = " | x " + e.X.ToString() + " , y " + e.Y.ToString() + " | ";
                        rgbpoints.Text = " | R " + R + " , G " + G + " , B " + G + " | ";
                    }
                    else
                    {
                        position.Text = " ";
                        rgbpoints.Text = "  ";
                    }
                }
                else
                {
                    position.Text = " ";
                    rgbpoints.Text = "  ";
                }
            }*/
        }

        int squareroot(int x)
        {
            if (x < 1) return 1 / squareroot(x);  // MSalter's general solution

            double xhi = Convert.ToDouble(x);
            double xlo = 0;
            double guess = x / 2;
            int ans;

            while (guess * guess != x)
            {
                if (guess * guess > x)
                    xhi = guess;
                else
                    xlo = guess;

                double new_guess = (xhi + xlo) / 2;
                if (new_guess == guess)
                    break; 
                guess = new_guess;
            }
            ans = Convert.ToInt32(guess);
            return ans;
        }

        void hue()    //funkcja zmieniająca wartości RGB obrazu
        {
            float changered = Red_Bar.Value * 0.1f;
            float changegreen = Green_Bar.Value * 0.1f;
            float changeblue = Blue_Bar.Value * 0.1f;

            Red_Value_label.Text = (changered * 10).ToString();
            Blue_Value_label.Text = (changeblue * 10).ToString();
            Green_Value_label.Text = (changegreen * 10).ToString();

            reload();
            if (!opened)
            {
            }
            else
            {
                Image img = pictureBox.Image;
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);

                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cmPicture = new ColorMatrix(new float[][]
                {
                    new float[]{1 + changered, 0, 0, 0, 0},
                    new float[]{0, 1 + changegreen, 0, 0, 0},
                    new float[]{0, 0, 1 + changeblue, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                }
              );
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();
                pictureBox.Image = bmpInverted;
            }
        }

        void brightness()   //funkcja zmieniająca jasność obrazu
        {
            float changebrightness = Brightness_Bar.Value / 255.0f;

            int val = (int)(changebrightness * 255);

            Brightness_Value_label.Text = val.ToString();

            reload();
            if (!opened)
            {
            }
            else
            {
                Image img = pictureBox.Image;
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
                ia.Dispose();
                pictureBox.Image = bmpInverted;
            }
        }

        void contrast()  //funkcja zmieniająca kontrast obrazu
        {
            float changecontrast = Contrast_Bar.Value * 0.041f;

            int val = (int)(changecontrast * 24.4);

            Contrast_Value_label.Text = val.ToString();

            reload();
            if (!opened)
            {
            }
            else
            {
                Image img = pictureBox.Image;
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
                ia.Dispose();
                pictureBox.Image = bmpInverted;
            }
        }

        void gamma()  //funkcja zmieniająca współczynnik gamma obrazu
        {
            reload();
            if (!opened)
            {
            }
            else
            {
                Gamma_Value_label.Text = Gamma_Bar.Value.ToString();

                float changegamma = Gamma_Bar.Value * 0.04f;

                //Gamma_Value_label.Text = Contrast_Bar.Value.ToString();

                Image img = pictureBox.Image;
                Bitmap bmpInverted = new Bitmap(newBitmap.Width, newBitmap.Height);

                Graphics g = Graphics.FromImage(bmpInverted);
                ImageAttributes ia = new ImageAttributes();

                ia.SetGamma(changegamma);

                g.DrawImage(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), 0, 0, newBitmap.Width, newBitmap.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();
                ia.Dispose();
                pictureBox.Image = bmpInverted;
            }
        }

        void threshold()
        {
            if (!opened)
            {
            }
            else
            {
                Threshold_Value_label.Text = Threshold_Bar.Value.ToString();

                float threshold = Threshold_Bar.Value * 25.5f;

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((newBitmap.GetPixel(x, y).GetBrightness() * 255) > threshold)
                            {
                                newBitmapTemp.SetPixel(x, y, Color.White);
                            }
                            else
                            {
                                newBitmapTemp.SetPixel(x, y, Color.Black);
                            }
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmapTemp;
            }
        }

        void color_shallowing()
        {
            if (!opened)
            {
            }
            else
            {
                Color_Shallowing_Value_label.Text = Color_Shallowing_Bar.Value.ToString();

                double xx = Math.Pow(2, Color_Shallowing_Bar.Value);
                int shallow = Convert.ToInt32(xx);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, y);
                            int R = (((int)pixel.R) / shallow) * shallow;
                            int G = (((int)pixel.G) / shallow) * shallow;
                            int B = (((int)pixel.B) / shallow) * shallow;
                            newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmapTemp;
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
                    pictureBox.Image = file;
                    opened = true;
                }
            }
        }

        void grayscale()   //funkcja zmieniająca kolory w odcienie szarości
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                Red_Bar.Value = 0;
                Green_Bar.Value = 0;
                Blue_Bar.Value = 0;
                Image img = pictureBox.Image;
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
                pictureBox.Image = bmpInverted;
            }
        }

        void sepia()   //funkcja zmieniająca oryginalny obraz w jego odpowiednik w kolorach sepii
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                Red_Bar.Value = 0;
                Green_Bar.Value = 0;
                Blue_Bar.Value = 0;
                Image img = pictureBox.Image;
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);

                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cmPicture = new ColorMatrix(new float[][]
                {
                    new float[]{0.393f, 0.349f, 0.272f, 0, 0},
                    new float[]{0.769f, 0.686f, 0.534f, 0, 0},
                    new float[]{0.189f, 0.168f, 0.131f, 0, 0},
                    new float[]{0, 0, 0, 1, 0},
                    new float[]{0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

                g.Dispose();
                pictureBox.Image = bmpInverted;
            }
        }

        void blur()   // funkcja rozmywająca obraz poprzez uśrednianie wartości pikseli
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                /*var options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 32000
                };*/

                Blur_Value_label.Text = Blur_Bar.Value.ToString();
                newBitmapTemp = newBitmap;
                int blur = Convert.ToInt32(Blur_Bar.Value * 10);

                for (int i = 0; i < blur; i++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    //Parallel.For(0, newBitmap.Width, x =>
                    {
                        Parallel.For(0, newBitmap.Height, y =>
                        //for (int y = 0; y < newBitmap.Height; y++)
                        {
                            try
                            {
                                Color prevX = newBitmapTemp.GetPixel(x - 1, y);
                                Color nextX = newBitmapTemp.GetPixel(x + 1, y);
                                Color prevY = newBitmapTemp.GetPixel(x, y - 1);
                                Color nextY = newBitmapTemp.GetPixel(x, y + 1);

                                int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                                int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                                int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);
                                
                                newBitmapTemp2.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                            }
                            catch (Exception) { }
                        });
                    }//);
                    newBitmapTemp = newBitmapTemp2;
                }
                pictureBox.Image = newBitmapTemp2;
            }
        }

        void pixelate()   // funkcja rozmywająca obraz poprzez uśrednianie wartości pikseli
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                /*Pixelate_Value_label.Text = Pixelate_Bar.Value.ToString();
                newBitmapTemp = newBitmap;
                int pixelate = Convert.ToInt32(Blur_Bar.Value * 10);

                int counter = 0;
                int avgR = 0;
                int avgG = 0;
                int avgB = 0;

                for (int x = 0; x < newBitmap.Width; x++)
                {
                    for (int y = 0; y < newBitmap.Height; y++)
                    {
                        Color pixel = newBitmap.GetPixel(x, y);
                        if ((counter == (10 * pixelate)) || (x == newBitmap.Width))
                        {
                            newBitmapTemp2.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));

                            avgR += (int)(pixel.R);
                            avgG += (int)(pixel.G);
                            avgB += (int)(pixel.B);
                        }
                        try
                        {
                            

                            int avgR += (int)(pixel.R);
                            int avgG += (int)(pixel.G);
                            int avgB += (int)(pixel.B);

                            newBitmapTemp2.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmapTemp2;*/

                /*int pixelationAmount = 50; //you can change it!!
                int width = OriginalBitmap.getWidth();
                int height = OriginalBitmap.getHeight();
                int avR, avB, avG; // store average of rgb 
                int pixel;

                for (int x = 0; x < width; x += pixelationAmount)
                { // do the whole image
                    for (int y = 0; y < height; y += pixelationAmount)
                    {
                        avR = 0; avG = 0; avB = 0;


                        int bx = x + pixelationAmount;
                        int by = y + pixelationAmount;
                        if (by >= height) by = height;
                        if (bx >= width) bx = width;
                        for (int xx = x; xx < bx; xx++)
                        {// YOU WILL WANT TO PUYT SOME OUT OF                                      BOUNDS CHECKING HERE
                            for (int yy = y; yy < by; yy++)
                            { // this is scanning the colors

                                pixel = OriginalBitmap.getPixel(xx, yy);
                                avR += (int)(Color.red(pixel));
                                avG += (int)(Color.green(pixel));
                                avB += (int)(Color.blue(pixel));
                            }
                        }
                        avR /= pixelationAmount ^ 2; //divide all by the amount of samples taken to get an average
                        avG /= pixelationAmount ^ 2;
                        avB /= pixelationAmount ^ 2;

                        for (int xx = x; xx < bx; xx++)// YOU WILL WANT TO PUYT SOME OUT OF BOUNDS CHECKING HERE
                            for (int yy = y; yy < by; yy++)
                            { // this is going back over the block 
                                bmOut.setPixel(xx, yy, Color.argb(255, avR, avG, avB)); //sets the block to the average color
                            }


                    }

                }
                iv.setImageBitmap(bmOut);*/

            }
        }

        void invert()   // funkcja odwracająca kolory(negatyw)
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    for (int y = 0; y < newBitmap.Height; y++)
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
                pictureBox.Image = newBitmap;
            }
        }

        void edge()   //funkcja znajdująca krawędzie na podstawie różnicy wartości piikseli( imitacja emboss)
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                Bitmap nB = new Bitmap(newBitmap.Width, newBitmap.Height);

                for (int x = 0; x <= newBitmap.Width - 1; x++)
                {
                    for (int y = 0; y <= newBitmap.Height - 1; y++)
                    {
                        nB.SetPixel(x, y, Color.White);
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
                                nB.SetPixel(x, y, Color.Black);
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
                                nB.SetPixel(x, y, Color.Black);
                                lastCol = colVal;
                            }
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = nB;
            }
        }

        void edge_WoB()   //funkcja znajdująca krawędzie na podstawie różnicy wartości piikseli( imitacja emboss)
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                Bitmap nB = new Bitmap(newBitmap.Width, newBitmap.Height);

                for (int x = 0; x <= newBitmap.Width - 1; x++)
                {
                    for (int y = 0; y <= newBitmap.Height - 1; y++)
                    {
                        nB.SetPixel(x, y, Color.Black);
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
                                nB.SetPixel(x, y, Color.White);
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
                                nB.SetPixel(x, y, Color.White);
                                lastCol = colVal;
                            }
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = nB;
            }
        }

        void add_RGB_ver()  //funkcja skejająca 4 takie same obrazy w formacie rgb
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapSize2 = new Bitmap(2 * width, 2 * height);

                for (int x = 0; x < newBitmapSize2.Width; x++)
                {
                    for (int y = 0; y < newBitmapSize2.Height; y++)
                    {
                        try
                        {
                            if (((x > 0) && (x <= width)) && ((y > 0) && (y < height)))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(R, G, B));
                            }
                            else if (((x > width) && (x <= (2 * width))) && ((y > 0) && (y <= height)))
                            {
                                Color pixel = newBitmap.GetPixel((x - width), y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(R, 0, 0));
                            }
                            else if (((x > 0) && (x <= width)) && ((y > height) && (y <= (2 * height))))
                            {
                                Color pixel = newBitmap.GetPixel(x, (y - height));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(0, G, 0));
                            }
                            else if (((x > width) && (x <= (2 * width))) && ((y > height) && (y <= (2 * height))))
                            {
                                Color pixel = newBitmap.GetPixel((x - width), (y - height));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(0, 0, B));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapSize2;
                pictureBox.Image = newBitmap;
            }
        }

        void add_RGB_ver_extended()  //funkcja skejająca 9 takiech samych obrazów w formacie rgb
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapSize2 = new Bitmap(3 * width, 3 * height);

                for (int x = 0; x < newBitmapSize2.Width; x++)
                {
                    for (int y = 0; y < newBitmapSize2.Height; y++)
                    {
                        try
                        {
                            if (((x > 0) && (x <= width)) && ((y > 0) && (y < height))) //1,1
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(R, G, 0));
                            }
                            else if (((x > width) && (x <= (2 * width))) && ((y > 0) && (y <= height)))   //1,2
                            {
                                Color pixel = newBitmap.GetPixel((x - width), y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(0, G, 0));
                            }
                            else if (((x > 0) && (x <= width)) && ((y > height) && (y <= (2 * height))))   //2,1
                            {
                                Color pixel = newBitmap.GetPixel(x, (y - height));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(R, 0, 0));
                            }
                            else if (((x > width) && (x <= (2 * width))) && ((y > height) && (y <= (2 * height))))   //2,2
                            {
                                Color pixel = newBitmap.GetPixel((x - width), (y - height));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(R, G, B));
                            }
                            else if (((x > 0) && (x <= width)) && ((y > 2 * height) && (y <= 3 * height)))  //1,3
                            {
                                Color pixel = newBitmap.GetPixel(x, (y - (2 * height)));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                int gray = (int)((0.299f * R) + (0.587f * G) + (.114f * B));
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                                //grayscale();
                            }
                            else if (((x > width) && (x <= (2 * width))) && ((y > 2 * height) && (y <= (3 * height))))   //2,3
                            {
                                Color pixel = newBitmap.GetPixel((x - width), (y - (2 * height)));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(R, 0, B));
                            }
                            else if (((x > 2 * width) && (x <= (3 * width))) && ((y > 0) && (y <= height)))  //3,1
                            {
                                Color pixel = newBitmap.GetPixel((x - (2 * width)), y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(0, G, B));

                            }
                            else if (((x > 2 * width) && (x <= (3 * width))) && ((y > height) && (y <= (2 * height))))   //3,2
                            {
                                Color pixel = newBitmap.GetPixel((x - (2 * width)), (y - height));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(0, 0, B));
                            }
                            else if (((x > 2 * width) && (x <= (3 * width))) && ((y > 2 * height) && (y <= (3 * height))))    //3,3
                            {
                                Color pixel = newBitmap.GetPixel((x - (2 * width)), (y - (2 * height)));
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(255 - R, 255 - G, 255 - B));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapSize2;
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_l()  //funkcja odbijająca lewą połowę obrazu 
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int xl = 0, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
                {
                    for (int y = 0; y <= newBitmap.Height; y++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xl, y);
                            newBitmap.SetPixel(xr, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_r()     //funkcja odbijająca prawą połowę obrazu 
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int xl = 0, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
                {
                    for (int y = 0; y < newBitmap.Height; y++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xr, y);
                            newBitmap.SetPixel(xl, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmap;
            }
        }
        void mirror_t()    //funkcja odbijająca górną połowę obrazu 
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    for (int yt = 0, yb = newBitmap.Height; yt < newBitmap.Height; yt++, yb--)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yt);
                            newBitmap.SetPixel(x, yb, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_b()    //funkcja odbijająca dolną połowę obrazu 
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    for (int yt = 0, yb = newBitmap.Height; yt < newBitmap.Height; yt++, yb--)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yb);
                            newBitmap.SetPixel(x, yt, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_vertical()  //funkcja odwracająca wertykalnie lewo-prawo
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int xl = 0, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
                {
                    for (int y = 0; y < newBitmap.Height; y++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xl, y);
                            newBitmapTemp.SetPixel(xr, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmapTemp;
            }
        }

        void mirror_horizontal()    //funkcja odwracająca horyzontalnie góra-dół
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    for (int yt = 0, yb = newBitmap.Height; yt < newBitmap.Height; yt++, yb--)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yt);
                            newBitmapTemp.SetPixel(x, yb, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmapTemp;
            }
        }

        void stich_horizontal()    // funkcja doklejająca to samo zdjęcie z prawej strony
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(2 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int xl = 0, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xl, y);

                            newBitmapTemp.SetPixel(xl, y, pixel);
                            newBitmapTemp.SetPixel(xr, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void stich_vertical()    // funkcja doklejająca to samo zdjęcie od dołu
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, 2 * height);

                for (int x = 0; x < newBitmap.Height; x++)
                {
                    for (int yl = 0, yr = newBitmap.Height; yl < newBitmap.Height; yl++, yr++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yl);
                            newBitmapTemp.SetPixel(x, yl, pixel);
                            newBitmapTemp.SetPixel(x, yr, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_stich_right()    // funkcja doklejająca to samo zdjęcie z prawej strony
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(2 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int xl = 0, xr = 2 * newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xl, y);
                            newBitmapTemp.SetPixel(xl, y, pixel);
                            newBitmapTemp.SetPixel(xr, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_stich_left()    // funkcja doklejająca to samo zdjęcie z lewej strony
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(2 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int xl = 0, xr = 2 * newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xl, y);
                            newBitmapTemp.SetPixel(xl + width, y, pixel);
                            newBitmapTemp.SetPixel(xr - width, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_stich_bottom()    // funkcja doklejająca to samo zdjęcie od dołu
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, 2 * height);

                for (int x = 0; x < newBitmap.Height; x++)
                {
                    for (int yl = 0, yr = 2 * newBitmap.Height; yl < newBitmap.Height; yl++, yr--)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yl);
                            newBitmapTemp.SetPixel(x, yl, pixel);
                            newBitmapTemp.SetPixel(x, yr, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void mirror_stich_top()    // funkcja doklejająca to samo zdjęcie ponad oryginalnym
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, 2 * height);

                for (int x = 0; x < newBitmap.Height; x++)
                {
                    for (int yl = 0, yr = 2 * newBitmap.Height; yl < newBitmap.Height; yl++, yr--)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yl);
                            newBitmapTemp.SetPixel(x, yl + height, pixel);
                            newBitmapTemp.SetPixel(x, yr - height, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void stich_left()    // funkcja doklejająca to samo zdjęcie z prawej strony
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(2 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int xl = 0, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xl, y);
                            newBitmapTemp.SetPixel(xl, y, pixel);
                            newBitmapTemp.SetPixel(xr, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void stich_right()    // funkcja doklejająca to samo zdjęcie z prawej strony
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(2 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int xl = 0, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(xl, y);
                            newBitmapTemp.SetPixel(xl, y, pixel);
                            newBitmapTemp.SetPixel(xr, y, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void stich_top()    // funkcja doklejająca to samo zdjęcie od dołu
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, 2 * height);

                for (int x = 0; x < newBitmap.Height; x++)
                {
                    for (int yl = 0, yr = newBitmap.Height; yl < newBitmap.Height; yl++, yr++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yl);
                            newBitmapTemp.SetPixel(x, yl, pixel);
                            newBitmapTemp.SetPixel(x, yr, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void stich_bottom()    // funkcja doklejająca to samo zdjęcie od dołu
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, 2 * height);

                for (int x = 0; x < newBitmap.Height; x++)
                {
                    for (int yl = 0, yr = newBitmap.Height; yl < newBitmap.Height; yl++, yr++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, yl);
                            newBitmapTemp.SetPixel(x, yl, pixel);
                            newBitmapTemp.SetPixel(x, yr, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void stich_sec_left()    // funkcja doklejająca to samo zdjęcie z prawej strony
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                DialogResult dr2 = openFileDialog1.ShowDialog();
                if (dr2 == DialogResult.OK)
                {
                    file2 = Image.FromFile(openFileDialog1.FileName);
                    newSecondBitmap = new Bitmap(openFileDialog1.FileName);
                    pictureBox1.Image = file2;
                }

                height = Math.Max(pictureBox.Height, newSecondBitmap.Height);

                int width_sum = width + newSecondBitmap.Width;

                Bitmap newBitmapTemp = new Bitmap(width_sum, height);
                Graphics g = Graphics.FromImage(newBitmapTemp);
                g.Clear(Color.Black);

                g.DrawImage(newSecondBitmap, new Point(0, 0));
                g.DrawImage(newBitmap, new Point(newSecondBitmap.Width, 0));

                /*for (int xl = 0, xr = newSecondBitmap.Width; xr < width_sum; xl++, xr++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        try
                        {
                            Color pixel = newSecondBitmap.GetPixel(xl, y);
                            Color pixel2 = newBitmap.GetPixel(xl, y);
                            newBitmapTemp.SetPixel(xl, y, pixel);
                            newBitmapTemp.SetPixel(xr, y, pixel2);
                        }
                        catch (Exception) { newBitmapTemp.SetPixel(xr, y, Color.Black); }
                    }
                }*/
                //newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmapTemp;
            }
        }

        void rotate_right()    // funkcja obracająca zdjęcie o 90 stopni w prawo
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(height, width);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(x, height - y);
                            newBitmapTemp.SetPixel(y, x, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void rotate_left()    // funkcja obracająca zdjęcie o 90 stopni w lewo
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(height, width);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            Color pixel = newBitmap.GetPixel(width - x, y);
                            newBitmapTemp.SetPixel(y, x, pixel);
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rgb()    // funkcja pokazująca składowe rgb w postaci kolejnych zdjęć
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(3 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < 3 * newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, 0, 0));
                            }
                            else if ((x > width) && (x <= 2 * width))
                            {
                                Color pixel = newBitmap.GetPixel(x - width, y);
                                int G = (int)pixel.G;
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb(0, G, 0));
                            }
                            else if ((x > 2 * width) && (x <= 3 * width))
                            {
                                Color pixel = newBitmap.GetPixel(x - (2 * width), y);
                                int B = (int)pixel.B;
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb(0, 0, B));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rgb2()    // funkcja pokazująca składowe rgb w postaci kolejnych zdjęć
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(3 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < 3 * newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((R > G) && (R > B)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                            else if ((x > width) && (x <= 2 * width))
                            {
                                Color pixel = newBitmap.GetPixel(x - width, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if ((G > R) && (G > B))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                            else if ((x > 2 * width) && (x <= 3 * width))
                            {
                                Color pixel = newBitmap.GetPixel(x - (2 * width), y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if ((B > G) && (B > R))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_ycbcr()    // funkcja pokazująca składowe YCbCr w postaci kolejnych zdjęć
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(3 * width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < 3 * newBitmap.Width; x++)
                    {
                        try
                        {
                            //int Y = (int)((0.299 * R) + (0.587 * G) + (0.114 * B));

                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb((int)(0.299 * R), (int)(0.587 * G), (int)(0.114 * B)));
                            }
                            else if ((x > width) && (x <= 2 * width))
                            {
                                Color pixel = newBitmap.GetPixel(x - width, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                int cb = (int)(128 - (0.168736 * R) - (0.331264 * G) + (0.5 * B));
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb((int)(128 - (0.168736 * R)), (int)(128 - (0.331264 * G)), (int)(128 + (0.5 * B))));
                                //newBitmapTemp.SetPixel(x, y, Color.
                            }
                            else if ((x > 2 * width) && (x <= 3 * width))
                            {
                                Color pixel = newBitmap.GetPixel(x - (2 * width), y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                int cr = (int)(128 + (0.5 * R) - (0.418688 * G) - (0.81312 * B));
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb((int)(128 + (0.5 * R)), 128 + (int)((0.418688 * G)), 128 + (int)((0.81312 * B))));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_bR()    
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((R > G) && (R > B)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_bG()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((G > R) && (G > B)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_bB()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((B > G) && (B > R)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rR()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((R > (G + B)) && (B <= (R * 0.65)) && (G <= (R * 0.65))))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rG()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((G > (R + B)) && (B <= (G * 0.65)) && (R <= (G * 0.65))))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rB()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((B > (G + R)) && (R <= (B * 0.65)) && (G <= (B * 0.65))))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rC()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((B * 0.65) >= R) && ((G * 0.65) >= R) && (G > (0.75 * B)) && (B > (0.75 * G)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rM()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((B * 0.65) >= G) && ((R * 0.65) >= G) && (R > (0.65 * B)) && (B > (0.65 * R)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void comp_rY()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;
                                if (((G * 0.65) >= B) && ((G * 0.65) >= B) && (G > (0.6 * R)) && (R > (0.65 * G)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        private void extractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                ex = new Extraction(this);
                ex.ShowDialog();
            }
        }

        void comp_extraction()
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(width, height);

                //int cR = Convert.ToInt32(ex.e_R_textBox.Text);
                //int cG = Convert.ToInt32(ex.e_R_textBox.Text);
                //int cB = Convert.ToInt32(ex.e_R_textBox.Text);

                int cR = Convert.ToInt32(ex.Send_R);
                int cG = Convert.ToInt32(ex.Send_G);
                int cB = Convert.ToInt32(ex.Send_B);

                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        try
                        {
                            if ((x > 0) && (x <= width))
                            {
                                Color pixel = newBitmap.GetPixel(x, y);
                                int R = (int)pixel.R;
                                int G = (int)pixel.G;
                                int B = (int)pixel.B;

                                if (((R == cR) && (G == cG) && (B == cB)))
                                {
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(R, G, B));
                                }
                                else
                                {
                                    int GRAY = (((R) + (G) + (B)) / 3);
                                    newBitmapTemp.SetPixel(x, y, Color.FromArgb(GRAY, GRAY, GRAY));
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapTemp;
                pictureBox.Image = newBitmap;
            }
        }

        void bilinear_x2()   // funkcja zmieniająca rozmiar obrazu w sposób biliniowy tj za pomocą wyliczania średniej/stosunku odleglości
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(2 * width, height);
                Bitmap newBitmapSize2 = new Bitmap(2 * width, 2 * height);
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    for (int x = 0; x < 2 * newBitmap.Width; x++)
                    {
                        try
                        {
                            if (x % 2 == 1)
                            {
                                Color pixel = newBitmap.GetPixel((x + 1) / 2, y);
                                newBitmapTemp.SetPixel(x, y, pixel);
                            }
                            if (x == (newBitmapTemp.Width))
                            {
                                Color pixel = newBitmap.GetPixel(x / 2, y);
                                newBitmapTemp.SetPixel(x, y, pixel);
                            }
                            else
                            {
                                Color pixelP = newBitmap.GetPixel((x / 2), y);
                                Color pixelN = newBitmap.GetPixel((x / 2) + 1, y);
                                int avgR = (int)((pixelP.R + pixelN.R) / 2);
                                int avgG = (int)((pixelP.G + pixelN.G) / 2);
                                int avgB = (int)((pixelP.B + pixelN.B) / 2);
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                for (int x = 0; x < newBitmapTemp.Width; x++)
                {
                    for (int y = 0; y < 2 * newBitmapTemp.Height; y++)
                    {
                        try
                        {
                            if (y % 2 == 1)
                            {
                                Color pixel = newBitmapTemp.GetPixel(x, (y + 1) / 2);
                                newBitmapSize2.SetPixel(x, y, pixel);
                            }
                            if (y == (newBitmapTemp.Height))
                            {
                                Color pixel = newBitmapTemp.GetPixel(x, y / 2);
                                newBitmapSize2.SetPixel(x, y, pixel);
                            }
                            else
                            {
                                Color pixelP = newBitmapTemp.GetPixel(x, (y / 2) - 1);
                                Color pixelN = newBitmapTemp.GetPixel(x, (y / 2) + 1);
                                int avgR = (int)((pixelP.R + pixelN.R) / 2);
                                int avgG = (int)((pixelP.G + pixelN.G) / 2);
                                int avgB = (int)((pixelP.B + pixelN.B) / 2);
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapSize2;
                pictureBox.Image = newBitmap;
            }
        }

        void bilinear_x3()   // funkcja zmieniająca rozmiar obrazu w sposób biliniowy tj za pomocą wyliczania średniej/stosunku odleglości
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapTemp = new Bitmap(((3 * width) - 2), height);
                Bitmap newBitmapSize2 = new Bitmap(((3 * width) - 2), ((3 * height) - 2));
                for (int y = 0; y <= newBitmap.Height; y++)
                {
                    for (int x = 0; x <= 3 * newBitmap.Width; x++)
                    {
                        try
                        {
                            if (x % 3 == 1)
                            {
                                Color pixel = newBitmap.GetPixel((x + 2) / 3, y);
                                newBitmapTemp.SetPixel(x, y, pixel);
                            }
                            if (x == (newBitmapTemp.Width))
                            {
                                Color pixel = newBitmap.GetPixel(x / 3, y);
                                newBitmapTemp.SetPixel(x, y, pixel);
                            }
                            if (x == (newBitmapTemp.Width - 1))
                            {
                                Color pixel = newBitmap.GetPixel((x + 1) / 3, y);
                                newBitmapTemp.SetPixel(x, y, pixel);
                            }
                            if (x % 3 == 2)
                            {
                                Color pixelP = newBitmap.GetPixel((x + 1) / 3, y);
                                Color pixelN = newBitmap.GetPixel(((x + 1) / 3) + 1, y);
                                int avgR = (int)(((pixelP.R * 2) + (pixelN.R)) / 3);
                                int avgG = (int)(((pixelP.G * 2) + (pixelN.G)) / 3);
                                int avgB = (int)(((pixelP.B * 2) + (pixelN.B)) / 3);
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                            }
                            if (x % 3 == 0)
                            {
                                Color pixelP = newBitmap.GetPixel(x / 3, y);
                                Color pixelN = newBitmap.GetPixel((x / 3) + 1, y);
                                int avgR = (int)(((pixelP.R) + (pixelN.R * 2)) / 3);
                                int avgG = (int)(((pixelP.G) + (pixelN.G * 2)) / 3);
                                int avgB = (int)(((pixelP.B) + (pixelN.B * 2)) / 3);
                                newBitmapTemp.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                for (int x = 0; x <= newBitmapTemp.Width; x++)
                {
                    for (int y = 0; y <= 3 * newBitmapTemp.Height; y++)
                    {
                        try
                        {
                            if (y % 3 == 1)
                            {
                                Color pixel = newBitmapTemp.GetPixel(x, (y + 2) / 3);
                                newBitmapSize2.SetPixel(x, y, pixel);
                            }
                            if (y == (newBitmapTemp.Height))
                            {
                                Color pixel = newBitmapTemp.GetPixel(x, y / 3);
                                newBitmapSize2.SetPixel(x, y, pixel);
                            }
                            if (y == (newBitmapTemp.Height - 1))
                            {
                                Color pixel = newBitmapTemp.GetPixel(x, (y + 1) / 3);
                                newBitmapSize2.SetPixel(x, y, pixel);
                            }
                            if (y % 3 == 2)
                            {
                                Color pixelP = newBitmapTemp.GetPixel(x, (y + 1) / 3);
                                Color pixelN = newBitmapTemp.GetPixel(x, ((y + 1) / 3) + 1);
                                int avgR = (int)(((pixelP.R * 2) + (pixelN.R)) / 3);
                                int avgG = (int)(((pixelP.G * 2) + (pixelN.G)) / 3);
                                int avgB = (int)(((pixelP.B * 2) + (pixelN.B)) / 3);
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                            }
                            if (y % 3 == 0)
                            {
                                Color pixelP = newBitmapTemp.GetPixel(x, y / 3);
                                Color pixelN = newBitmapTemp.GetPixel(x, ((y / 3) + 1));
                                int avgR = (int)(((pixelP.R) + (pixelN.R * 2)) / 3);
                                int avgG = (int)(((pixelP.G) + (pixelN.G * 2)) / 3);
                                int avgB = (int)(((pixelP.B) + (pixelN.B * 2)) / 3);
                                newBitmapSize2.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                            }
                        }
                        catch (Exception) { }
                    }
                }
                pictureBox.Image = newBitmapSize2;
            }
        }

        void nearest_neighbour_x3()   // funkcja zmieniająca rozmiar za pomoccą metody najbliższego sąsiada
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }
            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                Bitmap newBitmapSize2 = new Bitmap(((3 * width) - 2), ((3 * height) - 2));

                for (int x = 0; x < 3 * newBitmap.Width; x++)
                {
                    for (int y = 0; y < 3 * newBitmap.Height; y++)
                    {
                        try
                        {
                            if ((x % 3 == 1) && (y % 3 == 1))
                            {
                                Color pixel = newBitmap.GetPixel((x + 2) / 3, (y + 2) / 3);
                                newBitmapSize2.SetPixel(x - 1, y - 1, pixel);
                                newBitmapSize2.SetPixel(x - 1, y, pixel);
                                newBitmapSize2.SetPixel(x - 1, y + 1, pixel);
                                newBitmapSize2.SetPixel(x, y - 1, pixel);
                                newBitmapSize2.SetPixel(x, y, pixel);
                                newBitmapSize2.SetPixel(x, y + 1, pixel);
                                newBitmapSize2.SetPixel(x + 1, y - 1, pixel);
                                newBitmapSize2.SetPixel(x + 1, y, pixel);
                                newBitmapSize2.SetPixel(x + 1, y + 1, pixel);
                            }
                        }
                        catch (Exception) { }
                    }
                }
                newBitmap = newBitmapSize2;
                pictureBox.Image = newBitmap;
            }
        }

        /*void bicubic()   // funkcja zmieniająca rozmiar obrazu za pomocą metody bicubic  /  nie moja funkcja także nie zadziała
        {
            if (!opened)
            {
                MessageBox.Show("Open an Image then apply changes");
            }

            else
            {
                int width = pictureBox.Image.Width;
                int height = pictureBox.Image.Height;

                int bytesCount = width * height * bitmapSource.Format.BitsPerPixel / 8;

                int pixels = new byte[bytesCount];

                bitmapSource.CopyPixels(pixels, bitmapSource.Format.BitsPerPixel / 8 * oldPixelWidth, 0);

                int newPixls = new byte[(int)(pixels.Length * scaleRate * scaleRate)];

                await Task.Run(() =>;
                {
                    int oldStride = oldPixelWidth * 4;
                    int newStride = (int)(oldPixelWidth * 4 * scaleRate);

                    int newPixelHeiht = (int)(oldPixelHeight * scaleRate);
                    int newPixelWidth = (int)(oldPixelWidth * scaleRate);
                    for (int i = 0; i < newPixelHeiht; i++)
                    {
                        for (int j = 0; j < newPixelWidth; j++)
                        {
                            int srcX = j * oldPixelWidth / (double)(oldPixelWidth * scaleRate);
                            int srcY = i * oldPixelHeight / (double)(oldPixelHeight * scaleRate);
                            int x = (int)srcX;
                            int u = srcX - x;
                            if (x + 2 >= oldPixelWidth)
                            {
                                x -= 2;
                            }
                            if (x - 1 < 0)
                            {
                                x += 1;
                            }
                            int y = (int)srcY;
                            int v = srcY - y;
                            if (y + 1 >= oldPixelHeight)
                            {
                                y -= 1;
                            }
                            if (y - 2 < 0)
                            {
                                y += 2;
                            }
                            int A = Matrix<double>.Build;
                            double[] uInter = new double[4] { CalcInterpolation(1 + u), CalcInterpolation(u), CalcInterpolation(1 - u), CalcInterpolation(2 - u) };
                            double[] vInter = new double[4] { CalcInterpolation(1 + v), CalcInterpolation(v), CalcInterpolation(1 - v), CalcInterpolation(2 - v) };

                            double[] pixelValue = new double[16];

                            int matrixA = A.Dense(1, 4, uInter);

                            int matrixC = A.Dense(4, 1, vInter);

                            byte[] newPiexl = new byte[4];

                            for (int index = 0; index < 3; index++)
                            {
                                int count = 0;
                                for (int r = y - 2; r < y + 2; r++)
                                {
                                    for (int l = x - 1; l < x + 3; l++)
                                    {
                                        pixelValue[count++] = GetPixel(4 * (r * oldPixelWidth + l), index);
                                    }
                                }
                                int matrixB = A.Dense(4, 4, pixelValue);

                                var newPiexlMatric = matrixA * matrixB * matrixC;

                                newPiexl[index] = (byte)newPiexlMatric[0, 0];
                            }

                            int newPos = 4 * (i * newPixelWidth + j);

                            newPixls[newPos + 0] = newPiexl[0];
                            newPixls[newPos + 1] = newPiexl[1];
                            newPixls[newPos + 2] = newPiexl[2];
                            newPixls[newPos + 3] = 255;
                        }
                    }
                    int newSource = BitmapSource.Create((int)(oldPixelWidth * scaleRate), (int)(oldPixelHeight * scaleRate), bitmapSource.DpiX, bitmapSource.DpiY, PixelFormats.Bgr32, null, newPixls, (int)(4 * oldPixelWidth * scaleRate));

                    return newSource;

                    byte GetPixel(int pos, int index)
                    {
                        return (byte)(pixels[pos + index]);
                    }
                    double CalcInterpolation(double value)
                    {
                        if (value < 0)
                        {
                            value = -value;
                        }
                        if (value >= 2)
                            return 0;
                        if (value >= 1)
                        {
                            return 4 - 8 * value + 5 * value * value - value * value * value;
                        }
                        return 1 - 2 * value * value + value * value * value;
                    }
                }
            }
        }*/

        Bitmap newBitmap;        // 
        Bitmap newBitmapTemp;
        Bitmap newBitmapTemp2;
        Bitmap newSecondBitmap;
        Bitmap newBitmapSize2;        // 2x większe niż newBitmap w obu kierunkach
        Image file;              // implemerntacja zmiennych wykorzystywanych w funkcjach
        Image file2;
        //Image original;          // iplementacja 
        //Image file_2;
        int lastCol = 0;         //
        Boolean opened = false;  //
        
        //
        // poniżej znajduje się funkcjonalność odpowiadająca za reakcje na poszczególne 
        // przesunięcia/kliknięcia w oknie aplikacji
        //

        private void None_button_Click(object sender, EventArgs e)   // obsługa przycisku None resetującego zdjęcie
        {
            reload();
            Red_Bar.Value = 0;
            Green_Bar.Value = 0;
            Blue_Bar.Value = 0;
            Contrast_Bar.Value = 25;
            Brightness_Bar.Value = 1;
            Gamma_Bar.Value = 25;
            Gamma_Value_label.Text = Gamma_Bar.Value.ToString();
            Contrast_Value_label.Text = Contrast_Bar.Value.ToString();
            Threshold_Bar.Value = 0;
            Color_Shallowing_Bar.Value = 0;
            Threshold_Value_label.Text = Threshold_Bar.Value.ToString();
            Color_Shallowing_Value_label.Text = Color_Shallowing_Bar.Value.ToString();
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

        private void Gamma_Bar_Scroll(object sender, EventArgs e)
        {
            gamma();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void openImageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grayscale();
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sepia();
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blur();
        }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invert();
        }

        private void edgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edge();
        }

        private void mirrorLeftSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_l();
        }

        private void mirrorRightSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_r();
        }

        private void mirrorTopSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_t();
        }

        private void mirrorBottomSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_b();
        }

        private void invertHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_horizontal();
        }

        private void invertVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_vertical();
        }
         
        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ShowHistogram(!config.histogramVisible);
        }

        private void x2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bilinear_x2();
        }

        private void x3ToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            bilinear_x3();
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nearest_neighbour_x3();
        }

        private void addRGBVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_RGB_ver();
        }

        private void addRGBV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add_RGB_ver_extended();
        }

        private void rightSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_stich_right();
        }

        private void leftSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_stich_left();
        }

        private void topSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_stich_top();
        }

        private void bottomSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mirror_stich_bottom();
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotate_left();
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotate_right();
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rgb();
        }

        private void rGB2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rgb2();
        }

        private void rExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_bR();
        }

        private void gExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_bG();
        }

        private void bExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_bB();
        }

        private void yCbCrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_ycbcr();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //wyśietlanie wiadomości

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What is not working yet:" + "\n" + "- Drawing" + "\n" + "- Statistic" + "\n" + "- Resize(BiCubic)" + "\n" 
                + "- Stich(Stich different images)" + "\n" + "- Components(YCbCr)" + "\n" + "- Extraction");
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program contain:" + "\n" + "- Menu(top part of the screen)" + "\n" + 
                "- 3 Trackbars boxes(left, right and bottom side of the screen" + "\n" +
                "- Main window(middle-top part of window)" + "\n" + 
                "- Side window(left-bottom part of window)" + "\n" + 
                "- Button to cancel all changes(right-bottom corner of window)" +
                "" + "\n" +
                "" + "\n" +
                "To start click on File -> Open image. Now you can start applying changest to your picture." + "\n" +
                "Some of effects cant work combined(yet) so for now it is suggested to save your work between applying those changes to your image." + "\n" +
                "If you want to save your work click File -> Save image -> then choose your prefered extension." +
                "" + "\n" + "\n" +
                "Enjoy your time using this app"
                
                );
        }

        private void Threshold_Bar_Scroll(object sender, EventArgs e)
        {
            threshold();
        }

        private void Color_Shallowing_Bar_Scroll(object sender, EventArgs e)
        {
            color_shallowing();
        }

        private void horizontalStichToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stich_horizontal();
        }

        private void verticalStichToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stich_vertical();
        }

        private void Blur_Bar_Scroll(object sender, EventArgs e)
        {
            blur();
        }

        //private void extractionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
            //Extraction x = new Extraction(this);
            //x.ShowDialog();
        //}

        private void edgeWhiteOnBlackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edge_WoB();
        }

        private void firstLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stich_sec_left();
        }

        private void rRExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rR();
        }

        private void rGExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rG();
        }

        private void rBExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rB();
        }

        private void rCExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rC();
        }

        private void rMExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rM();
        }

        private void rYExtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comp_rY();
        }
    }
}