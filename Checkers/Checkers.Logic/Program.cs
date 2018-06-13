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
                test_move_detector_2();
                //test_move_detector_1_no_move_detected();
                //test_Piotrka();
                //test_change_man_to_king();
                //test_capturing_oponents_piece();
                //test_capturing_multiple_oponents_pieces_by_one_piece();
                //test_capturing_oponent_piece_by_multiple_pieces();
                //test_NOT_change_man_to_king_and_capturings();
                //test_capturing_multiple_oponents_pieces_by_one_king();
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

        private static void test_move_detector_2()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            var black_key = game.Generate_player_key(Color.Black);
            
            Draughts_checkers game2 = new Draughts_checkers(r, pcs);
            var white_key2 = game2.Generate_player_key(Color.White);
            var black_key2 = game2.Generate_player_key(Color.Black);

            Make_move_and_display_boards(ref game2, white_key2, new Coordinates(0, 5), new Coordinates(1, 4));

            //Display_board(game, Color.White);
            //Display_board(game2, Color.White);
            //Console.WriteLine("\ntry\n");
            try
            {
                var move = Move_Detector.DetectMove(game.Check_active_player(), game, game2.Get_copy_of_board(Color.Black));
                Console.WriteLine("Wykryto ruch");
                Make_move_and_display_boards(ref game, white_key, new Coordinates(move[0].X, move[0].Y), new Coordinates(move[1].X, move[1].Y));

                Make_move_and_display_boards(ref game2, black_key2, new Coordinates(0, 5), new Coordinates(1, 4));
                move = Move_Detector.DetectMove(game.Check_active_player(), game, game2.Get_copy_of_board(Color.Black));
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
        }
        private static void test_move_detector_1_no_move_detected()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            Checkers_piece[,] board_black = new Checkers_piece[game.Number_of_fields_in_row, game.Number_of_fields_in_row];
            {
                Draughts_checkers game_temp = new Draughts_checkers(r, pcs);
                board_black = game_temp.Get_copy_of_board(Color.Black);
            }
            Display_board(game);
            try
            {
                var move = Move_Detector.DetectMove(Color.White, game, board_black);
                Make_move_and_display_boards(ref game, black_key, new Coordinates(5, 0), new Coordinates(0, 5));
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
        }
        private static void test_Piotrka()
        {
            const int r = 8;// number_of_fields_in_row = 8;
            const int pcs = 12;// number_of_pieces_per_player = 12;
            Draughts_checkers game = new Draughts_checkers(r, pcs);
            var white_key = game.Generate_player_key(Color.White);
            Console.WriteLine("White key: " + white_key);

            var black_key = game.Generate_player_key(Color.Black);
            Console.WriteLine("Black key: " + black_key);
            Checkers_piece[,] board = new Checkers_piece[game.Number_of_fields_in_row, game.Number_of_fields_in_row];
            board[0, 1] = new Checkers_piece(Color.White, Type.Man);
            board[0, 3] = new Checkers_piece(Color.White, Type.Man);
            board[0, 5] = new Checkers_piece(Color.Black, Type.King);
            board[0, 7] = new Checkers_piece(Color.White, Type.Man);
            board[1, 0] = new Checkers_piece(Color.Black, Type.Man);
            board[0, 3] = new Checkers_piece(Color.Black, Type.Man);
            board[4, 7] = new Checkers_piece(Color.White, Type.Man);
            board[6, 1] = new Checkers_piece(Color.Black, Type.Man);
            board[6, 3] = new Checkers_piece(Color.Black, Type.Man);
            board[6, 7] = new Checkers_piece(Color.Black, Type.Man);
            board[7, 0] = new Checkers_piece(Color.Black, Type.Man);
            board[7, 2] = new Checkers_piece(Color.Black, Type.Man);
            game.Set_board(Color.Black, board);

            Display_board(game);
            Make_move_and_display_boards(ref game, black_key, new Coordinates(5, 0), new Coordinates(0, 5));
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
