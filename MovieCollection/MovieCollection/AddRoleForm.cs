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
        public AddRoleForm(RoleType type, Movie movie)
        {
            InitializeComponent();

            this.type = type;
            this.movie = movie;

            switch (type)
            {
                case RoleType.Actor:
                    Text = "Актёры";
                    break;
                case RoleType.Director:
                    Text = "Режиссёры";
                    break;
                case RoleType.Writer:
                    Text = "Сценаристы";
                    break;
            }

            Roles = new List<Role>();
        }

        IList<Panel> panels = new List<Panel>();
        
        private RoleType type;

        public Movie movie { get; set; }

        public IList<Role> Roles { get; set; }

        public void AddPanel(int n)
        {
            Panel p = new Panel();
            int k = panels.Count;

            Role r = Roles[n];

            p.Name = "Panel_" + r.Human.Name + "_" + r.Human.Surname + "_" + r.Character;
            p.Left = 0;
            p.Top = 60 * k;
            p.Width = 725;
            p.Height = 60;

            Button b = new Button();
            b.Text = "Удалить";
            b.Width = 100;
            b.Height = 28;
            b.Left = 620;
            b.Top = 16;
            b.Click += (sender, e) => { Delete(n); };

            Label l = new Label();
            l.Text = r.Human.Name + " " + r.Human.Surname;
            if (type == RoleType.Actor) l.Text += "\n\r" + r.Character;
            l.Width = 600;
            l.Top = 11;
            l.Left = 3;
            l.Height = 48;
            l.TextAlign = ContentAlignment.MiddleCenter;

            p.DoubleClick += (sender, e) => { Edit(n, p, l); };
            l.DoubleClick += (sender, e) => { Edit(n, p, l); };

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

            if (60 * panels.Count >= panel.Height)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 60 * panels.Count - panel.Height + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        void Delete(int n)
        {
            Role r = Roles[n];
            Roles.RemoveAt(n);

            Panel p = panels.FirstOrDefault((x) => { return x.Name == "Panel_" + r.Human.Name + "_" + r.Human.Surname + "_" + r.Character; });
            int k = panels.IndexOf(p);

            foreach (Control c in panel.Controls)
            {
                if (!(c is ScrollBar))
                    c.Top += vScrollBar1.Value;
            }
            vScrollBar1.Value = 0;

            for (int i = k + 1; i < panels.Count; i++)
                panels[i].Top -= 60;
            panel.Controls.Remove(p);
            panels.Remove(p);

            if (60 * panels.Count >= panel.Height)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 60 * panels.Count - panel.Height + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
            
        }

        void Add()
        {
            EditHumanRole editForm = new EditHumanRole(type, movie);
            if (editForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Role r = editForm.Role;

                if (Roles.FirstOrDefault((x) => { return x.Human.Name == r.Human.Name && x.Human.Surname == r.Human.Surname &&
                                            (x.Character == r.Character || type != RoleType.Actor); }) == null)
                {
                    Roles.Add(r);
                    AddPanel(Roles.Count - 1);
                }

            }
        }

        void Edit(int n, Panel p, Label l)
        {
            EditHumanRole editForm = new EditHumanRole(type, movie);
            editForm.Role = Roles[n];
            if (editForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Roles[n] = editForm.Role;

                l.Text = Roles[n].Human.Name + " " + Roles[n].Human.Surname;
                if (type == RoleType.Actor) l.Text += "\n\r" + Roles[n].Character;
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
