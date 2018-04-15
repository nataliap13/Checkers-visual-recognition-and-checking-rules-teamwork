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
            try
            {
                Draughts_checkers game = new Draughts_checkers(r, pcs);
                Display_board(game, true);
                Display_board(game, false);
                Console.WriteLine(game.Generate_player_key(true));
                Console.WriteLine(game.Generate_player_key(false));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void Display_board(Draughts_checkers game, bool display_white)
        {
            if (display_white)
            { Display_board(game.Get_board_white(), game.Number_of_fields_in_row); }
            else
            { Display_board(game.Get_board_black(), game.Number_of_fields_in_row); }
        }
        private static void Display_board(Checkers_piece[,] board, int number_of_fields_in_row)
        {
            Console.Write("\n");
            for (int i = 0; i < number_of_fields_in_row; i++)//i is row
            {
                Console.Write("\n" + (number_of_fields_in_row - i - 1) + ". ");
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    if (board[i, j] == null)
                    { Console.Write("# "); }
                    else
                    {
                        Console.Write(board[i, j].ToString() + " ");
                    }
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
