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
    public partial class AddRoleForm : Form
    {
        public AddRoleForm(RoleType roleType)
        {
            InitializeComponent();

            if (roleType == RoleType.Actor)
            {
            }
        }

        IList<Panel> panels = new List<Panel>();

        public IList<Human> Humans = new List<Human>();

        void AddPanel(string name, string surname)
        {
            Panel p = new Panel();
            int k = panels.Count;

            p.Name = name + " " + surname;
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
            b.Click += (sender, e) => { Delete(name, surname); };

            Label l = new Label();
            l.Text = name + " " + surname;
            l.Width = 200;
            l.Top = 11;
            l.Left = 3;

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

            if (40 * panels.Count >= 140)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 40 * panels.Count - 140 + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        void Delete(string name, string surname)
        {
            Human h = Humans.FirstOrDefault((x) => { return x.Name == name && x.Surname == surname; });
            if (h != null)
            {
                Humans.Remove(h);

                Panel p = panels.FirstOrDefault((x) => { return x.Name == name + " " + surname; });
                int k = panels.IndexOf(p);

                foreach (Control c in panel.Controls)
                {
                    if (!(c is ScrollBar))
                        c.Top += vScrollBar1.Value;
                }
                vScrollBar1.Value = 0;

                for (int i = k + 1; i < panels.Count; i++)
                    panels[i].Top -= 40;
                panel.Controls.Remove(p);
                panels.Remove(p);

                if (40 * panels.Count >= 140)
                {
                    vScrollBar1.Visible = true;
                    vScrollBar1.Maximum = 40 * panels.Count - 140 + 20;
                    vScrollBar1.LargeChange = 10;
                }
                else
                    vScrollBar1.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string surname = textBox2.Text;

            if (Humans.FirstOrDefault((x) => { return x.Name == name && x.Surname == surname; }) == null)
            {
                Humans.Add(new Human(name, surname));
                AddPanel(name, surname);
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
