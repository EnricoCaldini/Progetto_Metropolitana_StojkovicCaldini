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
        bool createStation = false;
        bool createLink = false;
        List<Stazione> staz = new List<Stazione>();
        Stazione uno = null;
        PictureBox pic;

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            int x = Cursor.Position.X - 38;
            int y = Cursor.Position.Y - 140;
            if (createStation)
            {
                PictureBox picture = new PictureBox
                {
                    Name = "pictureBox",
                    Size = new Size(50, 50),
                    Location = new Point(x, y),
                    BackgroundImage = Image.FromFile("42534-metro-icon.png"),
                    BackgroundImageLayout = ImageLayout.Zoom,
                };
                picture.MouseClick += stazioneClick;
                panel1.Controls.Add(picture);
                picture.Tag = staz.Count;
                staz.Add(new Stazione(x + 25, y + 25));
            }
        }

        private void stazioneClick(object sender, MouseEventArgs e)
        {
            PictureBox pic2 = (PictureBox)sender;
            Stazione sel = staz[Convert.ToInt32(pic2.Tag)];
            pic2.BackgroundImage = Image.FromFile("42534-metro-iconSel.png");
            if (createLink)
            {
                if (uno == null)
                {
                    uno = sel;
                    pic = pic2;
                }
                else
                {
                    Graphics gs = panel1.CreateGraphics();
                    Pen pn = new Pen(Color.Cyan, 3);
                    gs.DrawLine(pn, uno.x, uno.y, sel.x, sel.y);
                    uno = null;
                    pic.BackgroundImage = Image.FromFile("42534-metro-icon.png");
                    pic2.BackgroundImage = Image.FromFile("42534-metro-icon.png");
                }
            }
            else if(!createStation)
            {
                VisualStazione vs = new VisualStazione(sel);
                vs.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (createStation)
                button1.BackColor = Color.White;
            else
            {
                button1.BackColor = Color.DarkGray;
                button2.BackColor = Color.White;
                uno = null;
                createLink = false;
            }
            createStation = !createStation;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (createLink)
            {
                button2.BackColor = Color.White;
                uno = null;
            }
            else
            {
                button2.BackColor = Color.DarkGray;
                button1.BackColor = Color.White;
                createStation = false;
            }
            createLink = !createLink;
        }
    }
}
