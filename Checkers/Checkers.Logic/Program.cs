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
            try
            {
                change_man_to_king_test();
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
        }
        private static void Display_board(Draughts_checkers game, Color color)
        {
            Checkers_piece[,] board = game.Get_board(color);
            int number_of_fields_in_row = game.Number_of_fields_in_row;
            //Console.Write("\n");

            Console.Write("\n---");
            for (int i = 0; i < number_of_fields_in_row; i++)
            { Console.Write(i + " "); }

            for (int i = 0; i < number_of_fields_in_row; i++)//i is row
            {
                Console.Write("\n" + i + ". ");
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    if (board[i, j] == null)
                    { Console.Write("= "); }
                    //{ Console.Write("# "); }
                    else
                    { Console.Write(board[i, j].ToString() + " "); }
                }
            }
            Console.Write("\n---");
            for (int i = 0; i < number_of_fields_in_row; i++)
            { Console.Write(i + " "); }
            Console.Write(color + "\n");
        }

        private static void change_man_to_king_test() //sprawdzic zamiane pionka na dame
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(true);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(false);
            Console.WriteLine("Black key: " + black_key);
            Display_board(game, Color.White);
            //Display_board(game, Color.Black);
            Coordinates f1 = new Coordinates(0, 5);
            Coordinates f2 = new Coordinates(1, 4);
            Coordinates f3 = new Coordinates(2, 3);
            Coordinates f4 = new Coordinates(3, 2);
            Coordinates f5 = new Coordinates(4, 1);
            Coordinates f6 = new Coordinates(5, 0);
            game.Make_move(white_key, f1, f2);
            game.Make_move(black_key, new Coordinates(4, 5), new Coordinates(3, 4));
            game.Make_move(white_key, f2, f3);
            game.Make_move(black_key, new Coordinates(2, 5), new Coordinates(1, 4));
            game.Make_move(white_key, f3, f4);
            game.Make_move(black_key, new Coordinates(3, 6), new Coordinates(2, 5));
            game.Make_move(white_key, f4, f5);
            game.Make_move(black_key, new Coordinates(3, 4), new Coordinates(4, 3));
            game.Make_move(white_key, new Coordinates(2, 5), f2);
            game.Make_move(black_key, new Coordinates(2, 5), new Coordinates(3, 4));
            game.Make_move(white_key, f2, new Coordinates(0, 3));
            game.Make_move(black_key, new Coordinates(1, 6), new Coordinates(2, 5));
            game.Make_move(white_key, new Coordinates(6, 5), new Coordinates(5, 4));
            game.Make_move(black_key, new Coordinates(2, 7), new Coordinates(1, 6));
            game.Make_move(white_key, f5, f6);
            Display_board(game, Color.White);
        }
    }
}
