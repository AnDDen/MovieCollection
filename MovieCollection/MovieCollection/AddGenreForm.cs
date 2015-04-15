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

            MovieGenres = new Dictionary<int, string>();
        }

        Dictionary<int, string> genres;

        //public string GenreName { get; set; }
        //public int GenreID { get; set; }

        public Dictionary<int, string> MovieGenres { get; set; }

        IList<Panel> genrePanels = new List<Panel>();

        void AddPanel(int genreID)
        {
            Panel p = new Panel();
            int k = genrePanels.Count;

            p.Name = string.Format("genrePanel{0}", genreID);
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
            b.Click += (sender, e) => { Delete(genreID); };

            Label l = new Label();
            l.Text = MovieGenres[genreID];
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

        void Delete(int genreID)
        {
            MovieGenres.Remove(genreID);
            Panel p = genrePanels.FirstOrDefault((x) => { return x.Name == string.Format("genrePanel{0}", genreID); });
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

        private void AddGenreForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            genres = DataBase.GetGenres();
            foreach (int id in genres.Keys)
            {
                comboBox1.Items.Add(genres[id]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = comboBox1.SelectedItem.ToString();

            if (!MovieGenres.ContainsValue(name))
            {
                int id = genres.Keys.FirstOrDefault((x) => { return genres[x] == name; });
                MovieGenres.Add(id, name);
                AddPanel(id);
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
