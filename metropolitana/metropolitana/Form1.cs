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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            PictureBox picture = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(50, 50),
                Location = new Point(100, 100),
                Image = Image.FromFile("42534-metro-icon.png"),
            };
            this.Controls.Add(picture);
        }
    }
}
