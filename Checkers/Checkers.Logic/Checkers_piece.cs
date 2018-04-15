using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.logic

{
    enum Piece { Black_man, White_man, Black_king, White_king };
    class Checkers_piece
    {
        private Piece type;
        public Checkers_piece(Piece type_)
        { type = type_; }

        public Piece Type { get => type; }

        override public string ToString()
        {
            switch (type)
            {
                case Piece.Black_king:
                    { return ((int)type).ToString(); }
                case Piece.Black_man:
                    { return ((int)type).ToString(); }
                case Piece.White_king:
                    { return ((int)type).ToString(); }
                case Piece.White_man:
                    { return ((int)type).ToString(); }
                default:
                    { return "X "; }
            }
        }

    }
}
