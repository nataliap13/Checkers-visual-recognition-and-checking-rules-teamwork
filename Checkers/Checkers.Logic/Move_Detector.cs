using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic
{
    public class Move_Detector
    {
        private static Checkers_piece[,] Rotate_board(Checkers_piece[,] board_to_rotate, int number_of_fields_in_row)
        {
            Checkers_piece[,] board_rotated = new Checkers_piece[number_of_fields_in_row, number_of_fields_in_row];//row,column

            for (int i = 0; i < number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < number_of_fields_in_row; j++)//j is column
                {
                    board_rotated[number_of_fields_in_row - i - 1, number_of_fields_in_row - j - 1] = board_to_rotate[i, j];
                }
            }
            return board_rotated;
        }
        private static Checkers_piece[,] Simulate_a_move(Color moving_player_color, Draughts_checkers original_game, Coordinates origin, Coordinates destination)//does nor change original_board
        {
            //Console.WriteLine("Ruch probuje wykonac gracz " + moving_player_color);
            Draughts_checkers simulate_game = new Draughts_checkers(original_game.Number_of_fields_in_row, original_game.Number_of_pieces_per_player);

            var white_key = simulate_game.Generate_player_key(Color.White);

            var black_key = simulate_game.Generate_player_key(Color.Black);

            simulate_game.Set_board(original_game.Check_active_player(), original_game.Get_copy_of_board(original_game.Check_active_player()));
            simulate_game.Last_moved_piece_coords = original_game.Last_moved_piece_coords;
            simulate_game.Last_moved_piece_coords_color = original_game.Last_moved_piece_coords_color;

            //Console.WriteLine("Aktywny gracz to " + simulate_game.Check_active_player());

            int active_simulation_player_key = 0;
            if (moving_player_color == Color.Black)
            { active_simulation_player_key = black_key; }
            else
            { active_simulation_player_key = white_key; }
            simulate_game.Make_move(active_simulation_player_key, origin, destination);
            //Console.WriteLine("Tu sie powiodlo.");
            return simulate_game.Get_copy_of_board(moving_player_color);
        }
        public static Coordinates[] DetectMove(Color moving_player_color, Draughts_checkers game_before, Checkers_piece[,] real_black_board_after)
        {
            var board_before = game_before.Get_copy_of_board(game_before.Check_active_player());
            Checkers_piece[,] board_after;// = new Checkers_piece[game_before.Number_of_fields_in_row, game_before.Number_of_fields_in_row];
            if(moving_player_color == Color.Black)
            { board_after = real_black_board_after; }
            else
            { board_after = Rotate_board(real_black_board_after, game_before.Number_of_fields_in_row); }

            List<Coordinates> possible_origins = new List<Coordinates>();
            List<Coordinates> possible_destinations = new List<Coordinates>();
            for (int i = 0; i < game_before.Number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < game_before.Number_of_fields_in_row; j++)//j is column
                {
                    if (board_before[i, j] != board_after[i, j])
                    {
                        if (board_before[i, j] != null && board_after[i, j] == null)
                        { possible_origins.Add(new Coordinates(j, i)); }
                        else if (board_before[i, j] == null && board_after[i, j] != null)
                        { possible_destinations.Add(new Coordinates(j, i)); }
                        else
                        {
                            throw new Exception("Incompatible state of origin end final board!");
                        }
                    }
                }
            }
            if (possible_origins.Count == 0 && possible_destinations.Count == 0)
            { throw new Exception("No move detected!"); }
            if (possible_destinations.Count != 1)
            { throw new Exception("You have to move exactly one piece!"); }

            //szukanie pionka o kolorze tego, ktory sie pojawil na nowym polu
            List<Coordinates> moved_pieces = new List<Coordinates>();
            foreach (var position in possible_origins)
            {
                if (board_before[position.Y, position.X].Color == board_after[possible_destinations[0].Y, possible_destinations[0].X].Color)
                { moved_pieces.Add(position); }
            }
            if (moved_pieces.Count != 1)
            { throw new Exception("You have to move exactly one piece!"); }

            //symulowanie ruchu
            var after_simulation_board = Simulate_a_move(moving_player_color, game_before, moved_pieces[0], possible_destinations[0]);
            for (int i = 0; i < game_before.Number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < game_before.Number_of_fields_in_row; j++)//j is column
                {
                    if (after_simulation_board[i, j] != board_after[i, j])
                    { throw new Exception("Wrong state of final board!"); }
                }
            }
            return new Coordinates[2] { moved_pieces[0], possible_destinations[0] };
        }
    }
}
