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
    public partial class SearchResultsForm : Form
    {
        public SearchResultsForm(Dictionary<int, string> searchResults, Form1 parentForm)
        {
            InitializeComponent();

            this.parentForm = parentForm;

            if (searchResults.Keys.Count > 0)
            {
                vScrollBar1.Visible = false;
                panel6.Height = 0;
                panel6.Controls.Clear();
                panel6.Top = 0;

                foreach (int i in searchResults.Keys)
                {
                    AddSearchResult(i, searchResults[i]);
                }
            }
            else
            {
                Panel p = new Panel();
                p.Name = string.Format("NoResultsPanel");
                p.Dock = DockStyle.Top;
                p.Height = 60;

                Label l = new Label();
                l.Padding = new Padding(10, 19, 10, 20);
                l.Dock = DockStyle.Fill;
                l.Text = "Поиск не дал результатов";

                p.Controls.Add(l);

                panel6.Top = 0;
                panel6.Height = 60;
                panel6.Controls.Add(p);

                vScrollBar1.Visible = false;
            }
        }

        private Form1 parentForm;

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
            b.Click += (sender, e) => 
            { 
                parentForm.ShowMovieByID(movieID);
                Close();
            };

            pb.Controls.Add(b);

            Label l = new Label();
            l.Padding = new Padding(10, 19, 10, 20);
            l.Dock = DockStyle.Fill;
            l.Text = name;
            l.Click += (sender, e) =>
            {
                parentForm.ShowMovieByID(movieID);
                Close();
            };

            p.Controls.Add(pb);
            p.Controls.Add(l);

            panel6.Top = 0;
            panel6.Height += 60;
            panel6.Controls.Add(p);

            if (panel6.Height > 240)
                vScrollBar1.Visible = true;
            else
                vScrollBar1.Visible = false;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            panel6.Top += e.OldValue - e.NewValue;
        }
    }
}
