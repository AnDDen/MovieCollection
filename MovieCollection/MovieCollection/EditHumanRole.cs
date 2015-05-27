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
    public partial class EditHumanRole : Form
    {
        public EditHumanRole(RoleType roleType, Movie movie)
        {
            InitializeComponent();
            RoleType = roleType;
            Movie = movie;
        }

        public Movie Movie { get; set; }

        private RoleType roleType;

        public RoleType RoleType
        {
            get { return roleType; }
            set
            {
                roleType = value;
                if (roleType == RoleType.Actor)
                {
                    textBox3.Visible = true;
                    label3.Visible = true;
                }
                else 
                { 
                    textBox3.Visible = false;
                    label3.Visible = false;
                }
            }
        }

        public Role Role
        {
            get 
            { 
                if (RoleType != RoleType.Actor)
                    return new Role(new Human(textBox1.Text, textBox2.Text), RoleType, Movie); 
                else
                    return new Role(new Human(textBox1.Text, textBox2.Text), RoleType, Movie, textBox3.Text); 
            }
            set
            {
                textBox1.Text = value.Human.Name;
                textBox2.Text = value.Human.Surname;
                textBox3.Text = value.Character == null ? "" : value.Character;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool f = DataBase.HumanExist(Role.Human);
            if (!f)
                if (MessageBox.Show(
                    string.Format("{0} {1} не найден(а) в базе данных. Вы действительно хотитете добавить {0} {1}?", Role.Human.Name, Role.Human.Surname),
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
