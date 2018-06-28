using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic
{
    public static class Display_Board
    {
        public static void Display_board_of_game(Draughts_checkers game, Color color)
        { Display_board_helper(game, color); }
        private static void Display_board_helper(Draughts_checkers game, Color color)
        {
            Checkers_piece[,] board = game.Get_copy_of_board(color);
            int number_of_fields_in_row = game.Number_of_fields_in_row;
            Display_board_array(board, number_of_fields_in_row, color);
        }
        public static void Display_board_array(Checkers_piece[,] board, int _number_of_fields_in_row, Color color)
        {
            Console.Write("\n---");
            for (int i = 0; i < _number_of_fields_in_row; i++)
            { Console.Write(i + " "); }

            for (int i = 0; i < _number_of_fields_in_row; i++)//i is row
            {
                Console.Write("\n" + i + ". ");
                for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
                {
                    if (board[i, j] == null)
                    { Console.Write("= "); }
                    //{ Console.Write("# "); }
                    else
                    { Console.Write(board[i, j].ToString() + " "); }
                }
            }
            Console.Write("\n---");
            for (int i = 0; i < _number_of_fields_in_row; i++)
            { Console.Write(i + " "); }
            Console.Write(color + "\n");
        }
    }
}
