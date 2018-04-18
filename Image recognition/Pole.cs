namespace Warcaby {
    public class Pole {
        public int xmin = 0;
        public int xmax = 0;
        public int ymin = 0;
        public int ymax = 0;
        public KolorPola kolor;

        public Pole() { }

        public Pole(int x_min, int x_max, int y_min, int y_max) {
            xmin = x_min;
            xmax = x_max;
            ymin = y_min;
            ymax = y_max;
        }

        public Pole(int x_min, int x_max, int y_min, int y_max, KolorPola kolor_pola) : this(x_min, x_max, y_min, y_max) {
            kolor = kolor_pola;
        }

        public Pole(Pole p, KolorPola kolor_pola) : this(p.xmin, p.xmax, p.ymin, p.ymax) {
            kolor = kolor_pola;
        }
    }

    public enum KolorPola { biale, czarne };
}