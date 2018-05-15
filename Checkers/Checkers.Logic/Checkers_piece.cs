using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic

{
    public enum Type { Man, King };
    public enum Color { Black, White };
    public class Checkers_piece
    {
        private Type type;
        private Color color;
        public Checkers_piece(Color color_, Type type_)
        {
            type = type_;
            color = color_;
        }

        public Type Type { get => type; }
        public Color Color { get => color; }

        override public string ToString()
        {
            if (type == Type.Man && color == Color.Black)
            { return "0"; }
            else if (type == Type.Man && color == Color.White)
            { return "1"; }
            else if (type == Type.King && color == Color.Black)
            { return "2"; }
            else if (type == Type.King && color == Color.White)
            { return "3"; }
            else
            { return "X "; }
        }

    }
}
