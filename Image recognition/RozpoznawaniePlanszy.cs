using System.Collections.Generic;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using static Emgu.CV.CvInvoke;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System;
using System.Collections;

namespace Warcaby {
    public class RozpoznawaniePlanszy {
        private const string PLIK_KONFIGURACYJNY = @"C:\Users\Piotr\Desktop\Ustawienia.dat";
        private const int PLIK_DLUGOSC = 30;
        private const int ILOSC_KANALOW = 3;
        private const int ELEMENT_STRUKTURALNY1 = 8;
        private const int ELEMENT_STRUKTURALNY2 = 13;
        private const int ELEMENT_STRUKTURALNY3 = 11;
        private const int ILOSC_POL_KOLOR = 32;
        private const int ILOSC_POL = 64;
        private const int POLA_WIERSZ_KOLOR = 4;
        private const int POLA_WIERSZ = 8;

        public bool PokazywanieObrazu { get; private set; } = false;
        private KolorHSV pionki_min = new KolorHSV();
        private KolorHSV pionki_max = new KolorHSV(255, 255, 255);
        private KolorHSV damki_min = new KolorHSV();
        private KolorHSV damki_max = new KolorHSV(255, 255, 255);
        private KolorHSV pionki_wrog_min = new KolorHSV();
        private KolorHSV pionki_wrog_max = new KolorHSV(255, 255, 255);
        private KolorHSV damki_wrog_min = new KolorHSV();
        private KolorHSV damki_wrog_max = new KolorHSV(255, 255, 255);
        private KolorHSV pola_min = new KolorHSV();
        private KolorHSV pola_max = new KolorHSV(255, 255, 255);
        private Capture Kamera = new Capture(1);
        private Pole[,] Plansza = null;

        public RozpoznawaniePlanszy() {
            WczytajUstawienia();
        }

        /// <summary>
        /// Rozpoznaje białe pola na warcabnicy.
        /// </summary>
        /// <returns>Ilość rozpoznanych pól.</returns>
        public int RozpoznajPola() {
            Mat obr = WczytajObraz();
            VectorOfVectorOfPoint kontury = new VectorOfVectorOfPoint();
            Mat wyciety = new Mat();
            AnalizujPlansze(obr, pola_min, pola_max, ref kontury, ref wyciety);

            if(kontury.Size != ILOSC_POL_KOLOR) {
                Plansza = null;
                return kontury.Size;
            }

            Point[][] punkty = kontury.ToArrayOfArray();
            Pole[] biale = new Pole[punkty.Length];
            int srednia_szer = 0;
            int srednia_wys = 0;
            Pole pole;
            Point[] pola = new Point[ILOSC_POL];

            //Oblicz srednie rozmiary pol bialych
            for(int i = 0; i < punkty.Length; i++) {
                pole = new Pole(punkty[i].Min(p => p.X), punkty[i].Max(p => p.X), punkty[i].Min(p => p.Y), punkty[i].Max(p => p.Y));
                biale[i] = pole;
                srednia_szer += pole.xmax - pole.xmin;
                srednia_wys += pole.ymax - pole.ymin;
            }

            srednia_szer /= punkty.Length;
            srednia_wys /= punkty.Length;

            //Wyznacz granice pol
            biale = biale.OrderBy(p => p.ymin).ToArray();
            Pole plansza_rozm = new Pole(biale.Min(p => p.xmin), biale.Max(p => p.xmax), biale.Min(p => p.ymin), biale.Max(p => p.ymax));
            Plansza = new Pole[POLA_WIERSZ, POLA_WIERSZ];
            int ixw = 0;
            int ixk = 0;

            for(int i = 0; i < biale.Length; i += 4) {
                Pole[] wiersz = new Pole[POLA_WIERSZ_KOLOR];
                for(int j = 0; j < POLA_WIERSZ_KOLOR; j++) wiersz[j] = biale[i + j];
                wiersz = wiersz.OrderBy(p => p.xmin).ToArray();

                if(wiersz[0].xmin > (plansza_rozm.xmin + srednia_szer)) {    //W wierszu najpierw czarne
                    Plansza[0, ixk] = new Pole(plansza_rozm.xmin, wiersz[0].xmin, wiersz[0].ymin, wiersz[0].ymax, KolorPola.czarne);

                    ixw = 1;
                    for(int j = 0; j < POLA_WIERSZ_KOLOR; j++) {
                        Plansza[ixw, ixk] = new Pole(wiersz[j], KolorPola.biale);
                        ixw++;
                        if(j != POLA_WIERSZ_KOLOR - 1) {
                            Plansza[ixw, ixk] = new Pole(wiersz[j].xmax, wiersz[j + 1].xmin, wiersz[j].ymin, wiersz[j].ymax, KolorPola.czarne);
                            ixw++;
                        }
                    }

                } else {    //W wierszu najpierw biale
                    ixw = 0;
                    for(int j = 0; j < POLA_WIERSZ_KOLOR; j++) {
                        Plansza[ixw, ixk] = new Pole(wiersz[j], KolorPola.biale);
                        ixw++;
                        if(j != POLA_WIERSZ_KOLOR - 1) {
                            Plansza[ixw, ixk] = new Pole(wiersz[j].xmax, wiersz[j + 1].xmin, wiersz[j].ymin, wiersz[j].ymax, KolorPola.czarne);
                            ixw++;
                        }
                    }

                    Plansza[7, ixk] = new Pole(wiersz[3].xmax, plansza_rozm.xmax, wiersz[3].ymin, wiersz[3].ymax, KolorPola.czarne);
                }

                ixk++;
            }

            return punkty.Length;
        }

        private class DanePionka {
            public KolorHSV min;
            public KolorHSV max;
            public TypObiektu typ;
            public MCvScalar kolor;
        }

        /// <summary>
        /// Rozpoznaje pionki na planszy. Przed wywołaniem należy wywołać funkcję RozpoznajPola.
        /// </summary>
        /// <returns>Zwraca pozycje pionków na warcabnicy. Jeśli nie wywołano funkcji RozpoznajPola lub zwróciła ona inną wartość niż 32, funkcja zwraca null.</returns>
        public Pionek[] RozpoznajPionki() {
            if(Plansza == null) return null;

            Mat obr = WczytajObraz();
            HashSet<Pionek> pionki = new HashSet<Pionek>();

            DanePionka[] dane = new DanePionka[] {
                new DanePionka() { min = pionki_min, max = pionki_max, typ = TypObiektu.Pionki },
                new DanePionka() { min = damki_min, max = damki_max, typ = TypObiektu.Damki },
                new DanePionka() { min = pionki_wrog_min, max = pionki_wrog_max, typ = TypObiektu.PionkiWrog },
                new DanePionka() { min = damki_wrog_min, max = damki_wrog_max, typ = TypObiektu.DamkiWrog }
            };

            for(int d = 0; d < dane.Length; d++) {
                VectorOfVectorOfPoint kontury = new VectorOfVectorOfPoint();
                Mat wyciety = new Mat();
                AnalizujPlansze(obr, dane[d].min, dane[d].max, ref kontury, ref wyciety);

                Point[][] punkty = kontury.ToArrayOfArray();
                Pole pole;
                Point srodek;
                Pole pp;
                bool znaleziono;

                for(int i = 0; i < punkty.Length; i++) {
                    pole = new Pole(punkty[i].Min(p => p.X), punkty[i].Max(p => p.X), punkty[i].Min(p => p.Y), punkty[i].Max(p => p.Y));
                    srodek = new Point(pole.xmin + (pole.xmax - pole.xmin) / 2, pole.ymin + (pole.ymax - pole.ymin) / 2);
                    znaleziono = false;

                    for(int x = 0; x < POLA_WIERSZ; x++) {
                        for(int y = 0; y < POLA_WIERSZ; y++) {
                            pp = Plansza[x, y];

                            if(pp.xmin < srodek.X && pp.xmax > srodek.X && pp.ymin < srodek.Y && pp.ymax > srodek.Y) {
                                pionki.Add(new Pionek() { x = x, y = y, typ = dane[d].typ });
                                znaleziono = true;
                                break;
                            }
                        }

                        if(znaleziono) break;
                    }
                }
            }

            return pionki.ToArray();
        }

        /// <summary>
        /// Wyświetla okno umożliwiające wybranie zakresu kolorów, używanego do rozpoznania określonych elementów na obrazie.
        /// </summary>
        public void Kalibruj(TypObiektu element) {
            wndKalibracja okno;

            KolorHSV min = new KolorHSV(0, 0, 0);
            KolorHSV max = new KolorHSV(255, 255, 255);
            string kom = "Ustaw zakres tak, aby na obrazie widoczne były tylko ";

            switch(element) {
                case TypObiektu.Pola:
                    min = pola_min;
                    max = pola_max;
                    kom += "białe pola.";
                    break;

                case TypObiektu.Pionki:
                    min = pionki_min;
                    max = pionki_max;
                    kom += "Twoje pionki.";
                    break;

                case TypObiektu.Damki:
                    min = damki_min;
                    max = damki_max;
                    kom += "Twoje damki.";
                    break;

                case TypObiektu.PionkiWrog:
                    min = pionki_wrog_min;
                    max = pionki_wrog_max;
                    kom += "pionki przeciwnika.";
                    break;

                case TypObiektu.DamkiWrog:
                    min = damki_wrog_min;
                    max = damki_wrog_max;
                    kom += "damki przeciwnika.";
                    break;
            }

            okno = new wndKalibracja(min.H, min.S, min.V, max.H, max.S, max.V, kom);

            okno.Show();
            const string tytul = "Kalibracja";
            NamedWindow(tytul);

            while(okno.Otwarte) {
                VectorOfVectorOfPoint kontury = new VectorOfVectorOfPoint();
                Mat wyciety = new Mat();
                AnalizujPlansze(WczytajObraz(), new KolorHSV(okno.Hmin, okno.Smin, okno.Vmin), new KolorHSV(okno.Hmax, okno.Smax, okno.Vmax), ref kontury, ref wyciety);

                Imshow(tytul, wyciety);
                Application.DoEvents();
            }

            DestroyWindow(tytul);

            if(okno.DialogResult == DialogResult.OK) {
                min = new KolorHSV(okno.Hmin, okno.Smin, okno.Vmin);
                max = new KolorHSV(okno.Hmax, okno.Smax, okno.Vmax);

                switch(element) {
                    case TypObiektu.Pola:
                        pola_min = min;
                        pola_max = max;
                        break;

                    case TypObiektu.Pionki:
                        pionki_min = min;
                        pionki_max = max;
                        break;

                    case TypObiektu.Damki:
                        damki_min = min;
                        damki_max = max;
                        break;

                    case TypObiektu.PionkiWrog:
                        pionki_wrog_min = min;
                        pionki_wrog_max = max;
                        break;

                    case TypObiektu.DamkiWrog:
                        damki_wrog_min = min;
                        damki_wrog_max = max;
                        break;
                }
                ZapiszUstawienia();
            }
        }

        public enum TypObiektu { Pionki, Damki, PionkiWrog, DamkiWrog, Pola };
        public class Pionek{

            public int x;
            public int y;
            public TypObiektu typ;

            public override bool Equals(object obj) {
                Pionek p = (Pionek)obj;
                return p.x == x && p.y == y;
            }

            public override int GetHashCode() {
                return (x << 16) | y;
            }

        }

        //return (x << 16) | y;

        bool pokaz = false;





        /// <summary>
        /// Wyświetla obraz z kamery z zaznaczonymi polami, granicami pól i pionkami.
        /// </summary>
        public void PokazObraz() {
            PokazywanieObrazu = true;
            const string tytul = "Warcaby";
            NamedWindow(tytul);
            MCvScalar kolor_granice = new MCvScalar(0, 0, 255);

            DanePionka[] dane = new DanePionka[] {
                new DanePionka() { min = pionki_min, max = pionki_max, kolor = new MCvScalar(255, 0, 0) },
                new DanePionka() { min = damki_min, max = damki_max, kolor = new MCvScalar(255, 128, 0) },
                new DanePionka() { min = pionki_wrog_min, max = pionki_wrog_max, kolor = new MCvScalar(0, 128, 255) },
                new DanePionka() { min = damki_wrog_min, max = damki_wrog_max, kolor = new MCvScalar(0, 255, 255) },
                new DanePionka() { min = pola_min, max = pola_max, kolor = new MCvScalar(0, 255, 0)}
            };

            while(PokazywanieObrazu) {
                Mat obr = WczytajObraz();
                Mat obr2 = obr.Clone();
                CvtColor(obr2, obr2, ColorConversion.Hsv2Bgr);

                for(int d = 0; d < dane.Length; d++) {
                    VectorOfVectorOfPoint kontury = new VectorOfVectorOfPoint();
                    Mat wyciety = new Mat();







                    if(d == 4) pokaz = true;

                    AnalizujPlansze(obr, dane[d].min, dane[d].max, ref kontury, ref wyciety);

                    pokaz = false;






                    for(int i = 0; i < kontury.Size; i++) {
                        DrawContours(obr2, kontury, i, dane[d].kolor, 5);
                    }
                }


                if(Plansza != null) {
                    Point[][] pkt = new Point[64][];
                    int i = 0;
                    Pole pp;

                    for(int x = 0; x < POLA_WIERSZ; x++) {
                        for(int y = 0; y < POLA_WIERSZ; y++) {
                            pp = Plansza[x, y];
                            pkt[i] = new Point[4];
                            pkt[i][0] = new Point(pp.xmin, pp.ymin);
                            pkt[i][1] = new Point(pp.xmax, pp.ymin);
                            pkt[i][2] = new Point(pp.xmax, pp.ymax);
                            pkt[i][3] = new Point(pp.xmin, pp.ymax);
                            i++;
                        }
                    }

                    VectorOfVectorOfPoint p2 = new VectorOfVectorOfPoint(pkt);
                    for(i = 0; i < 64; i++) {
                        DrawContours(obr2, p2, i, kolor_granice, 1);
                    }
                }

                Imshow(tytul, obr2);
                Application.DoEvents();
            }

            DestroyAllWindows();
        }

        /// <summary>
        /// Zamyka okno podglądu obrazu z kamery.
        /// </summary>
        public void UkryjObraz() {
            PokazywanieObrazu = false;
        }

        private void WczytajUstawienia() {
            if(!File.Exists(PLIK_KONFIGURACYJNY)) return;

            FileStream fs = new FileStream(PLIK_KONFIGURACYJNY, FileMode.Open);
            if(fs.Length != PLIK_DLUGOSC) throw new IOException("Plik konfiguracyjny ma nieprawidłową długość.");
            fs.Seek(0, SeekOrigin.Begin);
            byte[] b = new byte[ILOSC_KANALOW];


            fs.Read(b, 0, ILOSC_KANALOW);
            pionki_min = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            pionki_max = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            damki_min = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            damki_max = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            pionki_wrog_min = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            pionki_wrog_max = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            damki_wrog_min = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            damki_wrog_max = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            pola_min = new KolorHSV(b);

            fs.Read(b, 0, ILOSC_KANALOW);
            pola_max = new KolorHSV(b);

            fs.Close();
        }

        private void ZapiszUstawienia() {
            FileStream fs = new FileStream(PLIK_KONFIGURACYJNY, FileMode.Create);
            byte[] b;

            b = pionki_min.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = pionki_max.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = damki_min.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = damki_max.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = pionki_wrog_min.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = pionki_wrog_max.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = damki_wrog_min.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = damki_wrog_max.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = pola_min.DoBajtow();
            fs.Write(b, 0, b.Length);

            b = pola_max.DoBajtow();
            fs.Write(b, 0, b.Length);

            fs.Close();
        }

        private Mat WczytajObraz() {
            Mat obr = Kamera.QueryFrame();
            //Mat obr = Imread(@"C:\Users\Marcin\Desktop\w.png", LoadImageType.Color);
            Mat obrhsv = new Mat();
            CvtColor(obr, obrhsv, ColorConversion.Bgr2Hsv);
            return obrhsv;
        }





        private void AnalizujPlansze(Mat obraz, KolorHSV min, KolorHSV max, ref VectorOfVectorOfPoint kontury, ref Mat obrazWyciety) {

            //NamedWindow("Test progów");

            Mat obr = new Mat();
            List<Point> pola = new List<Point>();
            Mat zakres = new Mat();

            InRange(obraz, min.DoTablicy(), max.DoTablicy(), zakres);
            BitwiseAnd(obraz, obraz, obrazWyciety, zakres);

            CvtColor(obrazWyciety, obrazWyciety, ColorConversion.Hsv2Bgr);
            obr = obrazWyciety.Clone();
            CvtColor(obr, obr, ColorConversion.Bgr2Gray);
            Threshold(obr, obr, 0, 255, ThresholdType.Binary);





            //if (pokaz) Imshow("Oryginal", obr);

            Erode(obr, obr, GetStructuringElement(ElementShape.Rectangle, new Size(ELEMENT_STRUKTURALNY1, ELEMENT_STRUKTURALNY1), new Point(-1, -1)), new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            //if(pokaz) Imshow("Erozja 1", obr);


            Dilate(obr, obr, GetStructuringElement(ElementShape.Rectangle, new Size(ELEMENT_STRUKTURALNY2, ELEMENT_STRUKTURALNY2), new Point(-1, -1)), new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            //if(pokaz) Imshow("Dylacja", obr);

            Erode(obr, obr, GetStructuringElement(ElementShape.Rectangle, new Size(ELEMENT_STRUKTURALNY3, ELEMENT_STRUKTURALNY3), new Point(-1, -1)), new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
            //if(pokaz) Imshow("Erozja 2", obr);
            



            IOutputArray hierarchia = new Mat();
            FindContours(obr, kontury, hierarchia, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
        }
    }
}