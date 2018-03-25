using System;
using System.Windows.Forms;

namespace Warcaby {
    public partial class wndKalibracja : Form {
        public byte Hmin { get; private set; } = 0;
        public byte Smin { get; private set; } = 0;
        public byte Vmin { get; private set; } = 0;
        public byte Hmax { get; private set; } = 0;
        public byte Smax { get; private set; } = 0;
        public byte Vmax { get; private set; } = 0;
        public bool Otwarte { get; private set; } = false;

        public wndKalibracja() {
            InitializeComponent();
        }

        public wndKalibracja(byte h1, byte s1, byte v1, byte h2, byte s2, byte v2, string opis) {
            InitializeComponent();
            trbH1.Value = h1;
            trbS1.Value = s1;
            trbV1.Value = v1;
            trbH2.Value = h2;
            trbS2.Value = s2;
            trbV2.Value = v2;
            lblOpis.Text = opis;
        }

        private void trbH1_ValueChanged(object sender, EventArgs e) {
            lblH1.Text = trbH1.Value.ToString();
            Hmin = (byte)trbH1.Value;
        }

        private void trbS1_ValueChanged(object sender, EventArgs e) {
            lblS1.Text = trbS1.Value.ToString();
            Smin = (byte)trbS1.Value;
        }

        private void trbV1_ValueChanged(object sender, EventArgs e) {
            lblV1.Text = trbV1.Value.ToString();
            Vmin = (byte)trbV1.Value;
        }

        private void trbH2_ValueChanged(object sender, EventArgs e) {
            lblH2.Text = trbH2.Value.ToString();
            Hmax = (byte)trbH2.Value;
        }

        private void trbS2_ValueChanged(object sender, EventArgs e) {
            lblS2.Text = trbS2.Value.ToString();
            Smax = (byte)trbS2.Value;
        }

        private void trbV2_ValueChanged(object sender, EventArgs e) {
            lblV2.Text = trbV2.Value.ToString();
            Vmax = (byte)trbV2.Value;
        }

        private void wndKalibracja_Load(object sender, EventArgs e) {
            Otwarte = true;
        }

        private void wndKalibracja_FormClosed(object sender, FormClosedEventArgs e) {
            Otwarte = false;
        }

        private void btnDomyslne_Click(object sender, EventArgs e) {
            trbH1.Value = 0;
            trbS1.Value = 0;
            trbV1.Value = 0;
            trbH2.Value = 255;
            trbS2.Value = 255;
            trbV2.Value = 255;
        }

        private void btnAnuluj_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnZapisz_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}