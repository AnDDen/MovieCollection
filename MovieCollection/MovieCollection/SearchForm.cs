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
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();

            listBox1.Items.Clear();
            IList<Genre> genres = DataBase.GetGenres();
            foreach (Genre g in genres)
            {
                listBox1.Items.Add(g.Name);
            }
        }

        public string MovieName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        public int Year1
        {
            get { return (int)numericYear1.Value; }
            set { numericYear1.Value = value; }
        }

        public int Year2
        {
            get { return (int)numericYear2.Value; }
            set { numericYear2.Value = value; }
        }

        public string Studio
        {
            get { return textBoxStudio.Text; }
            set { textBoxStudio.Text = value; }
        }

        public int GenreID
        {
            get { return listBox1.SelectedIndex + 1; }
            set { listBox1.SelectedIndex = value - 1; }
        }
    }
}
