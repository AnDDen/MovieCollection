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
    public partial class AddMovieForm : Form
    {
        public AddMovieForm()
        {
            InitializeComponent();
        }

        public Movie Movie { get; set; }

        private void AddMovieForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = DateTime.Now.Year;
            numericUpDown1.Maximum = DateTime.Now.Year + 10;
            Movie = new Movie("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Movie.Name = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Movie.Description = textBox2.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Movie.Year = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Movie.Age = (int)numericUpDown2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddGenreForm addGenre = new AddGenreForm();
            if (addGenre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox3.Text = "";
                Movie.Genres = new List<int>();

                foreach (int id in addGenre.MovieGenres.Keys)
                {
                    textBox3.Text += addGenre.MovieGenres[id] + "; ";
                    Movie.Genres.Add(id);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddRoleForm addDirector = new AddRoleForm();
            if (addDirector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (Human h in addDirector.Humans)
                    Movie.Roles.Add(h, RoleType.Director);
            }
        }
    }
}
