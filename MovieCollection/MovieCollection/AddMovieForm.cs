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
            AddGenreForm addGenre = new AddGenreForm(); ;
            if (Movie.Genres.Count != 0)
                addGenre.AddGenres(Movie.Genres);
            if (addGenre.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox3.Text = "";
                //Movie.Genres.Clear();
                Movie.Genres = addGenre.MovieGenres;

                foreach (Genre g in addGenre.MovieGenres)
                {
                    textBox3.Text += g.Name + "; ";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddRoleForm addDirector = new AddRoleForm(RoleType.Director, Movie);
            IList<Role> old = new List<Role>();
            if (Movie.Roles.Any((x) => { return x.Type == RoleType.Director; }))
            {
                Role r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Director; });
                while (r != null)
                {
                    addDirector.Roles.Add(r);
                    old.Add(r);
                    addDirector.AddPanel(r);
                    Movie.Roles.Remove(r);
                    r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Director; });
                }
            }
            switch (addDirector.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK :
                    textBox4.Text = "";
                    foreach (Role role in addDirector.Roles)
                    {
                        Movie.Roles.Add(role);
                        textBox4.Text += string.Format("{0} {1};\r\n", role.Human.Name, role.Human.Surname);
                    }
                    break;

                case System.Windows.Forms.DialogResult.Cancel :
                    foreach (Role role in old)
                        Movie.Roles.Add(role);
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddRoleForm addWriter = new AddRoleForm(RoleType.Writer, Movie);
            IList<Role> old = new List<Role>();                       
            if (Movie.Roles.Any((x) => { return x.Type == RoleType.Writer; }))
            {
                Role r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Writer; });
                while (r != null)
                {
                    addWriter.Roles.Add(r);
                    old.Add(r);
                    addWriter.AddPanel(r);
                    Movie.Roles.Remove(r);
                    r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Writer; });
                }
            }
            switch (addWriter.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    textBox5.Text = "";
                    foreach (Role role in addWriter.Roles)
                    {
                        Movie.Roles.Add(role);
                        textBox5.Text += string.Format("{0} {1};\r\n", role.Human.Name, role.Human.Surname);
                    }
                    break;

                case System.Windows.Forms.DialogResult.Cancel:
                    foreach (Role role in old)
                        Movie.Roles.Add(role);
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddRoleForm addActor = new AddRoleForm(RoleType.Actor, Movie);
            IList<Role> old = new List<Role>();
            if (Movie.Roles.Any((x) => { return x.Type == RoleType.Actor; }))
            {
                Role r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Actor; });
                while (r != null)
                {
                    addActor.Roles.Add(r);
                    old.Add(r);
                    addActor.AddPanel(r);
                    Movie.Roles.Remove(r);
                    r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Actor; });
                }
            }
            switch (addActor.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    textBox6.Text = "";

                    foreach (Role role in addActor.Roles)
                    {
                        Movie.Roles.Add(role);
                        textBox6.Text += string.Format("{0} {1} - {2};\r\n", role.Human.Name, role.Human.Surname, role.Character);
                    }
                    break;

                case System.Windows.Forms.DialogResult.Cancel:
                    foreach (Role role in old)
                        Movie.Roles.Add(role);
                    break;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Movie.Link = textBox7.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddImageForm imgForm = new AddImageForm();
            if (Movie.Images.Count != 0)
            {
                foreach (Image img in Movie.Images)
                {
                    imgForm.Images.Add(img);
                    imgForm.AddPanel(img);
                }

            }
            if (imgForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Movie.Images = imgForm.Images;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Movie.MovieStudio = textBox8.Text;
        }
    }
}
