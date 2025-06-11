using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kargo_Takip_Sistemi
{
    public partial class Form1: Form
    {
       
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Yurtiçi");
            comboBox1.Items.Add("Yurtdışı");
            comboBox1.SelectedIndex = 0; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Yurtiçi")
            {
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }

        }
    }
}
