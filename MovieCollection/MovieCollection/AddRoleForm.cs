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

            label3.Visible = false;
            textBox3.Visible = false;

            switch (type)
            {
                case RoleType.Actor:
                    Text = "Актёры";
                    label3.Visible = true;
                    textBox3.Visible = true;
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

        //public IList<Human> Humans = new List<Human>();

        public IList<Role> Roles { get; set; }

        public void AddPanel(Role r)
        {
            Panel p = new Panel();
            int k = panels.Count;

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
            b.Click += (sender, e) => { Delete(r); };

            Label l = new Label();
            l.Text = r.Human.Name + " " + r.Human.Surname;
            if (type == RoleType.Actor) l.Text += "\n\r" + r.Character;
            l.Width = 600;
            l.Top = 11;
            l.Left = 3;
            l.Height = 48;
            l.TextAlign = ContentAlignment.MiddleCenter;

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

            if (60 * panels.Count >= 140)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 60 * panels.Count - 140 + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
        }

        void Delete(Role r)
        {
            Roles.Remove(r);

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

            if (60 * panels.Count >= 140)
            {
                vScrollBar1.Visible = true;
                vScrollBar1.Maximum = 60 * panels.Count - 140 + 20;
                vScrollBar1.LargeChange = 10;
            }
            else
                vScrollBar1.Visible = false;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string surname = textBox2.Text;
            string character = textBox3.Text;

            if (Roles.FirstOrDefault((x) => { return x.Human.Name == name && x.Human.Surname == surname && 
                                                (x.Character == character || type != RoleType.Actor); }) == null)
            {
                Human h = new Human(name, surname);
                //Humans.Add(h);
                Role r = new Role(h, type, movie);
                Roles.Add(r);
                if (type == RoleType.Actor)
                {
                    r.Character = character;
                    AddPanel(r);
                }
                else AddPanel(r);
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
