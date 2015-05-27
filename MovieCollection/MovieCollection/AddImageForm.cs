using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MovieCollection
{
    public partial class AddImageForm : Form
    {
        public AddImageForm(Dictionary<string, System.Drawing.Image> pictures)
        {
            InitializeComponent();

            Images = new List<Image>();

            this.pictures = pictures;
        }

        public IList<Image> Images { get; set; }

        IList<Panel> panels = new List<Panel>();

        Dictionary<string, System.Drawing.Image> pictures;

        public void AddPanel(int n)
        {
            Panel p = new Panel();
            int k = panels.Count;

            p.Name = string.Format("panel_{0}", Images[n].URL);
            p.Left = 0;
            p.Top = 100 * k;
            p.Width = 425;
            p.Height = 100;

            Button b = new Button();
            b.Text = "Удалить";
            b.Width = 100;
            b.Height = 28;
            b.Left = 320;
            b.Top = 36;
            b.Click += (sender, e) => { Delete(n); };

            Label l = new Label();
            l.Name = "Description";
            l.Text = Images[n].Description;
            l.Width = 200;
            l.Top = 11;
            l.Left = 100;
            l.Height = 78;
            l.TextAlign = ContentAlignment.MiddleCenter;

            PictureBox pb = new PictureBox();
            pb.Name = "Picture";
            pb.Left = 3;
            pb.Top = 3;
            pb.Height = 94;
            pb.Width = 94;
            pb.SizeMode = PictureBoxSizeMode.Zoom;

            LoadImg(pb, Images[n]);

            p.MouseClick += (sender, e) => { ImgSelect(n); };
            l.MouseClick += (sender, e) => { ImgSelect(n); };
            pb.MouseClick += (sender, e) => { ImgSelect(n); };

            p.DoubleClick += (sender, e) => { Edit(n, p, l, pb); };
            l.DoubleClick += (sender, e) => { Edit(n, p, l, pb); };
            pb.DoubleClick += (sender, e) => { Edit(n, p, l, pb); };

            p.Controls.Add(pb);
            p.Controls.Add(l);
            p.Controls.Add(b);

            panels.Add(p);

            foreach (Control c in panel.Controls)
            {
                if (!(c is ScrollBar))
                    c.Top += vScrollBar1.Value;
            }
            vScrollBar1.Value = 0;

            panel.Controls.Add(panels[k]);

            if (100 * panels.Count >= panel.Height)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 100 * panels.Count - panel.Height + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        void LoadImg(PictureBox pb, Image img)
        {
            if (pictures.ContainsKey(img.URL))
                pb.Image = pictures[img.URL];
            else
            {
                pb.Load(img.URL);
                pictures[img.URL] = pb.Image;
            }
        }

        void ImgSelect(int n)
        {
            LoadImg(pictureBox1, Images[n]);
            label3.Text = Images[n].Description;
        }

        void Delete(int n)
        {
            Image img = Images[n];
            Images.RemoveAt(n);

            Panel p = panels.FirstOrDefault((x) => { return x.Name == "panel_" + img.URL; });
            int k = panels.IndexOf(p);

            foreach (Control c in panel.Controls)
            {
                if (!(c is ScrollBar))
                    c.Top += vScrollBar1.Value;
            }
            vScrollBar1.Value = 0;

            for (int i = k + 1; i < panels.Count; i++)
                panels[i].Top -= 100;
            panel.Controls.Remove(p);
            panels.Remove(p);

            if (100 * panels.Count >= panel.Height)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 100 * panels.Count - panel.Height + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        void Edit(int n, Panel p, Label l, PictureBox pb)
        {
            EditImageForm imgForm = new EditImageForm();
            imgForm.Image = Images[n];
            if (imgForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Images[n] = imgForm.Image;

                l.Text = Images[n].Description;
                LoadImg(pb, Images[n]);
            }
        }

        void Add()
        {
            EditImageForm imgForm = new EditImageForm();
            if (imgForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (imgForm.Image != null)
                {
                    Image img = imgForm.Image;
                    if (Images.FirstOrDefault((x) => { return x.URL == img.URL; }) == null)
                    {
                        Images.Add(img);
                        int n = Images.Count - 1;
                        AddPanel(n);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add();
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
