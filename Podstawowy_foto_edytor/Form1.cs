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
        private Histogram histogramWin = new Histogram();

        public Form1()
        {
            InitializeComponent();

            //histogramWin.DockStateChanged += new EventHandler(histogram_DockStateChanged);

            //histogramWin.VisibleChanged += new EventHandler(histogram_VisibleChanged);
        }


        void openImage()  //funkcja otwierająca obraz 
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                newBitmapTemp = new Bitmap(openFileDialog1.FileName);
                newBitmapSize2 = new Bitmap(openFileDialog1.FileName);
                pictureBox.Image = file;
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
                    pictureBox.Image.Save(sfd.FileName, format);
                }
            }
            else { MessageBox.Show("No image loaded, first upload image "); }
        }
        /*private void ShowHistogram(bool show)
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
                pictureBox.Image = bmpInverted;
            }
        }

        void brightness()   //funkcja zmieniająca jasność obrazu
        {
            float changebrightness = Brightness_Bar.Value / 255.0f;

            int val = (int)(changebrightness * 255);

            Brightness_Value_label.Text = val.ToString();

            //reload();
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
                pictureBox.Image = bmpInverted;


            }
        }

        void contrast()  //funkcja zmieniająca kontrast obrazu
        {
            float changecontrast = Contrast_Bar.Value * 0.041f;

            int val = (int)(changecontrast * 24.4);

            Contrast_Value_label.Text = val.ToString();

            //reload();
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
                pictureBox.Image = bmpInverted;


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

        void sepia()   //funkcja zmieniająca kolory w odcienie szarości
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

        void blur()   // funkcja rozmywająca obraz poprzez uśrednianiewartości pikseli
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
                pictureBox.Image = newBitmap;
            }
        }

        void invert()   // funkcja odwracająca kolory(negatyw)
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
            pictureBox.Image = newBitmap;
        }

        void edge()   //funkcja znajdująca krawędzie na podstawie różnicy wartości piikseli( imitacja emboss)
        {
            Bitmap nB = new Bitmap(newBitmap.Width, newBitmap.Height);

            for (int x = 1; x <= newBitmap.Width - 1; x++)
            {
                for (int y = 1; y <= newBitmap.Height - 1; y++)
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

        void mirror_l()  //funkcja odbijająca lewą połowę obrazu 
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
            pictureBox.Image = newBitmap;
        }

        void mirror_r()     //funkcja odbijająca prawą połowę obrazu 
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
            pictureBox.Image = newBitmap;
        }
        void mirror_t()    //funkcja odbijająca górną połowę obrazu 
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
            pictureBox.Image = newBitmap;
        }

        void mirror_b()    //funkcja odbijająca dolną połowę obrazu 
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
            pictureBox.Image = newBitmap;
        }

        void mirror_horizontal()  //funkcja odwracająca horyzontalnielewo-prawo
        {
            for (int xl = 1, xr = newBitmap.Width; xl < newBitmap.Width; xl++, xr--)
            {
                for (int y = 1; y < newBitmap.Height; y++)
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

        void mirror_vertical()    //funkcja odwracająca wertykalnie góra-dół
        {
            for (int x = 1; x < newBitmap.Width; x++)
            {
                for (int yt = 1, yb = newBitmap.Height; yt < newBitmap.Height; yt++, yb--)
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
                for (int y = 1; y < newBitmap.Height; y++)
                {
                    for (int x = 1; x < 2 * newBitmap.Width; x++)
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
                for (int x = 1; x < newBitmapTemp.Width; x++)
                {
                    for (int y = 1; y < 2 * newBitmapTemp.Height; y++)
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

        void add_RGB_ver()  //funkcja skejająca 4 takie same obrazy w formacie rgb
        {
            int width = pictureBox.Image.Width;
            int height = pictureBox.Image.Height;

            Bitmap newBitmapSize2 = new Bitmap(2 * width, 2 * height);

            for (int x = 1; x < newBitmapSize2.Width; x++)
            {
                for (int y = 1; y < newBitmapSize2.Height; y++)
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

        void add_RGB_ver_extended()  //funkcja skejająca 4 takie same obrazy w formacie rgb
        {
            int width = pictureBox.Image.Width;
            int height = pictureBox.Image.Height;

            Bitmap newBitmapSize2 = new Bitmap(3 * width, 3 * height);

            for (int x = 1; x < newBitmapSize2.Width; x++)
            {
                for (int y = 1; y < newBitmapSize2.Height; y++)
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
                for (int y = 1; y < newBitmap.Height; y++)
                {
                    for (int x = 1; x < 3 * newBitmap.Width; x++)
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
                for (int x = 1; x < newBitmapTemp.Width; x++)
                {
                    for (int y = 1; y < 3 * newBitmapTemp.Height; y++)
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

        void nearest_neighbour_x3()   // funkcja rozmywająca obraz poprzez uśrednianiewartości pikseli
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

                for (int x = 1; x < 3 * newBitmap.Width; x++)
                {
                    for (int y = 1; y < 3 * newBitmap.Height; y++)
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

        /*void bicubic()   // funkcja 
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
        Bitmap newBitmapSize2;        // 2x większe niż newBitmap w obu kierunkach
        Image file;              // implemerntacja zmiennych wykorzystywanych w funkcjach
        int lastCol = 0;         //
        Boolean opened = false;  //

        //
        // poniżej znajduje się funkcjonalność odpowiadająca za reakcje na poszczególne 
        // przesunięcia/kliknięcia w oknie aplikacji
        //

        private void button1_Click(object sender, EventArgs e)
        {
            reload();
            Red_Bar.Value = 0;
            Green_Bar.Value = 0;
            Blue_Bar.Value = 0;
            Contrast_Bar.Value = 25;
            Brightness_Bar.Value = 0;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ShowHistogram(!config.histogramVisible);
        }

        private void x2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bilinear_x2();
        }

        private void x3ToolStripMenuItem1_Click(object sender, EventArgs e)
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
    }
}