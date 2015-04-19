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
    public partial class AddGenreForm : Form
    {
        public AddGenreForm()
        {
            InitializeComponent();

            MovieGenres = new List<Genre>();

            comboBox1.Items.Clear();
            genres = DataBase.GetGenres();
            foreach (Genre g in genres)
            {
                comboBox1.Items.Add(g.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        IList<Genre> genres;

        public IList<Genre> MovieGenres { get; set; }

        IList<Panel> genrePanels = new List<Panel>();

        void AddPanel(Genre g)
        {
            Panel p = new Panel();
            int k = genrePanels.Count;

            p.Name = string.Format("genrePanel{0}", g.GenreID);
            p.Left = 0;
            p.Top = 40 * k;
            p.Width = 325;
            p.Height = 40;

            Button b = new Button();
            b.Text = "Удалить";
            b.Width = 100;
            b.Height = 28;
            b.Left = 220;
            b.Top = 6;
            b.Click += (sender, e) => { Delete(g); };

            Label l = new Label();
            l.Text = g.Name;
            l.Width = 200;
            l.Top = 11;
            l.Left = 3;

            p.Controls.Add(l);
            p.Controls.Add(b);

            genrePanels.Add(p);

            foreach (Control c in panel.Controls)
            {
                if (!(c is ScrollBar))
                    c.Top += vScrollBar1.Value;
            }
            vScrollBar1.Value = 0;

            panel.Controls.Add(genrePanels[k]);

            if (40 * genrePanels.Count >= 140)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 40 * genrePanels.Count - 140 + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        void Delete(Genre g)
        {
            MovieGenres.Remove(g);
            Panel p = genrePanels.FirstOrDefault((x) => { return x.Name == string.Format("genrePanel{0}", g.GenreID); });
            int k = genrePanels.IndexOf(p);

            foreach (Control c in panel.Controls)
            {
                if (!(c is ScrollBar))
                    c.Top += vScrollBar1.Value;
            }
            vScrollBar1.Value = 0;

            for (int i = k + 1; i < genrePanels.Count; i++)
                genrePanels[i].Top -= 40;
            panel.Controls.Remove(p);
            genrePanels.Remove(p);

            if (40 * genrePanels.Count >= 140)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 40 * genrePanels.Count - 140 + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = comboBox1.SelectedItem.ToString();

            if (MovieGenres.FirstOrDefault((x) => { return x.Name == name; }) == null)
            {
                Genre g = genres.FirstOrDefault((x) => { return x.Name == name; });
                MovieGenres.Add(g);
                AddPanel(g);
            }
        }

        public void AddGenres(IList<Genre> genres)
        {
            MovieGenres = genres;
            foreach (Genre g in MovieGenres)
            {
                AddPanel(g);
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            foreach (Control c in panel.Controls)
            {
                if (!(c is ScrollBar))
                    c.Top += e.OldValue - e.NewValue;
            }
        }
    }
}
