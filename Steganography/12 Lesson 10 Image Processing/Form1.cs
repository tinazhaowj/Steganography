using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace _12_Lesson_10_Image_Processing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //delcare global variable
        Bitmap bitmapImage;
        Color[,] ImageArray = new Color[320, 240];
        Color[,] RestoreArray = new Color[320, 240];
        Stream myStream = null;

        //open a picture
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Image Browser";

            //open the picture
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    Image newImage = Image.FromStream(myStream);
                    bitmapImage = new Bitmap(newImage, 320, 240);
                    pictureBox1.Image = bitmapImage;
                    myStream.Close();
                }
            }

            SetArayFromBitmap();
        }

        private void SetBitmapFromAray()
        {
            for (int row = 0; row < 320; row++)
                for (int col = 0; col < 240; col++)
                {
                    bitmapImage.SetPixel(row, col, ImageArray[row, col]);
                }
        }

        private void SetArayFromBitmap()
        {
            for (int row = 0; row < 320; row++)
            {
                for (int col = 0; col < 240; col++)
                {
                    ImageArray[row, col] = bitmapImage.GetPixel(row, col);
                    RestoreArray[row, col] = ImageArray[row, col];
                }
            }
        }

        //blue filter
        private void btnBlue_Click(object sender, EventArgs e)//blue
        {
            if (bitmapImage == null)
                return;

            Byte Blue;

            int iWidth = 320;
            int iHeight = 240;

            //pick out all the blue elements in the pixel
            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    Color col = ImageArray[i, j];

                    Blue = col.B;

                    Color newColor = Color.FromArgb(255, 0, 0, Blue);
                    ImageArray[i, j] = newColor;

                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        //negative
        private void btnNegative_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            Byte Red, Green, Blue;

            int iWidth = 320;
            int iHeight = 240;

            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    Color col = ImageArray[i, j];

                    Red = col.R;
                    Green = col.G;
                    Blue = col.B;

                    //find the opposite of the actually color 
                    Color newColor = Color.FromArgb(255, 255 - Red, 255 - Green, 255 - Blue);
                    ImageArray[i, j] = newColor;

                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnPolarize_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            Byte Red, Green, Blue;

            int iWidth = 320;
            int iHeight = 240;

            int averageR = 0;
            int averageG = 0;
            int averageB = 0;

            //find the average of color element in the picture
            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    Color col = ImageArray[i, j];
                    Red = col.R;
                    Green = col.G;
                    Blue = col.B;

                    averageR = averageR + Red;
                    averageG = averageG + Green;
                    averageB = averageB + Blue;
                }
            }

            averageR = averageR / (iWidth * iHeight);
            averageG = averageG / (iWidth * iHeight);
            averageB = averageB / (iWidth * iHeight);

            //compare the color in the pixel to the color in the picture
            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    Color col = ImageArray[i, j];

                    Red = col.R;
                    Green = col.G;
                    Blue = col.B;

                    if (col.R > averageR)
                        Red = 225;
                    else
                        Red = 0;
                    if (col.G > averageG)
                        Green = 225;
                    else
                        Green = 0;
                    if (col.B > averageB)
                        Blue = 225;
                    else
                        Blue = 0;

                    Color newColor = Color.FromArgb(255, Red, Green, Blue);
                    ImageArray[i, j] = newColor;

                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnGrey_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            Byte Red, Green, Blue;

            int iWidth = 320;
            int iHeight = 240;

            int average = 0;

            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    Color col = ImageArray[i, j];
                    Red = col.R;
                    Green = col.G;
                    Blue = col.B;

                    average = (Red + Green + Blue) / 3;//find the average of the 3 color element

                    Color newColor = Color.FromArgb(255, average, average, average);//set as the color of the pixel
                    ImageArray[i, j] = newColor;
                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        //red filter
        private void btnRed_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            Byte Red;

            int iWidth = 320;
            int iHeight = 240;

            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    Color col = ImageArray[i, j];

                    Red = col.R;

                    Color newColor = Color.FromArgb(255, Red, 0, 0);
                    ImageArray[i, j] = newColor;

                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        //green filter
        private void btnGreen_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            /// Process the array data here!!!

            Byte Green;

            int iWidth = 320;
            int iHeight = 240;

            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    Color col = ImageArray[i, j];

                    //Get the green element of the color
                    Green = col.G;

                    Color newColor = Color.FromArgb(255, 0, Green, 0);
                    ImageArray[i, j] = newColor;

                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnFlipVertically_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            int iWidth = 320;
            int iHeight = 240;
            Color temp;

            //row doesn;t change, change the columns
            for (int i = 0; i < iWidth / 2; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    temp = ImageArray[i, j];
                    ImageArray[i, j] = ImageArray[iWidth - i - 1, j];
                    ImageArray[iWidth - i - 1, j] = temp;
                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                for (int row = 0; row < 320; row++)
                {
                    for (int col = 0; col < 240; col++)
                    {
                        bitmapImage.SetPixel(row, col, RestoreArray[row, col]);//cover the picture with original image
                        ImageArray[row, col] = RestoreArray[row, col];//set the original picture as the picture
                    }
                }
                pictureBox1.Image = bitmapImage;
            }
            catch
            {
                return;
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            int iWidth = 320;
            int iHeight = 240;
            Color temp;

            //switch the frist pixel with the last pixel
            for (int i = 0; i < iWidth / 2; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    temp = ImageArray[i, j];
                    ImageArray[i, j] = ImageArray[iWidth - i - 1, iHeight - 1 - j];
                    ImageArray[iWidth - i - 1, iHeight - 1 - j] = temp;
                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnBlur_Click(object sender, EventArgs e)
        {
            if (bitmapImage == null)
                return;

            Color c, c1, c2, c3, c4, c5, c6, c7, c8;

            int iWidth = 320;
            int iHeight = 240;

            int averageR = 0;
            int averageG = 0;
            int averageB = 0;

            //find the location of each pixel that needed
            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    c = ImageArray[i, j];
                    c1 = ImageArray[i, j];
                    c2 = ImageArray[i, j];
                    c3 = ImageArray[i, j];
                    c4 = ImageArray[i, j];
                    c5 = ImageArray[i, j];
                    c6 = ImageArray[i, j];
                    c7 = ImageArray[i, j];
                    c8 = ImageArray[i, j];

                    /*c1 = ImageArray[i, j-1];
                    c2 = ImageArray[i+1, j-1];
                    c3 = ImageArray[i+1, j];
                    c4 = ImageArray[i+1, j+1];
                    c5 = ImageArray[i, j+1];
                    c6 = ImageArray[i-1, j+1];
                    c7 = ImageArray[i-1, j];
                    c8 = ImageArray[i-1, j-1];*/


                    if (i == 0 && j == 0)//top left corner
                    {
                        c3 = ImageArray[i + 1, j];
                        c4 = ImageArray[i + 1, j + 1];
                        c5 = ImageArray[i, j + 1];
                    }
                    else if (i == 319 && j == 0)//top right corner
                    {
                        c5 = ImageArray[i, j + 1];
                        c6 = ImageArray[i - 1, j + 1];
                        c7 = ImageArray[i - 1, j];
                    }
                    else if (i == 0 && j == 239)//bottom left corner
                    {
                        c1 = ImageArray[i, j - 1];
                        c2 = ImageArray[i + 1, j - 1];
                        c3 = ImageArray[i + 1, j];
                    }
                    else if (i == 319 && j == 239)//bottome right corner
                    {
                        c1 = ImageArray[i, j - 1];
                        c7 = ImageArray[i - 1, j];
                        c8 = ImageArray[i - 1, j - 1];
                    }
                    else if (j == 0)//first row
                    {
                        c3 = ImageArray[i + 1, j];
                        c4 = ImageArray[i + 1, j + 1];
                        c5 = ImageArray[i, j + 1];
                        c6 = ImageArray[i - 1, j + 1];
                        c7 = ImageArray[i - 1, j];
                    }
                    else if (j == 239)//last row
                    {
                        c1 = ImageArray[i, j - 1];
                        c2 = ImageArray[i + 1, j - 1];
                        c3 = ImageArray[i + 1, j];
                        c7 = ImageArray[i - 1, j];
                        c8 = ImageArray[i - 1, j - 1];
                    }
                    else if (i == 0)//first column
                    {
                        c1 = ImageArray[i, j - 1];
                        c2 = ImageArray[i + 1, j - 1];
                        c3 = ImageArray[i + 1, j];
                        c4 = ImageArray[i + 1, j + 1];
                        c5 = ImageArray[i, j + 1];
                    }
                    else if (i == 319)//last column
                    {
                        c1 = ImageArray[i, j - 1];
                        c5 = ImageArray[i, j + 1];
                        c6 = ImageArray[i - 1, j + 1];
                        c7 = ImageArray[i - 1, j];
                        c8 = ImageArray[i - 1, j - 1];
                    }
                    else
                    {
                        c1 = ImageArray[i, j - 1];
                        c2 = ImageArray[i + 1, j - 1];
                        c3 = ImageArray[i + 1, j];
                        c4 = ImageArray[i + 1, j + 1];
                        c5 = ImageArray[i, j + 1];
                        c6 = ImageArray[i - 1, j + 1];
                        c7 = ImageArray[i - 1, j];
                        c8 = ImageArray[i - 1, j - 1];
                    }

                    //find the average
                    averageR = (c.R + c1.R + c2.R + c3.R + c4.R + c5.R + c6.R + c7.R + c8.R) / 9;
                    averageG = (c.G + c1.G + c2.G + c3.G + c4.G + c5.G + c6.G + c7.G + c8.G) / 9;
                    averageB = (c.B + c1.B + c2.B + c3.B + c4.B + c5.B + c6.B + c7.B + c8.B) / 9;

                    Color newColor = Color.FromArgb(255, averageR, averageG, averageB);
                    ImageArray[i, j] = newColor;
                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnSwitch_Click(object sender, EventArgs e)////////switch
        {
            if (bitmapImage == null)
                return;

            int iWidth = 320;
            int iHeight = 240;
            Color temp;

            //switch the top left corner of the two smaller square
            for (int i = 0; i < iWidth / 2; i++)
            {
                for (int j = 0; j < iHeight / 2; j++)
                {
                    temp = ImageArray[i, j];
                    ImageArray[i, j] = ImageArray[i + (iWidth / 2), j + (iHeight / 2)];
                    ImageArray[i + (iWidth / 2), j + (iHeight / 2)] = temp;
                }
            }

            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnScroll_Click(object sender, EventArgs e)////////scroll
        {
            //enable the bar
            vScrollBar1.Enabled = true;
        }

        private void vScrollBar1_Scroll_Up(object sender, ScrollEventArgs e)
        {
            if (bitmapImage == null)
                return;

            Color[,] ScrollArray = new Color[320, 240];

            int iWidth = 320;
            int iHeight = 240;

            for (int i = 0; i < iWidth; i++)
            {
                for (int j = 0; j < iHeight; j++)
                {
                    if (j >= 15)//moving the image up by shorten the height
                        ScrollArray[i, j - 15] = ImageArray[i, j];
                    else//adds 225 if it's smaller
                        ScrollArray[i, j + 225] = ImageArray[i, j];
                }
            }
            for (int i = 0; i < iWidth; i++)//output the image
            {
                for (int j = 0; j < iHeight; j++)
                {
                    ImageArray[i, j] = ScrollArray[i, j];
                }
            }
            SetBitmapFromAray();
            pictureBox1.Image = bitmapImage;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                if (bitmapImage == null)
                    return;

                int iWidth = 320;
                int iHeight = 240;

                int i, j;

                for (j = 0; j < iHeight; j++)
                {
                    for (i = 1; i < iWidth; i++)
                    {
                        Color col = ImageArray[i, j];

                        int k = i - 1;//taking tow pixel

                        while (k >= 0)
                        {
                            if (ImageArray[k, j].R > col.R)//compare the red element and sort them
                                ImageArray[k + 1, j] = ImageArray[k, j];
                            else
                                break;
                            k = k - 1;
                        }
                        ImageArray[k + 1, j] = col;
                    }
                }
                SetBitmapFromAray();
                pictureBox1.Image = bitmapImage;
            }
            catch
            {
                MessageBox.Show("Wrong!");
            }
        }

        private void btnPixellate_Click(object sender, EventArgs e)
        {
            try
            {
                if (bitmapImage == null)
                    return;

                int iWidth = 320;
                int iHeight = 240;

                for (int k = 0; k < iWidth; k = k + 4)//getting the surrounding 4*4 block
                {
                    for (int l = 0; l < iHeight; l = l + 4)
                    {
                        for (int i = 0; i < 4; i++)//make each pixel in that block equal to the top left corner
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                ImageArray[k + i, l + j] = ImageArray[k, l];
                            }
                        }
                    }
                }

                SetBitmapFromAray();
                pictureBox1.Image = bitmapImage;
            }
            catch
            {
                MessageBox.Show("Wrong!");
            }
        }

        private void btnTile_Click(object sender, EventArgs e)
        {
            try
            {
                if (bitmapImage == null)
                    return;

                int iWidth = 320;
                int iHeight = 240;
                Color[,] TileArray = new Color[160, 120];
                int averageR = 0, averageG = 0, averageB = 0;
                Color c1, c2, c3, c4;


                //Bitmap newpic = new Bitmap(bitmapImage, new Size(iWidth / 2, iHeight / 2));//first tile

                for (int i = 0; i < iWidth; i = i + 2)/////the small tile
                {
                    for (int j = 0; j < iHeight; j = j + 2)
                    {
                        c1 = ImageArray[i, j];
                        c2 = ImageArray[i, j + 1];
                        c3 = ImageArray[i + 1, j];
                        c4 = ImageArray[i + 1, j + 1];

                        averageR = (c1.R + c2.R + c3.R + c4.R) / 4;
                        averageG = (c1.G + c2.G + c3.G + c4.G) / 4;
                        averageB = (c1.B + c2.B + c3.B + c4.B) / 4;

                        Color newColor = Color.FromArgb(255, averageR, averageG, averageB);
                        TileArray[i / 2, j / 2] = newColor;
                    }
                }
                for (int i = 0; i < 160; i++)//put the samll tile in top left
                {
                    for (int j = 0; j < 120; j++)
                    {
                        ImageArray[i, j] = TileArray[i, j];
                    }
                }

                for (int i = 0; i < 160; i++)//put the samll tile in top right
                {
                    for (int j = 0; j < 120; j++)
                    {
                        ImageArray[i + 160, j] = TileArray[159 - i, j];
                    }
                }
                for (int i = 0; i < 160; i++)//put the samll tile in bottom left
                {
                    for (int j = 0; j < 120; j++)
                    {
                        ImageArray[i, j + 120] = TileArray[i, 119 - j];
                    }
                }
                for (int i = 0; i < 160; i++)//put the samll tile in bottom right
                {
                    for (int j = 0; j < 120; j++)
                    {
                        ImageArray[i + 160, j + 120] = TileArray[159 - i, 119 - j];
                    }
                }
                SetBitmapFromAray();
                pictureBox1.Image = bitmapImage;
            }
            catch
            {
                MessageBox.Show("Wrong!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (bitmapImage == null)
                    return;

                ////convert 
                //ImageCodecInfo myImageCodecInfo = GetEncoderInfo("image/jpeg");
                //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                //EncoderParameter myEncoderParameter;
                //EncoderParameters myEncoderParameters;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();//create dialog
                saveFileDialog1.Filter = "Bitmap|*.bmp";//formats
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.Title = "Save";//title of the popout window       
                //DialogResult r = saveFileDialog1.ShowDialog();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        bitmapImage.Save(saveFileDialog1.FileName);
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                richtxtHide.Clear();
            }
        }

        //private static ImageCodecInfo GetEncoderInfo(String mimeType)
        //{
        //    int j;
        //    ImageCodecInfo[] encoders;
        //    encoders = ImageCodecInfo.GetImageEncoders();
        //    for (j = 0; j < encoders.Length; ++j)
        //    {
        //        if (encoders[j].MimeType == mimeType)
        //            return encoders[j];
        //    }
        //    return null;
        //}

        private void btnHide_Click(object sender, EventArgs e)
        {
            try
            {
                if (bitmapImage == null)
                    return;

                string strHide = richtxtHide.Text;
                //strHide = strHide.Length.ToString("000") + strHide;
                char ch;
                byte ASCII;
                int icounter = 29;
                int j = 0;
                int key = 30;
                byte blength = (byte)richtxtHide.Text.Length;

                Color lengthColor = Color.FromArgb(255, blength, blength, blength);//the first pixel is the length
                ImageArray[0, 0] = lengthColor;

                for (int i = 0; i < richtxtHide.Text.Length; i++)
                {
                    ch = strHide[i];//getting each char
                    ASCII = (byte)(ch);//convert to ascii

                    if (icounter > 319)//if reach the end of the row, go to the next row
                    {
                        icounter = icounter - 319;
                        j++;
                    }

                    Color newColor = Color.FromArgb(255, ASCII, ASCII, ASCII);//hidding the messages
                    ImageArray[icounter, j] = newColor;

                    icounter = icounter + key;
                }
                Color keyColor = Color.FromArgb(255, key, key, key);//the last pixel is the key
                ImageArray[319, 239] = keyColor;

                richtxtHide.Clear();
                SetBitmapFromAray();
                pictureBox1.Image = bitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                richtxtHide.Clear();
            }
        }

        private void btnDecipher_Click(object sender, EventArgs e)
        {
            try
            {
                if (bitmapImage == null)
                    return;

                string strDecipher = "";
                char ch;
                byte ASCII;
                int icounter = 29, j = 0;
                int ilength = ImageArray[0, 0].R;
                int key = ImageArray[319, 239].R;// get the key from the last pixel

                for (int i = 0; i < ilength; i++)//find out the message
                {
                    if (icounter > 319)
                    {
                        icounter = icounter - 319;
                        j++;
                    }

                    ASCII = ImageArray[icounter, j].R;//get the ascii
                    ch = (char)ASCII;//convert back to a char
                    strDecipher = strDecipher + ch;//read the message

                    icounter = icounter + key;
                }
                richtxtDecipher.Text = strDecipher;//output
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                richtxtDecipher.Clear();
            }
        }
    }
}
