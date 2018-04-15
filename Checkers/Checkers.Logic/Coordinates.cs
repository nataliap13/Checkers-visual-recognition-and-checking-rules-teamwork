using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic
{
    class Coordinates
    {
        private int _x;
        private int _y;
        public Coordinates(int x_, int y_)
        {
            _x = x_;
            _y = y_;
        }

        public int X { get => _x; }
        public int Y { get => _y; }
    }
}
