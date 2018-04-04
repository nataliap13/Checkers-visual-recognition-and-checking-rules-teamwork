using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.logic

{
    class Program
    {
        static void Main(string[] args)
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            display_board(game.Get_board_black(), r);
            display_board(game.Get_board_white(), r);

        }
        public static void display_board(Checkers_piece[,] board, int number_of_fields_in_row)
        {
            Console.Write("\n");
            for (int i = 0; i < number_of_fields_in_row; i++)//i is row
            {
                Console.Write("\n" + (number_of_fields_in_row - i - 1) + ". ");
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    if (board[i, j] == null)
                    { Console.Write("N "); }

                    else if (board[i, j].get_type() == Piece.Black_king)
                    { Console.Write((int)board[i, j].get_type() + " "); }

                    else if (board[i, j].get_type() == Piece.Black_man)
                    { Console.Write((int)board[i, j].get_type() + " "); }

                    else if (board[i, j].get_type() == Piece.White_king)
                    { Console.Write((int)board[i, j].get_type() + " "); }

                    else if (board[i, j].get_type() == Piece.White_man)
                    { Console.Write((int)board[i, j].get_type() + " "); }
                    else
                    { Console.Write("X "); }
                }
            }
            Console.Write("\n---");
            for (int i = 0; i < number_of_fields_in_row; i++)
            {
                Console.Write(i + " ");
            }
            Console.Write("\n");
        }
    }
}
