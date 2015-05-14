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

            labelImage.BackColor = Color.FromArgb(150, 0, 0, 0);
            labelImage.Dock = DockStyle.Bottom;
            labelImage.Parent = pictureBox1;

            loadImageThread = new Thread(LoadPics);

            pictures = new Dictionary<string, System.Drawing.Image>();
        }

        SearchForm searchForm = new SearchForm();

        IList<Image> images = new List<Image>();

        Dictionary<string, System.Drawing.Image> pictures;

        int curImage = 0;
        int curMovie = -1;
        Movie movie;

        Thread loadImageThread;

        private void button1_Click(object sender, EventArgs e)
        {
            AddMovieForm movieForm = new AddMovieForm(pictures);
            if (movieForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int movieID = DataBase.InsertMovie(movieForm.Movie);
                ShowMovieByID(movieID);
            }
        }

        void AdjustPanelSize()
        {
            int h = 0;
            foreach (Control c in panelMovieInfo.Controls)
            {
                if (c is Label)
                {
                    c.MaximumSize = new Size(panelMovieInfo.Width - 30, 0); 
                }
                h += c.Height + c.Padding.Top + c.Padding.Bottom;
            }

            int scroll = 1;
            if (h > Height)
            {
                vScrollBar2.Visible = true;
                int dh = h - Height;
                vScrollBar2.Minimum = Height;
                vScrollBar2.Maximum = h + 10;
                vScrollBar2.SmallChange = dh / 10;
                vScrollBar2.LargeChange = dh;
            }
            else
            { 
                vScrollBar2.Visible = false; 
                scroll = 0;
                panelMovieInfo.Top = 0;
            }

            panelMovieInfo.Left = panel4.Left + panel4.Width;
            panelMovieInfo.Width = panelView.Width - panelMovieInfo.Left - scroll * vScrollBar2.Width;
            panelMovieInfo.Height = panelView.Height;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustPanelSize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelView.Visible = true;

            curMovie = DataBase.LatestID();
            ShowMovieByID(curMovie);

            AdjustPanelSize();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchByName();
        }

        void SearchByName()
        {
            Dictionary<int, string> searchResults = DataBase.SearchMovies(textBox1.Text);

            SearchResultsForm search = new SearchResultsForm(searchResults, this);
            search.ShowDialog();            
        }

        

        void ShowMovie(Movie movie)
        {
            this.movie = movie;
            labelName.Text = movie.Name;
            labelDescription.Text = movie.Description;
            labelYear.Text = "Год выпуска: " + movie.Year.ToString();
            labelAge.Text = "Возрастной рейтинг: " + movie.Age.ToString();
            labelStudio.Text = "Студия: " + movie.MovieStudio;
            labelGenre.Text = "Жанры: ";
            labelDirector.Text = "Режиссёры: ";
            labelActor.Text = "Актёры: \r\n";
            labelWriter.Text = "Сценаристы: ";
            if (loadImageThread.IsAlive) loadImageThread.Abort();

            foreach (Genre g in movie.Genres)
                labelGenre.Text += g.Name + ", ";
            labelGenre.Text = labelGenre.Text.Substring(0, labelGenre.Text.Length - 2);


            foreach (Role r in movie.Roles)
            {
                switch (r.Type)
                {
                    case RoleType.Actor:
                        labelActor.Text += string.Format("{0} {1} - {2},\r\n", r.Human.Name, r.Human.Surname, r.Character);
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

            /*prevImgBtn.Hide();
            if (curImage >= images.Count - 1) nextImgBtn.Hide();

            if (images[curImage].URL != "")
                labelImage.Text = images[curImage].Description;

            if (pictures.ContainsKey(images[curImage].URL))
                pictureBox1.Image = pictures[images[curImage].URL];
            else
            {
                loadImageThread = new Thread(LoadPics);
                loadImageThread.IsBackground = true;
                loadImageThread.Start();
            } */

            LoadImage();
        }

        void LoadImage()
        {
            if (loadImageThread.IsAlive) loadImageThread.Abort();

            if (images[curImage].URL != "")
                labelImage.Text = images[curImage].Description;

            if (pictures.ContainsKey(images[curImage].URL))
                pictureBox1.Image = pictures[images[curImage].URL];
            else
            {
                loadImageThread = new Thread(LoadPics);
                loadImageThread.IsBackground = true;
                loadImageThread.Start();
            }

            if (curImage == 0) prevImgBtn.Hide(); else prevImgBtn.Show();
            if (curImage >= images.Count - 1) nextImgBtn.Hide(); else nextImgBtn.Show();
        }

        void LoadPics()
        {
            if (curImage < images.Count)
            {
                pictureBox1.Load(images[curImage].URL);
                pictures.Add(images[curImage].URL, pictureBox1.Image);
            }
        }

        public void ShowMovieByID(int MovieID)
        {
            panelView.Visible = true;
            curMovie = MovieID;
            
            ShowMovie(DataBase.LoadMovie(MovieID));

            AdjustPanelSize();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // if (textBox1.Text.Length >= 3)
           //     SearchByName();            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddMovieForm editMovie = new AddMovieForm(movie, pictures);
            if (editMovie.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataBase.UpdateMovie(curMovie, movie);
                ShowMovie(movie);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (searchForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Dictionary<int, string> searchResults = DataBase.SearchMovies(searchForm.MovieName,
                searchForm.Year1, searchForm.Year2, searchForm.GenreID, searchForm.Studio);

                SearchResultsForm search = new SearchResultsForm(searchResults, this);
                search.ShowDialog(); 
            }
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            int d = e.NewValue - e.OldValue;
            panelMovieInfo.Top = panelMovieInfo.Top - d;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (images[curImage].Description != "")
                labelImage.Show();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            labelImage.Hide();
        }

        private void prevImgBtn_Click(object sender, EventArgs e)
        {
            if (curImage <= 0 || curImage >= images.Count)
                return;

            curImage--;

            LoadImage();
        }

        private void nextImgBtn_Click(object sender, EventArgs e)
        {
            if (curImage < 0 || curImage >= images.Count)
                return;

            curImage++;

            LoadImage();
        }
    }
}
