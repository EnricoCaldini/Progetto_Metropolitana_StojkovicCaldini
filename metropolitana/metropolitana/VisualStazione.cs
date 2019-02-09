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
    public partial class VisualStazione : Form
    {
        public VisualStazione(Stazione s)
        {
            InitializeComponent();
            labelX.Text += s.x;
            labelY.Text += s.y;
            labelNome.Text += s.nome;
        }
    }
}
