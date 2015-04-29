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
using System.Threading;

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

            listBox1.Items.Clear();
            IList<Genre> genres = DataBase.GetGenres();
            foreach (Genre g in genres)
            {
                listBox1.Items.Add(g.Name);
            }
        }

        IList<Image> images = new List<Image>();
        int curImage = 0;
        int curMovie = -1;
        Movie movie;

        private void button1_Click(object sender, EventArgs e)
        {
            AddMovieForm movieForm = new AddMovieForm();
            if (movieForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int movieID = DataBase.InsertMovie(movieForm.Movie);
                ShowMovie(movieID);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int h = 0;
            foreach (Control c in panel5.Controls)
            {
                if (c is Label)
                {
                    c.MaximumSize = new Size(panel5.Width - 30, 0); 
                }
                h += c.Height + c.Padding.Top + c.Padding.Bottom;
            }
            panelSearchResults.Left = Width - panelSearchResults.Width;
            panelSearch.Left = Width - panelSearch.Width - 18;
            if (h > Height) vScrollBar2.Visible = true;
            else vScrollBar2.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelView.Visible = false;
            panelSearchResults.Left = Width - panelSearchResults.Width;
            panelSearch.Left = Width - panelSearch.Width - 18;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
            SearchByName();
        }

        void SearchByName()
        {
            Dictionary<int, string> searchResults = DataBase.SearchMovies(textBox1.Text);
            panelSearchResults.Visible = false;
            if (searchResults.Keys.Count > 0)
            {
                panelSearchResults.Visible = true;
                vScrollBar1.Visible = false;
                panel6.Height = 0;
                panel6.Controls.Clear();
                panel6.Top = 0;
                foreach (int i in searchResults.Keys)
                {
                    AddSearchResult(i, searchResults[i]);
                }
            }
            
        }

        void AddSearchResult(int movieID, string name)
        {
            int k = panelSearchResults.Controls.Count;

            Panel p = new Panel();
            p.Name = string.Format("searchResult{0}", k);
            p.Dock = DockStyle.Top;
            p.Height = 60;

            Panel pb = new Panel();
            pb.Dock = DockStyle.Right;
            pb.Width = 140;

            Button b = new Button();
            b.Left = 10;
            b.Top = 15;
            b.Height = 30;
            b.Width = 115;
            b.Text = "Перейти";
            b.Click += (sender, e) => { ShowMovie(movieID); };

            pb.Controls.Add(b);

            Label l = new Label();
            l.Padding = new Padding(10, 19, 10, 20);
            l.Dock = DockStyle.Fill;
            l.Text = name;

            p.Controls.Add(pb);
            p.Controls.Add(l);
            
            panel6.Top = 0;
            panel6.Height += 60;
            panel6.Controls.Add(p);

            if (panel6.Height > 240)
                vScrollBar1.Visible = true;
        }

        void ShowMovie(int MovieID)
        {
            panelSearchResults.Visible = false;
            foreach (Control c in panel5.Controls)
            {
                if (c is Label)
                {
                    c.MaximumSize = new Size(panel5.Width - 30, 0);
                }
            }
            panelView.Visible = true;
            curMovie = MovieID;
            
            movie = DataBase.LoadMovie(MovieID);
            labelName.Text = movie.Name;
            labelDescription.Text = movie.Description;
            labelYear.Text = "Год выпуска: " + movie.Year.ToString();
            labelAge.Text = "Возрастной рейтинг: " + movie.Age.ToString();
            labelStudio.Text = "Студия: " + movie.MovieStudio;
            labelGenre.Text = "Жанры: ";
            labelDirector.Text = "Режиссёры: ";
            labelActor.Text = "Актёры: \r\n";
            labelWriter.Text = "Сценаристы: ";

            foreach (Genre g in movie.Genres)
                labelGenre.Text += g.Name + ", ";
            labelGenre.Text = labelGenre.Text.Substring(0, labelGenre.Text.Length - 2);

            
            foreach (Role r in movie.Roles)
            {
                switch (r.Type)
                {
                    case RoleType.Actor :
                        labelActor.Text += string.Format("{0} {1} - {2},\r\n", r.Human.Name, r.Human.Surname,r.Character);
                        break;
                    case RoleType.Director:
                        labelDirector.Text += string.Format("{0} {1}, ", r.Human.Name, r.Human.Surname);
                        break;
                    case RoleType.Writer:
                        labelWriter.Text += string.Format("{0} {1}, ", r.Human.Name, r.Human.Surname);
                        break;
                }
            }
            labelActor.Text = labelActor.Text.Substring(0, labelActor.Text.Length - 3);
            labelDirector.Text = labelDirector.Text.Substring(0, labelDirector.Text.Length - 2);
            labelWriter.Text = labelWriter.Text.Substring(0, labelWriter.Text.Length - 2);

            images.Clear();
            foreach (Image img in movie.Images)
            {
                images.Add(img);
            }
            curImage = 0;

            Thread loadImageThread = new Thread(LoadPics);
            loadImageThread.IsBackground = true;
            loadImageThread.Start();         
        }

        void LoadPics()
        {
            if (curImage < images.Count)
                pictureBox1.Load(images[curImage].URL);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panel6.Top += e.OldValue - e.NewValue;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panelSearchResults.Visible = false;
            panelSearch.Visible = false;
            if (textBox1.Text.Length >= 3)
                SearchByName();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddMovieForm editMovie = new AddMovieForm(movie);
            if (editMovie.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataBase.UpdateMovie(curMovie, movie);
                ShowMovie(curMovie);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panelSearch.Visible)
            {
                panelSearch.Visible = false;
            }
            else
            {
                panelSearch.Visible = true;
                panelSearchResults.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelSearchResults.Visible = false;
            panelSearch.Visible = false;
            Search();
        }

        void Search()
        {
            Dictionary<int, string> searchResults = DataBase.SearchMovies(textBoxName.Text, 
                (int)numericYear1.Value, (int)numericYear2.Value, listBox1.SelectedIndex + 1, textBoxStudio.Text);

            panelSearchResults.Visible = false;
            if (searchResults.Keys.Count > 0)
            {
                panelSearchResults.Visible = true;
                vScrollBar1.Visible = false;
                panel6.Height = 0;
                panel6.Controls.Clear();
                panel6.Top = 0;
                foreach (int i in searchResults.Keys)
                {
                    AddSearchResult(i, searchResults[i]);
                }
            }

        }
    }
}
