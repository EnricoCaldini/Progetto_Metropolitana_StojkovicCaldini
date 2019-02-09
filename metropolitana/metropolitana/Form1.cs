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
                picture.MouseDown += stazioneDownUp;
                picture.MouseMove += stazioneMove;
                picture.MouseUp += stazioneDownUp;
                panel1.Controls.Add(picture);
                picture.Tag = staz.Count;
                staz.Add(new Stazione(x + 25, y + 25));
                staz[staz.Count - 1].pic = picture;
                Dettagli det = new Dettagli(staz[staz.Count - 1]);
                det.ShowDialog();
                Label a = new Label
                {
                    Name = "labelA",
                    Location = new Point(x, y + 53),
                    Text = staz[staz.Count - 1].nome,
                    Font = new Font("Calibri", 8),
                    Size = new Size(50, 10),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel1.Controls.Add(a);
                staz[staz.Count - 1].lab = a;
            }
        }

        private void stazioneMove(object sender, MouseEventArgs e)
        {
            PictureBox a = (PictureBox)sender;
            if (staz[Convert.ToInt32(a.Tag)].selected && createStation)
            {
                int x = Cursor.Position.X - 38, y = Cursor.Position.Y - 140;
                ((PictureBox)staz[Convert.ToInt32(a.Tag)].pic).Location = new Point(x, y);
                staz[Convert.ToInt32(a.Tag)].x = x + 25;
                staz[Convert.ToInt32(a.Tag)].y = y + 25;
                ((Label)staz[Convert.ToInt32(a.Tag)].lab).Location = new Point(Cursor.Position.X - 38, Cursor.Position.Y - 87);
            }
        }

        private void stazioneDownUp(object sender, MouseEventArgs e)
        {
            PictureBox a = (PictureBox)sender;
            staz[Convert.ToInt32(a.Tag)].selected = !staz[Convert.ToInt32(a.Tag)].selected;
        }

        private void stazioneClick(object sender, MouseEventArgs e)
        {
            PictureBox pic2 = (PictureBox)sender;
            Stazione sel = staz[Convert.ToInt32(pic2.Tag)];
            if (createLink)
            {
                pic2.BackgroundImage = Image.FromFile("42534-metro-iconSel.png");
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
