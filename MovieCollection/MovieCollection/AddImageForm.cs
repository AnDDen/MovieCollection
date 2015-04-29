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
    public partial class AddImageForm : Form
    {
        public AddImageForm()
        {
            InitializeComponent();

            Images = new List<Image>();
        }

        public IList<Image> Images { get; set; }

        IList<Panel> panels = new List<Panel>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        public void AddPanel(Image img)
        {
            Panel p = new Panel();
            int k = panels.Count;

            p.Name = string.Format("panel_{0}", img.URL);
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
            b.Click += (sender, e) => { Delete(img); };

            Label l = new Label();
            l.Text = img.Description;
            l.Width = 200;
            l.Top = 11;
            l.Left = 100;
            l.Height = 78;
            l.TextAlign = ContentAlignment.MiddleCenter;

            PictureBox pb = new PictureBox();
            pb.Left = 3;
            pb.Top = 3;
            pb.Height = 94;
            pb.Width = 94;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            try
            {
                //pb.Image = System.Drawing.Image.FromFile(img.URL);
                pb.Load(img.URL);
            }
            catch (Exception)
            {
            }

            p.MouseEnter += (sender, e) => { ImgMouseEnter(img); };
            l.MouseEnter += (sender, e) => { ImgMouseEnter(img); };
            b.MouseEnter += (sender, e) => { ImgMouseEnter(img); };
            pb.MouseEnter += (sender, e) => { ImgMouseEnter(img); };

            p.MouseLeave += (sender, e) => { ImgMouseLeave(); };
            l.MouseLeave += (sender, e) => { ImgMouseLeave(); };
            b.MouseLeave += (sender, e) => { ImgMouseLeave(); };
            pb.MouseLeave += (sender, e) => { ImgMouseLeave(); };

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

            if (100 * panels.Count >= 260)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 100 * panels.Count - 260 + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        void ImgMouseEnter(Image img)
        {
            try
            {
                //pictureBox1.Image = System.Drawing.Image.FromFile(img.URL);
                pictureBox1.Load(img.URL);
            }
            catch (Exception) { }
            label3.Text = img.Description;
        }

        void ImgMouseLeave()
        {
            pictureBox1.Image = null;
            label3.Text = "";
        }

        void Delete(Image img)
        {
            Images.Remove(img);

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

            if (100 * panels.Count >= 260)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 100 * panels.Count - 260 + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Images.FirstOrDefault((x) => { return x.URL == textBox1.Text; }) == null)
            {
                Image img = new Image(textBox1.Text, textBox2.Text);
                Images.Add(img);
                AddPanel(img);
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
