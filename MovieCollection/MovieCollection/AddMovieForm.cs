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
        public AddMovieForm(Dictionary<string, System.Drawing.Image> pictures)
        {
            InitializeComponent();
            Movie = new Movie("");
            numericUpDown1.Value = DateTime.Now.Year;
            numericUpDown1.Maximum = DateTime.Now.Year + 10;
            this.pictures = pictures;
        }

        Dictionary<string, System.Drawing.Image> pictures;

        public AddMovieForm(Movie movie, Dictionary<string, System.Drawing.Image> pictures)
        {
            InitializeComponent();
            Movie = movie;

            this.pictures = pictures;

            textBox1.Text = Movie.Name;
            textBox2.Text = Movie.Description;
            numericUpDown1.Value = Movie.Year;
            numericUpDown2.Value = Movie.Age;

            textBox3.Text = "";

            foreach (Genre g in Movie.Genres)
            {
                textBox3.Text += g.Name + "; ";
            }

            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            foreach (Role role in Movie.Roles)
            {
                switch (role.Type)
                {
                    case RoleType.Director:
                        textBox4.Text += string.Format("{0} {1};\r\n", role.Human.Name, role.Human.Surname);
                        break;
                    case RoleType.Writer:
                        textBox5.Text += string.Format("{0} {1};\r\n", role.Human.Name, role.Human.Surname);
                        break;
                    case RoleType.Actor:
                        textBox6.Text += string.Format("{0} {1} - {2};\r\n", role.Human.Name, role.Human.Surname, role.Character);
                        break;
                }
                
            }

            textBox7.Text = Movie.Link;
            textBox8.Text = Movie.MovieStudio;
        }

        public Movie Movie { get; set; }

        private void AddMovieForm_Load(object sender, EventArgs e)
        {

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
            if (Movie.Roles.Any((x) => { return x.Type == RoleType.Director; }))
            {
                if (Movie.Images.Count != 0)
                {
                    addDirector.Roles.Clear();
                    int k = 0;
                    for (int n = 0; n < Movie.Roles.Count; n++)
                    {
                        if (Movie.Roles[n].Type == RoleType.Director)
                        {
                            addDirector.Roles.Add(Movie.Roles[n]);
                            addDirector.AddPanel(k++);
                        }
                    }
                }
            }
            if (addDirector.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                textBox4.Text = "";

                Role r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Director; });
                while (r != null)
                {
                    Movie.Roles.Remove(r);
                    r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Director; });
                }

                foreach (Role role in addDirector.Roles)
                {
                    Movie.Roles.Add(role);
                    textBox4.Text += string.Format("{0} {1};\r\n", role.Human.Name, role.Human.Surname);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddRoleForm addWriter = new AddRoleForm(RoleType.Writer, Movie);                     
            if (Movie.Roles.Any((x) => { return x.Type == RoleType.Writer; }))
            {
                if (Movie.Images.Count != 0)
                {
                    addWriter.Roles.Clear();
                    int k = 0;
                    for (int n = 0; n < Movie.Roles.Count; n++)
                    {
                        if (Movie.Roles[n].Type == RoleType.Writer)
                        {
                            addWriter.Roles.Add(Movie.Roles[n]);
                            addWriter.AddPanel(k++);
                        }
                    }
                }
            }
            if (addWriter.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox5.Text = "";

                Role r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Writer; });
                while (r != null)
                {
                    Movie.Roles.Remove(r);
                    r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Writer; });
                }

                foreach (Role role in addWriter.Roles)
                {
                    Movie.Roles.Add(role);
                    textBox5.Text += string.Format("{0} {1};\r\n", role.Human.Name, role.Human.Surname);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddRoleForm addActor = new AddRoleForm(RoleType.Actor, Movie);
            if (Movie.Roles.Any((x) => { return x.Type == RoleType.Actor; }))
            {
                if (Movie.Images.Count != 0)
                {
                    addActor.Roles.Clear();
                    int k = 0;
                    for (int n = 0; n < Movie.Roles.Count; n++)
                    {
                        if (Movie.Roles[n].Type == RoleType.Actor)
                        {
                            addActor.Roles.Add(Movie.Roles[n]);
                            addActor.AddPanel(k++);
                        }
                    }
                }
            }
            if (addActor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox6.Text = "";

                Role r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Actor; });
                while (r != null)
                {
                    Movie.Roles.Remove(r);
                    r = Movie.Roles.FirstOrDefault((x) => { return x.Type == RoleType.Actor; });
                }

                foreach (Role role in addActor.Roles)
                {
                    Movie.Roles.Add(role);
                    textBox6.Text += string.Format("{0} {1} - {2};\r\n", role.Human.Name, role.Human.Surname, role.Character);
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Movie.Link = textBox7.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddImageForm imgForm = new AddImageForm(pictures);
            if (Movie.Images.Count != 0)
            {
                imgForm.Images.Clear();
                for (int n = 0; n < Movie.Images.Count; n++)
                {
                    imgForm.Images.Add(Movie.Images[n]);
                    imgForm.AddPanel(n);
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
