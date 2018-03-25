using System;
using System.Drawing;
using System.Windows.Forms;

namespace Warcaby {
    public partial class Form1 : Form {
        RozpoznawaniePlanszy plansza = new RozpoznawaniePlanszy();
        public Form1() {
            InitializeComponent();
        }

        private void btnPokaz_Click(object sender, EventArgs e) {
            if(plansza.PokazywanieObrazu) {
                btnPokaz.Text = "Pokaż obraz";
                plansza.UkryjObraz();
            } else {
                btnPokaz.Text = "Ukryj obraz";
                plansza.PokazObraz();
            }
        }

        private void btnKalPionki_Click(object sender, EventArgs e) {
            plansza.Kalibruj(RozpoznawaniePlanszy.TypObiektu.Pionki);
        }

        private void btnKalPlansze_Click(object sender, EventArgs e) {
            plansza.Kalibruj(RozpoznawaniePlanszy.TypObiektu.Pola);
        }

        private void btnRozpoznajPionki_Click(object sender, EventArgs e) {
            wndPionki wnd = new wndPionki();

            int ile = plansza.RozpoznajPola();
            if(ile != 32) return;
            Point[] p = plansza.RozpoznajPionki();

            Bitmap bm = new Bitmap(321, 321);
            Graphics gr = Graphics.FromImage(bm);
            gr.Clear(Color.White);

            for(int i = 0; i <= 320; i+= 40) {
                gr.DrawLine(Pens.Black, i, 0, i, 320);
                gr.DrawLine(Pens.Black, 0, i, 320, i);
            }

            for(int i = 0; i < p.Length; i++) {
                gr.FillEllipse(Brushes.Red, p[i].X * 40 + 2, p[i].Y * 40 + 2, 36, 36);
            }

            wnd.pctPlansza.Image = bm;
            wnd.Show();
        }
    }
}