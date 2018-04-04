using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.logic

{
    class Draughts_checkers
    {
        private Checkers_piece[,] board;//row,column
        private int number_of_fields_in_row;
        private int number_of_white_men;
        private int number_of_black_men;
        private int number_of_white_kings;
        private int number_of_black_kings;
        public Draughts_checkers(int number_of_fields_in_row_, int number_of_pieces_per_player)
        {
            number_of_fields_in_row = number_of_fields_in_row_;
            int number_of_whites = number_of_pieces_per_player;
            int number_of_blacks = number_of_pieces_per_player;
            board = new Checkers_piece[number_of_fields_in_row, number_of_fields_in_row];

            for (int i = 0; i < number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    if ((i % 2 + j % 2) == 1 && number_of_whites > 0)
                    {
                        board[i, j] = new Checkers_piece(Piece.White_man);
                        number_of_whites--;
                    }
                    else if (number_of_whites == 0)
                    { break; }
                }
            }

            for (int i = number_of_fields_in_row - 1; i > -0; i--)//i is row
            {
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    if ((i % 2 + j % 2) == 1 && number_of_blacks > 0)
                    {
                        board[i, j] = new Checkers_piece(Piece.Black_man);
                        number_of_blacks--;
                    }
                    else if (number_of_blacks == 0)
                    { break; }
                }
            }
            number_of_white_men = number_of_pieces_per_player;
            number_of_black_men = number_of_pieces_per_player;
            number_of_white_kings = 0;
            number_of_black_kings = 0;
        }

        public Checkers_piece[,] Get_board_black()//row,column
        { return board; }

        public Checkers_piece[,] Get_board_white()//row,column
        {
            Checkers_piece[,] white_board = new Checkers_piece[number_of_fields_in_row, number_of_fields_in_row];//row,column

            for (int i = 0; i < number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    white_board[number_of_fields_in_row - i - 1, number_of_fields_in_row - j - 1] = board[i, j];
                }
            }

            return white_board;
        }

    }
}
