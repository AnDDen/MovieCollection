using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MovieCollection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            if (!File.Exists("movies.sqlite"))
            {
                DataBase.Create();
            }

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddMovieForm movieForm = new AddMovieForm();
            if (movieForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataBase.InsertMovie(movieForm.Movie);
            }
        }
    }
}
