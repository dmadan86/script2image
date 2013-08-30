using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace script2image
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            ConvertToImage(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Javascript files (*.js)|*.js|All files (*.*)|*.*";
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        void ConvertToImage(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            long length = fileInfo.Length;
            var width = Math.Ceiling(Math.Sqrt(length));
            var height = width;

            Bitmap bitmap = new Bitmap(Convert.ToInt32(width), Convert.ToInt32(height));
            
            StreamReader sr = File.OpenText(path);
            string sourceData = sr.ReadToEnd();
            sr.Close();
            
            for (int y = 0, index = 0, asciiValue = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    asciiValue = 32;
                    if (index < sourceData.Length-1 )
                    {
                        asciiValue = (int) sourceData[index];
                        if (asciiValue < 0 || asciiValue > 255)
                            asciiValue = 32;
                    }
                    bitmap.SetPixel(x, y, Color.FromArgb(asciiValue, asciiValue, asciiValue));
                    index++;
                }
            }
            bitmap.Save(fileInfo.Directory + "//imageScript.png", ImageFormat.Png);
            bitmap.Dispose();
            MessageBox.Show("Successfully Converted script to Image");
        }
    }
}
