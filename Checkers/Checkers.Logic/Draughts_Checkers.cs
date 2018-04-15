using Checkers.Logic;
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
        private int _number_of_fields_in_row;
        private int _number_of_white_men;
        private int _number_of_black_men;
        private int _number_of_white_kings;
        private int _number_of_black_kings;
        private int _player_black_secret_key = 0;
        private int _player_white_secret_key = 0;
        private int _player_turn_key;
        private Random _rand = new Random();

        public Draughts_checkers(int number_of_fields_in_row_, int number_of_pieces_per_player)
        {
            if (number_of_pieces_per_player >= (number_of_fields_in_row_ * number_of_fields_in_row_ / 4))
            {
                throw new Exception("To many pieces per player.");
            }
            _number_of_fields_in_row = number_of_fields_in_row_;
            int number_of_whites = number_of_pieces_per_player;
            int number_of_blacks = number_of_pieces_per_player;
            board = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];

            for (int i = 0; i < _number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
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

            for (int i = _number_of_fields_in_row - 1; i > -0; i--)//i is row
            {
                for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
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
            _number_of_white_men = number_of_pieces_per_player;
            _number_of_black_men = number_of_pieces_per_player;
            _number_of_white_kings = 0;
            _number_of_black_kings = 0;
        }

        public int Number_of_fields_in_row { get => _number_of_fields_in_row; }
        public int Number_of_white_men { get => _number_of_white_men; }
        public int Number_of_black_men { get => _number_of_black_men; }
        public int Number_of_white_kings { get => _number_of_white_kings; }
        public int Number_of_black_kings { get => _number_of_black_kings; }

        public Checkers_piece[,] Get_board_black()//row,column
        { return board; }

        public Checkers_piece[,] Get_board_white()//row,column
        {
            Checkers_piece[,] white_board = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];//row,column

            for (int i = 0; i < _number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
                {
                    white_board[_number_of_fields_in_row - i - 1, _number_of_fields_in_row - j - 1] = board[i, j];
                }
            }

            return white_board;
        }

        public int Generate_player_key(bool is_player_white)
        {
            if (_player_white_secret_key == 0 && is_player_white == true)
            {
                _player_turn_key = _player_white_secret_key = _rand.Next(10000, 32000);
                return _player_white_secret_key;
            }
            else if (_player_black_secret_key == 0 && is_player_white == false)
            {
                return _player_black_secret_key = _rand.Next(10000, 32000);
            }
            else
            {
                throw new Exception("Player already exists!");
            }

        }
        private void Switch_player_turn_key()
        {
            if (_player_turn_key == _player_black_secret_key)
            { _player_turn_key = _player_white_secret_key}

            else if (_player_turn_key == _player_white_secret_key)
            { _player_turn_key = _player_black_secret_key}
            else
            { throw new Exception("Unexpected Switch_player_turn_key exception!"); };
        }
        public int Check_active_player()
        {
            if (_player_turn_key == _player_black_secret_key)
            { return 1; }

            else if (_player_turn_key == _player_white_secret_key)
            { return 0; }
            else
            { throw new Exception("The player did not join yet!"); }
        }
        public bool Check_active_player(int player_secret_key)
        {
            if (_player_turn_key == player_secret_key)
            { return true; }
            else
            { return false; }
        }

        public int Make_move(int player_secret_key, Coordinates origin, Coordinates destination)//int is error code, to be implemented
        {
            if (Check_active_player(player_secret_key))
            {
                //do something
                Switch_player_turn_key();//zmien aktywnego gracza na drugiego gracza
                return 1;
            }
            else
            {
                return 0;//to be determined which error code
                //return error
            }
        }

    }
}
