using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRYPTOGRAPHYimage
{
    public partial class Form4 : Form
    {
        static int d = 0;
        static int n = 0;
        static string loadcipher = "";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            disable_all();
        }
        public string decryption(String decryptimage)
        {
            char[] ar = decryptimage.ToCharArray();
            int i = 0, j = 0;
            string c = "", dc = "";
            progressBar2.Maximum = ar.Length;
            try
            {
                for (; i < ar.Length; i++)
                {
                    Application.DoEvents();
                    c = "";
                    progressBar2.Value = i;
                    for (j = i; ar[j] != '-'; j++)
                        c = c + ar[j];
                    i = j;

                    int xx = Convert.ToInt16(c);
                    dc = dc + ((char)CRYPTOGRAPHYimage.RSAalgorithm.BigMod(xx, d, n)).ToString();
                }
            }
            catch (Exception ex) { }

            return dc;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "Set Details")
            {
                if (textBox9.Text == "" || textBox8.Text == "")
                {
                    MessageBox.Show("Enter Valid Detail For RSA", "ERROR");
                }
                else
                {
                    if (Convert.ToInt16(textBox9.Text) > 0)
                    {
                        d = Convert.ToInt16(textBox9.Text);
                    }
                    else
                    {
                        textBox9.Text = "";
                        MessageBox.Show("Enter Valid Number");
                        return;
                    }
                    if (Convert.ToInt16(textBox8.Text) > 0)
                    {
                        n = Convert.ToInt16(textBox8.Text);
                    }
                    else
                    {
                        textBox8.Text = "";
                        MessageBox.Show("Enter Valid Number");
                        return;
                    }

                    textBox9.Enabled = false;
                    textBox8.Enabled = false;
                    button8.Text = "ReSet Details";
                    button7.Enabled = true;

                }
            }
            else
            {
                textBox9.Text = "";
                textBox8.Text = "";
                textBox9.Enabled = true;
                textBox8.Enabled = true;
                button8.Text = "Set Details";
                button7.Enabled = false;
            }


        }
        private void disable_all()
        {
           

            button9.Enabled = false;
            groupBox5.Enabled = false;
            button7.Enabled = false;
            button6.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {    
            OpenFileDialog open1 = new OpenFileDialog();
            open1.Filter = "TEXT|*.txt";
            
            if (open1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Properties.Resources.blank;
                textBox7.Text = open1.FileName;
                button9.Enabled = true;
            }
            else
            {
                textBox7.Text = "";
                button9.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog save1 = new SaveFileDialog();
            save1.Filter = "JPG|*.JPG";
            if (save1.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = save1.FileName;
                button6.Enabled = true;
                
            }
            else
            {
                textBox6.Text = "";
                button6.Enabled = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            loadcipher = File.ReadAllText(textBox7.Text);
            pictureBox1.Image = Properties.Resources.blank;
            pictureBox1.Visible = false;
            label1.Visible = true;
            MessageBox.Show("Load Cipher Successfully");
            label1.Text = File.ReadAllText(textBox7.Text);
            groupBox5.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            pictureBox1.Visible = true;
            pictureBox1.Image = Properties.Resources.blank;
            button10.Enabled = false;
            disable_all();
            String de = decryption(loadcipher);
            pictureBox1.Image = CRYPTOGRAPHYimage.libraryfile.ConvertByteToImage(CRYPTOGRAPHYimage.libraryfile.formhex(de));
            MessageBox.Show("Process Over");
            pictureBox1.Image.Save(textBox6.Text, System.Drawing.Imaging.ImageFormat.Jpeg);
            button10.Enabled = true;
            FileInfo fi = new FileInfo(textBox6.Text);

            label9.Text = "File Name: " + fi.Name;
            label10.Text = "Image Resolution: " + pictureBox1.Image.PhysicalDimension.Height + " X " + pictureBox1.Image.PhysicalDimension.Width;

            double imageMB = ((fi.Length / 1024f) / 1024f);

            label11.Text = "Image Size: " + imageMB.ToString(".##") + "MB";
        }
    }
}
