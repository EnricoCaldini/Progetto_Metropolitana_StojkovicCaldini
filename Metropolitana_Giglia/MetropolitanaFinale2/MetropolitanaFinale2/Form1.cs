using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MetropolitanaFinale2
{
    public partial class Form1 : Form
    {
        MySqlConnection Connessione;
        bool createStation = false;
        bool createLink = false;
        List<Stazione> staz = new List<Stazione>();
        List<Linea> linee = new List<Linea>(); 
        Stazione uno = null;
        PictureBox pic;
        List<Color> colori = new List<Color> { Color.Black, Color.Gray, Color.Red, Color.Blue, Color.Cyan, Color.Brown, Color.Pink, Color.Purple, Color.Orange, Color.Green, Color.DarkSeaGreen, Color.LightGoldenrodYellow, Color.YellowGreen, Color.Violet, Color.Navy };

        public Form1()
        {
            InitializeComponent();
            Connessione = new MySqlConnection("Server=127.0.0.1;Port=3306;user=root;Database=metro;Password=root;charset=utf8;");
            Connessione.Open();
            caricaMappe();
            if (comboBoxMappe.Items.Count > 0)
            {
                comboBoxMappe.SelectedIndex = 0;
                disegnaStazioni();
            }
        }

        private void disegnaStazioni()
        {
            MySqlCommand comando = new MySqlCommand("SELECT Nome, CONCAT(x, ''), CONCAT(y, '') FROM stazione WHERE idMappa = " + comboBoxMappe.SelectedItem, Connessione);
            MySqlDataAdapter adapt = new MySqlDataAdapter(comando);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            foreach (DataRow a in dt.Rows)
            {
                staz.Add(new Stazione(Convert.ToInt32(a[1]), Convert.ToInt32(a[2])));
                staz[staz.Count - 1].nome = a[0].ToString();
                DrawStation(staz[staz.Count - 1].x, staz[staz.Count - 1].y, staz[staz.Count - 1].nome);
            }
            comando = new MySqlCommand("SELECT Colore FROM linea WHERE idMappa = " + comboBoxMappe.SelectedItem, Connessione);
            adapt = new MySqlDataAdapter(comando);
            dt.Clear();
            adapt.Fill(dt);
            foreach(DataRow a in dt.Rows)
                for(int i=0; i<colori.Count; i++)
                    if((colori[i].ToString().Substring(7, colori[i].ToString().Length - 8) == (string)a[0]))
                    {
                        linee.Add(new Linea(colori[i]));
                        comboBoxLinea.Items.Add(colori[i].ToString().Substring(7, colori[i].ToString().Length - 8));
                        colori.RemoveAt(i);
                    }
            //qui deve leggere i link
        }

        private void DrawStation(int x, int y, string nome)
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
            Label a = new Label
            {
                Name = "labelA",
                Location = new Point(x, y + 53),
                Text = nome,
                Font = new Font("Calibri", 8),
                Size = new Size(50, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panel1.Controls.Add(a);
            staz[staz.Count - 1].lab = a;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            int x = Cursor.Position.X - 38;
            int y = Cursor.Position.Y - 140;
            if (createStation)
            {
                DrawStation(x, y, "nome");
                Dettagli det = new Dettagli(staz[staz.Count - 1]);
                det.ShowDialog();
                ((Label)staz[staz.Count - 1].lab).Text = staz[staz.Count - 1].nome;
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
            Stazione InQuestione = staz[Convert.ToInt32(a.Tag)];
            InQuestione.selected = !InQuestione.selected;
            Graphics gs = panel1.CreateGraphics();
            if (comboBoxLinea.SelectedIndex != -1)
            {
                Pen pn = new Pen(linee[comboBoxLinea.SelectedIndex].c, 3);
                if (InQuestione.selected)
                {
                    pn.Color = Color.Beige;
                    for(int i = 0; i < InQuestione.linx.Count; i++)
                    {
                        gs.DrawLine(pn, InQuestione.x, InQuestione.y, InQuestione.listaPunti[i].X, InQuestione.listaPunti[i].Y);
                        gs.DrawLine(pn, InQuestione.linx[i].rrv.x, InQuestione.linx[i].rrv.y, InQuestione.listaPunti[i].X, InQuestione.listaPunti[i].Y);
                    }
                    InQuestione.listaPunti.Clear();
                    for(int i=0; i<staz.Count; i++)
                    {
                        for(int j=0; j<staz[i].linx.Count; j++)
                        {
                            if (staz[i].linx[j].rrv.Equals(InQuestione))
                            {
                                int appo = staz[i].linx[j].dstnz;
                                staz[i].linx.RemoveAt(j);
                                staz[i].listaPunti.RemoveAt(j);
                                staz[i].linx.Add(new Link(staz[i], InQuestione, appo));
                            }
                        }
                    }
                }
                else
                {
                    foreach (Link zelda in InQuestione.linx)
                        calcolaLink(InQuestione, zelda.rrv);
                }
            }
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
                    if (uno.nome != sel.nome)
                    {
                        Link lino = new Link(uno, sel, 10);
                        Dettagli det = new Dettagli(lino);
                        det.ShowDialog();
                        pic.BackgroundImage = Image.FromFile("42534-metro-icon.png");
                        pic2.BackgroundImage = Image.FromFile("42534-metro-icon.png");
                        uno.linx.Add(lino);
                        sel.linx.Add(new Link(sel, uno, lino.dstnz));
                        uno = null;
                    }
                    else
                        MessageBox.Show("Non puoi collegare una stazione a sè stessa");
                }
            }
            else if (!createStation)
            {
                VisualStazione vs = new VisualStazione(sel);
                vs.ShowDialog();
            }
        }

        private void calcolaLink(Stazione uno, Stazione sel)
        {
            Graphics gs = panel1.CreateGraphics();
            Pen pn = new Pen(linee[comboBoxLinea.SelectedIndex].c, 3);
            //gs.DrawLine(pn, uno.x, uno.y, sel.x, sel.y);
            retta[] funz1 = new retta[4];
            retta[] funz2 = new retta[4];
            Point[] incontro = new Point[16];
            for (int i = -1; i <= 1; i++)
            {
                funz1[i + 1] = new retta(i, uno.y - i * uno.x);
                funz2[i + 1] = new retta(i, sel.y - i * sel.x);
            }
            funz1[3] = new retta(uno.x);
            funz2[3] = new retta(sel.x);
            int a, b, k = 0;
            int[] posI = new int[16];
            int[] posJ = new int[16];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (funz1[i].parallelaY)
                    {
                        incontro[k] = new Point(funz1[i].ValoreX, funz2[j].calcola(funz1[i].ValoreX));
                        posI[k] = i;
                        posJ[k++] = j;
                    }
                    else if (funz2[j].parallelaY)
                    {
                        incontro[k] = new Point(funz2[j].ValoreX, funz1[i].calcola(funz2[j].ValoreX));
                        posI[k] = i;
                        posJ[k++] = j;
                    }
                    else
                    {
                        for (int x = 0; x < 1357; x++)
                        {
                            a = funz1[i].calcola(x);
                            b = funz2[j].calcola(x);
                            if (a - 1 < b && a + 1 > b)
                            {
                                incontro[k] = new Point(x, b);
                                posI[k] = i;
                                posJ[k++] = i;
                                x = 1400;
                            }
                            else if (x == 1356)
                                k++;
                        }
                    }
                }
            }
            List<int> distanze = new List<int>();
            for (int i = 0; i < 16; i++)
                if (incontro[i] != null)
                    distanze.Add(calcolaDistanza(incontro[i], uno) + calcolaDistanza(incontro[i], sel));
            Point scelta = incontro[posMin(distanze)];
            uno.listaPunti.Add(scelta);
            for(int i=0; i<staz.Count; i++)
                if(staz[i].Equals(sel))
                    staz[i].listaPunti.Add(scelta);
            //gs.DrawEllipse(pn, scelta.X, scelta.Y, 10, 10);
            gs.DrawLine(pn, uno.x, uno.y, scelta.X, scelta.Y);
            gs.DrawLine(pn, sel.x, sel.y, scelta.X, scelta.Y);
        }

        private int calcolaDistanza(Point a, Stazione b)
        {
            return (int)(Math.Sqrt(Math.Pow(a.X - b.x, 2) + Math.Pow(a.Y - b.y, 2)));
        }

        private int posMin(List<int> lista)
        {
            int min = lista[0];
            int pos = 0;
            for(int i=1; i<lista.Count; i++)
                if(lista[i]< lista[pos])
                {
                    min = lista[i];
                    pos = i;
                }
            return pos;
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
                comboBoxLinea.Visible = false;
            }
            createStation = !createStation;
            annullaVerde();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBoxLinea.Visible = !comboBoxLinea.Visible;
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
                foreach (Linea lin in linee)
                    comboBoxLinea.Items.Add(lin.c.ToString().Substring(7, lin.c.ToString().Length-8));
                comboBoxLinea.Text = comboBoxLinea.Items[0].ToString();
            }
            createLink = !createLink;
            if (!createLink)
                annullaVerde();
        }

        private void annullaVerde()
        {
            foreach (Stazione stz in staz)
                ((PictureBox)stz.pic).BackgroundImage = Image.FromFile("42534-metro-icon.png");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Linea lin = new Linea(Color.Black);
            Dettagli ddiasndwadn = new Dettagli(lin, colori);
            ddiasndwadn.ShowDialog();
            linee.Add(lin);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //qui deve salvare tutto tipo
            Connessione.Close();
        }

        private void caricaMappe()
        {
            MySqlCommand comando = new MySqlCommand("SELECT Nome FROM mappa", Connessione);
            MySqlDataAdapter adapt = new MySqlDataAdapter(comando);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            foreach (DataRow a in dt.Rows)
                comboBoxMappe.Items.Add(a[0]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dettagli det = new Dettagli(Connessione);
            det.ShowDialog();
            comboBoxMappe.Items.Clear();
            caricaMappe();
            comboBoxMappe.SelectedIndex = comboBoxMappe.Items.Count - 1;
            staz.Clear();
            linee.Clear();
            colori = new List<Color> { Color.Black, Color.Gray, Color.Red, Color.Blue, Color.Cyan, Color.Brown, Color.Pink, Color.Purple, Color.Orange, Color.Green, Color.DarkSeaGreen, Color.LightGoldenrodYellow, Color.YellowGreen, Color.Violet, Color.Navy };
            comboBoxLinea.Items.Clear();
            disegnaStazioni();
        }

        private void comboBoxMappe_SelectedIndexChanged(object sender, EventArgs e)
        {
            disegnaStazioni();
        }
    }
}