using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRYPTOGRAPHYimage
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

             
               private void Button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Form3 f1 = new Form3();
                f1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Choose one of the ALGORITHM");
            }

        }

        
    }
}
