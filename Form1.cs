using ColorThiefDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ImageWriter
{
    
    public partial class Form1 : Form
    {
        private string colorHex;
        public Form1()
        {
            InitializeComponent();
        }

        private void ImageBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
                ImageBox.Image = new Bitmap(open.FileName);
            
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (ImageBox.Image == null)
                MessageBox.Show("Select An Image to write your message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                var colorThief = new ColorThief();
                colorHex = colorThief.GetColor(ImageBox.Image as Bitmap).Color.ToHexString() ;
                DrawNewImage();
                MessageBox.Show($"text : {Message.Text}", "Succss", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }


        private void DrawNewImage()
        {
            var bitmap = new Bitmap(ImageBox.Image);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Arial", 10))
                {
                    PointF cords = new PointF(10f,10f);
                  
                    graphics.DrawString(Message.Text,arialFont, GetTextColor(),cords);
                }
                imageWithTextBox.Image = bitmap;
            }
            
        }

        private Brush GetTextColor()
        {
            
            ColorConverter conv = new ColorConverter();
            return new SolidBrush((System.Drawing.Color)conv.ConvertFromString(colorHex));
        }

       
    }

  
}
