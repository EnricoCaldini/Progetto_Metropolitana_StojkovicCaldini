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
        ComboBox box;
        List<Color> listaColori;
        int switchint = 0;
        object ogg;

        public Dettagli(Stazione staz)
        {
            InitializeComponent();
            ogg = staz;
            funzione("Nome Stazione: ");
        }

        public Dettagli(Link linchino)
        {
            InitializeComponent();
            ogg = linchino;
            switchint = 1;
            funzione("Distanza tra " + linchino.prtnz.nome + " e " + linchino.rrv.nome + ":");
        }

        public Dettagli(Linea lineetta, List<Color> listaColori)
        {
            InitializeComponent();
            this.listaColori = listaColori;
            ogg = lineetta;
            switchint = 2;
            Label a = new Label
            {
                Name = "labelA",
                Location = new Point(10, 30),
                Text = "Colore Linea: ",
                Font = new Font("Calibri", 11),
                Size = new Size(120, 20)
            };
            Controls.Add(a);
            box = new ComboBox
            {
                Name = "ComboBoxA",
                Location = new Point(120, 27),
                Font = new Font("Calibri", 11)
            };
            foreach (Color col in listaColori)
                box.Items.Add(col.Name);
            Controls.Add(box);
        }
        
        private void funzione(string nomeLabel)
        {
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
                Text = nomeLabel,
                Font = new Font("Calibri", 11),
                Size = new Size(120, 20)
            };
            Controls.Add(a);
        }

        private void btnConf_Click(object sender, EventArgs e)
        {
            if (txtbx.Text == "")
                MessageBox.Show("Compilare i campi");
            else
            {
                switch (switchint)
                {
                    case 0: ((Stazione)ogg).nome = txtbx.Text; break;
                    case 1:
                        try
                        {
                            ((Link)ogg).dstnz = Convert.ToInt32(txtbx.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Deve essere un intero");
                            return;
                        }
                        break;
                    case 2: ((Linea)ogg).c = listaColori[box.SelectedIndex];
                        listaColori.RemoveAt(box.SelectedIndex); break;
                }
                Close();
            }
        }
    }
}
