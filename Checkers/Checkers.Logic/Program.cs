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
                //test_change_man_to_king();
                //test_capturing_oponents_piece();
                //test_capturing_multiple_oponents_pieces_by_one_piece();
                //test_capturing_oponent_piece_by_multiple_pieces();
                //test_NOT_change_man_to_king_and_capturings();
                test_capturing_multiple_oponents_pieces_by_one_king();
                //test_of_reference_in_functions();
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
        }
        private static void Display_board_helper(Draughts_checkers game, Color color)
        {
            Checkers_piece[,] board = game.Get_copy_of_board(color);
            int number_of_fields_in_row = game.Number_of_fields_in_row;
            Display_board_helper(board, number_of_fields_in_row, color);
        }

        private static void Display_board_helper(Checkers_piece[,] board, int _number_of_fields_in_row, Color color)
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
        public static void Display_board(Draughts_checkers game)//displays a board of current player
        { Display_board_helper(game, game.Check_active_player()); }
        public static void Display_board(Draughts_checkers game, Color color)
        { Display_board_helper(game, color); }
        public static void Make_move_and_display_boards(ref Draughts_checkers game, int player_secret_key, Coordinates origin, Coordinates destination)
        {
            Console.WriteLine("\n" + origin.ToString() + " -> " + destination.ToString());
            game.Make_move(player_secret_key, origin, destination);
            Display_board(game, game.Check_player_color(player_secret_key));
            Display_board(game, game.Check_active_player());
        }

        private static void test_change_man_to_king()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            Checkers_piece[,] board = new Checkers_piece[game.Number_of_fields_in_row, game.Number_of_fields_in_row];
            board[1, 6] = new Checkers_piece(Color.White, Type.Man);
            game.Set_board(Color.White, board);

            Display_board(game);
            Make_move_and_display_boards(ref game, white_key, new Coordinates(6, 1), new Coordinates(5, 0));
        }
        private static void test_capturing_oponents_piece()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            Display_board(game, Color.White);
            //Display_board(game, Color.Black);
            Coordinates f1 = new Coordinates(0, 5);
            Coordinates f2 = new Coordinates(1, 4);
            Make_move_and_display_boards(ref game, white_key, f1, f2);
            Make_move_and_display_boards(ref game, black_key, new Coordinates(6, 5), new Coordinates(5, 4));
            Make_move_and_display_boards(ref game, white_key, new Coordinates(2, 5), new Coordinates(3, 4));
            Make_move_and_display_boards(ref game, black_key, new Coordinates(5, 4), new Coordinates(7, 2));
        }
        private static void test_capturing_multiple_oponents_pieces_by_one_piece()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            Display_board(game, Color.White);
            //Display_board(game, Color.Black);
            Coordinates f1 = new Coordinates(0, 5);
            Coordinates f2 = new Coordinates(1, 4);
            Make_move_and_display_boards(ref game, white_key, f1, f2);
            Make_move_and_display_boards(ref game, black_key, new Coordinates(4, 5), new Coordinates(3, 4));
            Make_move_and_display_boards(ref game, white_key, new Coordinates(2, 5), new Coordinates(3, 4));
            Make_move_and_display_boards(ref game, black_key, new Coordinates(3, 4), new Coordinates(5, 2));
            Make_move_and_display_boards(ref game, black_key, new Coordinates(5, 2), new Coordinates(7, 4));
        }
        private static void test_capturing_oponent_piece_by_multiple_pieces()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            //Display_board(game, Color.White);
            //Display_board(game, Color.Black);
            Make_move_and_display_boards(ref game, white_key, new Coordinates(2, 5), new Coordinates(3, 4));
            Make_move_and_display_boards(ref game, black_key, new Coordinates(0, 5), new Coordinates(1, 4));
            Make_move_and_display_boards(ref game, white_key, new Coordinates(6, 5), new Coordinates(5, 4));
            Make_move_and_display_boards(ref game, black_key, new Coordinates(6, 5), new Coordinates(5, 4));
            //Make_move_and_display_boards(ref game, white_key, new Coordinates(7, 6), new Coordinates(6, 5));//runs wrong way exception
            Make_move_and_display_boards(ref game, white_key, new Coordinates(5, 4), new Coordinates(7, 2));//not run exception
            //Make_move_and_display_boards(ref game, black_key, new Coordinates(5, 4), new Coordinates(6, 3));
        }
        private static void test_NOT_change_man_to_king_and_capturings()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);

            Checkers_piece[,] board = new Checkers_piece[game.Number_of_fields_in_row, game.Number_of_fields_in_row];
            board[2, 7] = new Checkers_piece(Color.White, Type.Man);
            board[1, 6] = new Checkers_piece(Color.Black, Type.Man);
            board[1, 4] = new Checkers_piece(Color.Black, Type.Man);
            game.Set_board(Color.White, board);

            Display_board(game);
            Make_move_and_display_boards(ref game, white_key, new Coordinates(7, 2), new Coordinates(5, 0));
            Make_move_and_display_boards(ref game, white_key, new Coordinates(5, 0), new Coordinates(3, 2));
        }
        private static void test_capturing_multiple_oponents_pieces_by_one_king()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            Checkers_piece[,] board = new Checkers_piece[game.Number_of_fields_in_row, game.Number_of_fields_in_row];
            board[4, 1] = new Checkers_piece(Color.White, Type.Man);
            board[5, 2] = new Checkers_piece(Color.White, Type.King);
            board[2, 1] = new Checkers_piece(Color.Black, Type.Man);
            board[6, 5] = new Checkers_piece(Color.Black, Type.Man);
            board[1, 4] = new Checkers_piece(Color.Black, Type.Man);
            game.Set_board(Color.White, board);

            Display_board(game);
            Make_move_and_display_boards(ref game, white_key, new Coordinates(2, 5), new Coordinates(3, 4));
            Make_move_and_display_boards(ref game, black_key, new Coordinates(6, 5), new Coordinates(5, 4));
            //Make_move_and_display_boards(ref game, white_key, new Coordinates(3, 4), new Coordinates(2, 5));//runs exception
            Make_move_and_display_boards(ref game, white_key, new Coordinates(3, 4), new Coordinates(6, 7));//not run exception

            //Make_move_and_display_boards(ref game, white_key, new Coordinates(0, 1), new Coordinates(6, 7));//not run exception
            Make_move_and_display_boards(ref game, white_key, new Coordinates(1, 4), new Coordinates(3, 2));// runs exception
        }
        private static void test_of_reference_in_functions()//now result is no//bcoz function set_board has been changed to load a copy, not a reference
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            Checkers_piece[,] board = new Checkers_piece[game.Number_of_fields_in_row, game.Number_of_fields_in_row];
            board[4, 3] = new Checkers_piece(Color.White, Type.King);
            board[3, 2] = new Checkers_piece(Color.Black, Type.Man);
            board[6, 5] = new Checkers_piece(Color.Black, Type.Man);
            game.Set_board(Color.Black, board);
            Display_board(game);

            board[1, 0] = new Checkers_piece(Color.Black, Type.King);
            Display_board(game);
        }

    }
}
