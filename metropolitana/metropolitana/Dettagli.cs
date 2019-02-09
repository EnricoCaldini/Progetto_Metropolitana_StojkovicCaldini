using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace metropolitana
{
    public partial class Dettagli : Form
    {
        TextBox txtbx;
        int switchint = 0;
        object ogg;

        public Dettagli(Stazione staz)
        {
            InitializeComponent();
            ogg = staz;
            txtbx = new TextBox
            {
                Location = new Point(120, 27),
                Font = new Font("Calibri", 11),
                Size = new Size(140, 20)
            };
            Controls.Add(txtbx);
            Label a = new Label
            {
                Name = "labelA",
                Location = new Point(10, 30),
                Text = "Nome Stazione: ",
                Font = new Font("Calibri", 11),
                Size = new Size(120, 20)
            };
            Controls.Add(a);
        }

        private void btnConf_Click(object sender, EventArgs e)
        {
            switch (switchint)
            {
                case 0:
                    if (txtbx.Text != "")
                        ((Stazione)ogg).nome = txtbx.Text;
                    else
                        MessageBox.Show("Inserire nome stazione");
                    break;
                default: break;
            }
            Close();
        }
    }
}
