using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic

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
                Display_board(game, Color.White);
                Display_board(game, Color.Black);
                var white_key = game.Generate_player_key(true);
                Console.WriteLine("White key: " + white_key);

                var black_key = game.Generate_player_key(false);
                Console.WriteLine("Black key: " + black_key);
                Coordinates begin = new Coordinates(0, 5);
                Coordinates end = new Coordinates(1, 4);
                game.Make_move(white_key, begin, end);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void Display_board(Draughts_checkers game, Color color)
        {
            Checkers_piece[,] board = game.Get_board(color);
            int number_of_fields_in_row = game.Number_of_fields_in_row;
            //Console.Write("\n");

            Console.Write("\n---");
            for (int i = 0; i < number_of_fields_in_row; i++)
            {
                Console.Write(i + " ");
            }

            for (int i = 0; i < number_of_fields_in_row; i++)//i is row
            {
                Console.Write("\n" + i + ". ");
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    if (board[i, j] == null)
                    { Console.Write("= "); }
                    //{ Console.Write("# "); }
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
