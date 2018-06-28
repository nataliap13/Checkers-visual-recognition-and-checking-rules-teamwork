using System;
using System.Drawing;
using System.Windows.Forms;
using static Warcaby.RozpoznawaniePlanszy;
using System.Diagnostics;
using System.Threading;
using Checkers.Logic;

namespace Warcaby
{
    public partial class Form1 : Form
    {
        RozpoznawaniePlanszy plansza = new RozpoznawaniePlanszy();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPokaz_Click(object sender, EventArgs e)
        {
            if (plansza.PokazywanieObrazu)
            {
                btnPokaz.Text = "Pokaż obraz";
                plansza.UkryjObraz();
            }
            else
            {
                btnPokaz.Text = "Ukryj obraz";
                plansza.PokazObraz();
            }
        }

        private void btnKalPionki_Click(object sender, EventArgs e)
        {
            plansza.Kalibruj(RozpoznawaniePlanszy.TypObiektu.Pionki);
        }

        private void btnKalPlansze_Click(object sender, EventArgs e)
        {
            plansza.Kalibruj(RozpoznawaniePlanszy.TypObiektu.Pola);
        }

        private wndPionki wnd = new wndPionki();

        private void PokazPlansze(Pionek[] p)
        {
            //wnd?.Close();
            //wnd = new wndPionki();

            Brush pionki = new SolidBrush(System.Drawing.Color.FromArgb(0, 0, 255));
            Brush damki = new SolidBrush(System.Drawing.Color.FromArgb(0, 128, 255));
            Brush pionki_wrog = new SolidBrush(System.Drawing.Color.FromArgb(255, 128, 0));
            Brush damki_wrog = new SolidBrush(System.Drawing.Color.FromArgb(0, 255, 0));

            //int ile = plansza.RozpoznajPola();
            //if (ile != 32) return;
            //RozpoznawaniePlanszy.Pionek[] p = plansza.RozpoznajPionki();

            Bitmap bm = new Bitmap(321, 321);
            Graphics gr = Graphics.FromImage(bm);
            gr.Clear(System.Drawing.Color.White);

            for (int i = 0; i <= 320; i += 40)
            {
                gr.DrawLine(Pens.Black, i, 0, i, 320);
                gr.DrawLine(Pens.Black, 0, i, 320, i);
            }

            for (int i = 0; i < p.Length; i++)
            {
                Brush br = new SolidBrush(System.Drawing.Color.Lime);

                switch (p[i].typ)
                {
                    case RozpoznawaniePlanszy.TypObiektu.Pionki:
                        br = pionki;
                        break;

                    case RozpoznawaniePlanszy.TypObiektu.Damki:
                        br = damki;
                        break;

                    case RozpoznawaniePlanszy.TypObiektu.PionkiWrog:
                        br = pionki_wrog;
                        break;

                    case RozpoznawaniePlanszy.TypObiektu.DamkiWrog:
                        br = damki_wrog;
                        break;
                }
                gr.FillEllipse(br, p[i].x * 40 + 2, p[i].y * 40 + 2, 36, 36);
            }
            wnd.pctPlansza.Image = bm;
            //wnd.Show();
        }

        private void btnRozpoznajPionki_Click(object sender, EventArgs e)
        {
            wndPionki wnd = new wndPionki();

            Brush pionki = new SolidBrush(System.Drawing.Color.FromArgb(0, 0, 255));
            Brush damki = new SolidBrush(System.Drawing.Color.FromArgb(0, 128, 255));
            Brush pionki_wrog = new SolidBrush(System.Drawing.Color.FromArgb(255, 128, 0));
            Brush damki_wrog = new SolidBrush(System.Drawing.Color.FromArgb(0, 255, 0));

            int ile = plansza.RozpoznajPola();
            if (ile != 32) return;
            RozpoznawaniePlanszy.Pionek[] p = plansza.RozpoznajPionki();

            Bitmap bm = new Bitmap(321, 321);
            Graphics gr = Graphics.FromImage(bm);
            gr.Clear(System.Drawing.Color.White);

            for (int i = 0; i <= 320; i += 40)
            {
                gr.DrawLine(Pens.Black, i, 0, i, 320);
                gr.DrawLine(Pens.Black, 0, i, 320, i);
            }

            for (int i = 0; i < p.Length; i++)
            {
                Brush br = new SolidBrush(System.Drawing.Color.Lime);

                switch (p[i].typ)
                {
                    case RozpoznawaniePlanszy.TypObiektu.Pionki:
                        br = pionki;
                        break;

                    case RozpoznawaniePlanszy.TypObiektu.Damki:
                        br = damki;
                        break;

                    case RozpoznawaniePlanszy.TypObiektu.PionkiWrog:
                        br = pionki_wrog;
                        break;

                    case RozpoznawaniePlanszy.TypObiektu.DamkiWrog:
                        br = damki_wrog;
                        break;
                }

                gr.FillEllipse(br, p[i].x * 40 + 2, p[i].y * 40 + 2, 36, 36);
            }

            wnd.pctPlansza.Image = bm;
            wnd.Show();
        }

        Process Client;
        Process Server;

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Początek
            int ile = plansza.RozpoznajPola();
            if (ile != 32) return;
            RozpoznawaniePlanszy.Pionek[] p = plansza.RozpoznajPionki();

            Client = CreateProcess("Test_Client.py");
            Server = CreateProcess("Test_Server.py");

            Checkers.Logic.Color kolor;
            Checkers.Logic.Type typ;

            Checkers_piece[,] pl = new Checkers_piece[8, 8];
            for (int i = 0; i < p.Length; i++)
            {
                KonwertujKolor(p[i].typ, out kolor, out typ);
                pl[p[i].y, p[i].x] = new Checkers_piece(kolor, typ);
            }

            Checkers.Logic.Draughts_checkers dc = new Draughts_checkers(8, 12);
            int bialy = dc.Generate_player_key(Checkers.Logic.Color.White);
            int czarny = dc.Generate_player_key(Checkers.Logic.Color.Black);

            //dc.Set_board(Checkers.Logic.Color.Black, pl);
            dc.Set_active_player(Checkers.Logic.Color.White);
            wnd.Show();


            while (true)
            {
                Thread.Sleep(300);
                Application.DoEvents();

                //Petla
                ile = plansza.RozpoznajPola();
                if (ile != 32) continue;
                p = plansza.RozpoznajPionki();
                Checkers.Logic.Color k = Checkers.Logic.Color.Black;
                Coordinates[] move = null;

                PokazPlansze(p);

                try
                {
                    pl = new Checkers_piece[8, 8];
                    for (int i = 0; i < p.Length; i++)
                    {
                        KonwertujKolor(p[i].typ, out kolor, out typ);
                        pl[p[i].y, p[i].x] = new Checkers_piece(kolor, typ);
                    }

                    move = Move_Detector.DetectMove(dc.Check_active_player(), dc, pl);
                    k = dc.Check_active_player();
                    Make_move_and_display_boards(ref dc, (k == Checkers.Logic.Color.Black ? czarny : bialy), new Coordinates(move[0].X, move[0].Y), new Coordinates(move[1].X, move[1].Y));
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    lblBlad.Text = ex.Message;
                    continue;
                }

                if (k == Checkers.Logic.Color.Black)
                {
                    Client.StandardInput.WriteLine(move[0].X);
                    Client.StandardInput.WriteLine(move[0].Y);
                    Client.StandardInput.WriteLine(move[1].X);
                    Client.StandardInput.WriteLine(move[1].Y);
                }
                else
                {
                    Server.StandardInput.WriteLine(move[0].X);
                    Server.StandardInput.WriteLine(move[0].Y);
                    Server.StandardInput.WriteLine(move[1].X);
                    Server.StandardInput.WriteLine(move[1].Y);
                }
            }
        }

        public static void Make_move_and_display_boards(ref Draughts_checkers game, int player_secret_key, Coordinates origin, Coordinates destination)
        {
            Console.WriteLine("\n" + origin.ToString() + " -> " + destination.ToString());
            game.Make_move(player_secret_key, origin, destination);
            //Display_board(game, game.Check_player_color(player_secret_key));
            //Display_board(game, game.Check_active_player());
        }

        private void KonwertujKolor(TypObiektu typ, out Checkers.Logic.Color kolor, out Checkers.Logic.Type kolor2)
        {

            kolor = Checkers.Logic.Color.White;
            kolor2 = Checkers.Logic.Type.Man;

            switch (typ)
            {
                case RozpoznawaniePlanszy.TypObiektu.Pionki:
                    kolor = Checkers.Logic.Color.Black;
                    kolor2 = Checkers.Logic.Type.Man;
                    break;

                case RozpoznawaniePlanszy.TypObiektu.Damki:
                    kolor = Checkers.Logic.Color.Black;
                    kolor2 = Checkers.Logic.Type.King;
                    break;

                case RozpoznawaniePlanszy.TypObiektu.PionkiWrog:
                    kolor = Checkers.Logic.Color.White;
                    kolor2 = Checkers.Logic.Type.Man;
                    break;

                case RozpoznawaniePlanszy.TypObiektu.DamkiWrog:
                    kolor = Checkers.Logic.Color.White;
                    kolor2 = Checkers.Logic.Type.King;
                    break;
            }
        }

        private void btnKalDamki_Click(object sender, EventArgs e)
        {
            plansza.Kalibruj(RozpoznawaniePlanszy.TypObiektu.Damki);
        }

        private void btnKalPionkiWrog_Click(object sender, EventArgs e)
        {
            plansza.Kalibruj(RozpoznawaniePlanszy.TypObiektu.PionkiWrog);
        }

        private void btnKalDamkiWrog_Click(object sender, EventArgs e)
        {
            plansza.Kalibruj(RozpoznawaniePlanszy.TypObiektu.DamkiWrog);
        }

        private static Process CreateProcess(string File)
        {
            Process p = new Process();
            p.StartInfo.FileName = "python";//Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\..\Local\Programs\Python\Python36\python.exe");    //@"C:\Users\Marcin\AppData\Local\Programs\Python\Python36\python.exe";
            p.StartInfo.Arguments = @"C:\Users\Piotr\Desktop\Display\Tests\" + File;//Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Display\Tests\" + File; //@"C:\Users\Marcin\Desktop\Display\Tests\" + File;

            //przekierowanie wejścia/wyjścia
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;

            //bez wyświetlania okna
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            return p;
        }
    }
}