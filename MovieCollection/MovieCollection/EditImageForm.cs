using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieCollection
{
    public partial class EditImageForm : Form
    {
        public EditImageForm()
        {
            InitializeComponent();
            Image = null;
        }

        public Image Image 
        {
            get 
            {
                if (textBox1.Text == "") return null;
                return new Image(textBox1.Text, textBox2.Text); 
            }
            set 
            {
                if (value == null)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    textBox1.Text = value.URL;
                    textBox2.Text = value.Description;
                }
            } 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
