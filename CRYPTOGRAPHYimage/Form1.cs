using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace CRYPTOGRAPHYimage
{
    public partial class Form1 : Form
    {
        static int RSA_P = 0;
        static int RSA_Q = 0;
        static int RSA_E = 0;
        static int n = 0;
       


        static string loadImage = "";
        public Form1()
        {
                       InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Disable_all();
        }
        public string Encrypt(string imageToEncrypt)
        {
            string hex = imageToEncrypt;
            char[] ar = hex.ToCharArray();
            String c = "";
            
            progressBar1.Maximum = ar.Length;
            for (int i = 0; i < ar.Length; i++)
            {
                Application.DoEvents();
                progressBar1.Value = i;
                if (c == "")
                {
                    c = c + CRYPTOGRAPHYimage.RSAalgorithm.BigMod(ar[i], RSA_E, n);
                   
                }
                else
                {
                    c = c + "-" + CRYPTOGRAPHYimage.RSAalgorithm.BigMod(ar[i], RSA_E, n);
                    
                }
            }
            return c;
        }
        

        
        private void Button3_Click(object sender, EventArgs e)
        {

            if (button3.Text == "Set Details")
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Enter Valid Detail For RSA", "ERROR");
                }

                else
                {
                    if (CRYPTOGRAPHYimage.libraryfile.IsPrime(Convert.ToInt16(textBox2.Text)))
                    {
                        RSA_P = Convert.ToInt16(textBox2.Text);
                    }
                    else
                    {
                        textBox2.Text = "";
                        MessageBox.Show("Enter Prime Number");
                        return;
                    }
                    if (CRYPTOGRAPHYimage.libraryfile.IsPrime(Convert.ToInt16(textBox3.Text)))
                    {
                        RSA_Q = Convert.ToInt16(textBox3.Text);
                    }
                    else
                    {
                        textBox3.Text = "";
                        MessageBox.Show("Enter Prime Number");
                        return;
                    }

                    RSA_E = Convert.ToInt16(textBox4.Text);

                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    button4.Enabled = true;
                    button3.Text = "ReSet Details";
                }
            }
            else
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                button4.Enabled = false;
                button3.Text = "Set Details";
            }
        }

        

       
      
        private void Button2_Click(object sender, EventArgs e)
        {
            loadImage = BitConverter.ToString(CRYPTOGRAPHYimage.libraryfile.ConvertImageToByte(pictureBox1.Image));
            MessageBox.Show("Image Load Successfully");
            pictureBox1.Visible = true;
            label5.Visible = false;
            groupBox4.Enabled = true;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog save1 = new SaveFileDialog
            {
                Filter = "TEXT|*.txt"
            };
            if (save1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = save1.FileName;
                button5.Enabled = true;
            }
            else
            {
                textBox5.Text = "";
                button5.Enabled = false;
            }
        }

       

        private void Disable_all()
        {
            button2.Enabled = false;
            groupBox4.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            
        }

        private void Enable_all()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            groupBox4.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

       
        private void Button5_Click_1(object sender, EventArgs e)
        {
            n = CRYPTOGRAPHYimage.RSAalgorithm.valueN(RSA_P, RSA_Q);
            button1.Enabled = false;
            Disable_all();
            String en = Encrypt(loadImage);
            File.WriteAllText(textBox5.Text, en);
            
            MessageBox.Show("Encryption Done");
            button1.Enabled = true;
            pictureBox1.Visible = false;
            label5.Visible = true;
            label5.Text = File.ReadAllText(textBox5.Text);
            pictureBox1.Image = Properties.Resources.blank;
            textBox1.Text = "";
            label9.Text = "File Name: ";
            label10.Text = "Image Resolution: ";
            label11.Text = "Image Size: ";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog
            {
                Filter = "JPG|*.JPG"
            };
            if (open1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open1.FileName;
                pictureBox1.Image = Image.FromFile(textBox1.Text);
                button2.Enabled = true;
                FileInfo fi = new FileInfo(textBox1.Text);

                label9.Text = "File Name: " + fi.Name;
                label10.Text = "Image Resolution: " + pictureBox1.Image.PhysicalDimension.Height + " X " + pictureBox1.Image.PhysicalDimension.Width;

                double imageMB = ((fi.Length / 1024f) / 1024f);

                label11.Text = "Image Size: " + imageMB.ToString(".##") + "MB";
            }
            else
            {
                textBox1.Text = "";
                label9.Text = "File Name: ";
                label10.Text = "Image Resolution: ";
                label11.Text = "Image Size: ";

                pictureBox1.Image = Properties.Resources.blank;
                button2.Enabled = false;

            }
        }
    }
}

