using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic

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
                        board[i, j] = new Checkers_piece(Color.White, Type.Man);
                        number_of_whites--;
                    }
                    else if (number_of_whites == 0)
                    { break; }
                }
            }

            for (int i = _number_of_fields_in_row - 1; i >= 0; i--)//i is row
            {
                for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
                {
                    if ((i % 2 + j % 2) == 1 && number_of_blacks > 0)
                    {
                        board[i, j] = new Checkers_piece(Color.Black, Type.Man);
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

        public Checkers_piece[,] Get_board(Color color)//row,column
        {
            if (color == Color.Black)
            {
                return board;
            }
            else
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
            { _player_turn_key = _player_white_secret_key; }

            else if (_player_turn_key == _player_white_secret_key)
            { _player_turn_key = _player_black_secret_key; }
            else
            { throw new Exception("Unexpected Switch_player_turn_key exception!"); };
        }
        public Color Check_active_player()
        {
            if (_player_turn_key == _player_black_secret_key)
            { return Color.Black; }

            else if (_player_turn_key == _player_white_secret_key)
            { return Color.White; }
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

        public void Make_move(int player_secret_key, Coordinates origin, Coordinates destination)//int is error code, to be implemented
        {
            if (Check_active_player(player_secret_key) == false)
            { throw new Exception("Wrong player is trying to make a move. It's Not your turn!"); }

            if (origin == destination)
            { throw new Exception("Origin and destination is the same field!"); }

            var work_board = Get_board(Check_active_player());
            var checkers_piece = work_board[origin.Y, origin.X];
            if (checkers_piece == null)
            { throw new Exception("This field is empty!"); }

            if (Check_active_player() != checkers_piece.Color)
            { throw new Exception("Your are trying to move not your piece!"); }

            if (work_board[destination.Y, destination.X] == null)//sprawdz czy pole destination jest puste
            {
                var x_distance = destination.X - origin.X;
                var y_distance = destination.Y - origin.Y;
                if ((x_distance == 1 || x_distance == -1) && y_distance == -1)//przesuniecie pionka do przodu tylko
                {
                    work_board[destination.Y, destination.X] = work_board[origin.Y, origin.X];
                    work_board[origin.Y, origin.X] = null;
                }
                //przesuwanie damy w dowolnym kierunku
                //bicie przy uzyciu damy
                //czy jest bicie
                else
                { throw new Exception("Not allowed move!"); }
            }
            else
            { throw new Exception("Destination field is not empty!"); }

            //sprawdz czy jest oddalone tylko o 1 od bierzacego
            //jesli nie to sprawdz czy na oddalonym o 1 jest pionek przeciwnika
            //do something
            Switch_player_turn_key();//zmien aktywnego gracza na drugiego gracza jesli nie bylo bicia albo bylo to juz ostatnie mozliwe bicie
        }
    }
}
