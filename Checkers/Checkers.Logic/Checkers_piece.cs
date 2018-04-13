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
        {
            type = type_;
        }

        public Piece get_type()
        { return type; }

    }
}
