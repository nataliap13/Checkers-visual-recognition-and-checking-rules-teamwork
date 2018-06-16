using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic
{
    public class Coordinates
    {
        private int _x;
        private int _y;
        public Coordinates(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X { get => _x; }
        public int Y { get => _y; }
        public static bool operator == (Coordinates a, Coordinates b)
        {
            return ((a.X == b.X) && (a.Y == b.Y));
        }
        public static bool operator != (Coordinates a, Coordinates b)
        {
            return !(a == b);
        }
        override public string ToString()
        {
            string result = "(" + X +"," + Y + ")";
            return result;
        }
    }
}
