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
        public static bool operator ==(Checkers_piece a, Checkers_piece b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            if (a != null && b != null)
            {
                if (a.Color == b.Color && a.Type == b.Type)
                { return true; }
                else { return false; }
            }
            else
            { return false; }
        }
        public static bool operator !=(Checkers_piece a, Checkers_piece b)
        { return !(a == b); }

        public bool Equals(Checkers_piece other)
        { return this == other; }
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
