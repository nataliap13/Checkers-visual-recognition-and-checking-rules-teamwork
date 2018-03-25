using Emgu.CV;
using Emgu.CV.Structure;
using System;

namespace Warcaby {
    public class KolorHSV {
        public byte H = 0;
        public byte S = 0;
        public byte V = 0;

        public KolorHSV() { }

        public KolorHSV(byte h, byte s, byte v) {
            H = h;
            S = s;
            V = v;
        }

        public KolorHSV(byte[] b) {
            if(b == null) throw new ArgumentException("Nie podano tablicy.");
            if(b.Length != 3) throw new ArgumentException("Tablica musi mieć 3 elementy.");
            H = b[0];
            S = b[1];
            V = b[2];
        }

        public ScalarArray DoTablicy() {
            return new ScalarArray(new MCvScalar(H, S, V));
        }

        public byte[] DoBajtow() {
            byte[] b = new byte[3];
            b[0] = H;
            b[1] = S;
            b[2] = V;
            return b;
        }
    }
}