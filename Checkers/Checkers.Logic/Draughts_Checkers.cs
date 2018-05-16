using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Logic
{
    public class Draughts_checkers
    {
        private Checkers_piece[,] board_black;//row,column
        private int _number_of_fields_in_row;
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
            board_black = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];

            for (int i = 0; i < _number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
                {
                    if ((i % 2 + j % 2) == 1 && number_of_whites > 0)
                    {
                        board_black[i, j] = new Checkers_piece(Color.White, Type.Man);
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
                        board_black[i, j] = new Checkers_piece(Color.Black, Type.Man);
                        number_of_blacks--;
                    }
                    else if (number_of_blacks == 0)
                    { break; }
                }
            }
        }

        public int Number_of_fields_in_row { get => _number_of_fields_in_row; }
        public int Number_of_pieces(Color color, Type type)
        {
            int number_of_pieces = 0;
            for (int i = 0; i < Number_of_fields_in_row; i++)
            {
                for (int j = 0; j < Number_of_fields_in_row; j++)
                {
                    if (board_black[i, j].Color == color && board_black[i, j].Type == type)
                    { number_of_pieces++; }
                }
            }
            return number_of_pieces;
        }
        //private Checkers_piece[,] Board_Black { set => board_black = value; }
        //private Checkers_piece[,] Board_White { set => board_black = Rotate_board(value); }

        public Checkers_piece[,] Get_board(Color color)//row,column
        {
            if (color == Color.Black)
            { return board_black; }
            else
            { return Rotate_board(board_black); }
        }
        private void Set_board(Color color, Checkers_piece[,] new_board)//row,column
        {
            if (color == Color.Black)
            { board_black = new_board; }
            else
            { board_black = Rotate_board(new_board); }
        }
        private Checkers_piece[,] Rotate_board(Checkers_piece[,] board_to_rotate)
        {
            Checkers_piece[,] board_rotated = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];//row,column

            for (int i = 0; i < _number_of_fields_in_row; i++)//i is row
            {
                for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
                {
                    board_rotated[_number_of_fields_in_row - i - 1, _number_of_fields_in_row - j - 1] = board_to_rotate[i, j];
                }
            }
            return board_rotated;
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
        public Color Check_oponent_player()
        {
            if (_player_turn_key != _player_black_secret_key)
            { return Color.Black; }

            else if (_player_turn_key != _player_white_secret_key)
            { return Color.White; }
            else
            { throw new Exception("The player did not join yet!"); }
        }

        public void Make_move(int player_secret_key, Coordinates origin, Coordinates destination)//int is error code, to be implemented
        {
            if (Check_active_player(player_secret_key) == false)
            { throw new Exception("Wrong player is trying to make a move. It's Not your turn, " + Check_oponent_player() + "!"); }

            if (origin == destination)
            { throw new Exception("Origin and destination is the same field!"); }

            var work_board = Get_board(Check_active_player());
            var current_piece = work_board[origin.Y, origin.X];
            if (current_piece == null)
            { throw new Exception("Origin field is empty!"); }

            var checkers_piece_dest = work_board[destination.Y, destination.X];
            if (checkers_piece_dest != null)
            { throw new Exception("Destination field " + destination.ToString() + " is not empty!"); }

            if (Check_active_player() != current_piece.Color)
            { throw new Exception("Your are trying to move not your piece!"); }

            if ((destination.Y + destination.X) % 2 == 0)
            { throw new Exception("Your are trying to move a piece to the white field!"); }
            //wlasciwa rozgrywka
            {
                //szukanie bicia dla pionka do przodu w lewo
                List<List<Coordinates>> possible_ways = new List<List<Coordinates>>();
                int length_of_capturing = 0;
                for (int i = 0; i < _number_of_fields_in_row; i++)//row
                {
                    for (int j = 0; j < _number_of_fields_in_row; j++)//column
                    {
                        List<Coordinates> destinations = new List<Coordinates>();
                        int length_of_capturing_for_this_piece = find_next_capture_for_this_piece(work_board, new Coordinates(j, i), ref destinations);
                        if (length_of_capturing_for_this_piece == length_of_capturing && length_of_capturing > 0)
                        {
                            destinations.Reverse();
                            destinations.Add(new Coordinates(j, i));
                            destinations.Reverse();
                            possible_ways.Add(destinations);
                        }
                        else if (length_of_capturing_for_this_piece > length_of_capturing)
                        {
                            length_of_capturing = length_of_capturing_for_this_piece;
                            possible_ways = new List<List<Coordinates>>();
                            destinations.Reverse();
                            destinations.Add(new Coordinates(j, i));
                            destinations.Reverse();
                            possible_ways.Add(destinations);
                        }
                    }
                }
                Console.WriteLine("\nZnaleziono " + length_of_capturing + " bic z rzedu.");
                Console.WriteLine("Najdluzsze bicie/bicia mozna wykonac pionkami: ");
                foreach (var way in possible_ways)
                {
                    foreach (var jump in way)
                    {
                        Console.Write(" -> " + jump.ToString());
                    }
                    Console.WriteLine("\nlub");
                }
                Console.WriteLine("Koniec opcji.");
                //doto
                //szukanie bicia dla damy

                //odleglosc wraz ze znakiem zwrotu/kierunku
                var x_distance = destination.X - origin.X;
                var y_distance = destination.Y - origin.Y;
                if (length_of_capturing > 0)//bicie jest obowiazkowe
                {
                    List<Coordinates> chosen_way = new List<Coordinates>();//szukanie sciezki wybranej przez gracza
                    foreach (var way in possible_ways)
                    {
                        if ((way[0] == origin) && (way[1] == destination))
                        {
                            chosen_way = way;
                        }
                    }
                    if (chosen_way.Count() == 0)//exception
                    {
                        string s = string.Empty;
                        foreach (var way in possible_ways)
                        {
                            foreach (var step in way)
                            {
                                s += " -> " + step.ToString();
                            }
                            s += "\n";
                        }
                        throw new Exception("\nYou have to choose one of presented ways:\n" + s);
                    }
                    else//wlasciwe bicie
                    {
                        single_capturing_by_piece(ref work_board, origin, destination);
                        if (length_of_capturing > 1)
                        {
                            Switch_player_turn_key();//zmiana aktywnego gracza. Na koncu sprawdzania zasad zawsze jest zamiana,
                            //wiec poprzez podwojna zamiane, tura wroci na bijacego gracza i bedzie on miec dodatkowy ruch na bicie
                        }
                        else if(length_of_capturing == 1 && current_piece.Type == Type.Man && destination.Y == 0)//jesli ruszymy pionek o 1 i dociera on do bandy to na pewno zostanie zamieniony na dame
                    {
                            work_board[destination.Y, destination.X] = new Checkers_piece(Check_active_player(), Type.King);
                            Set_board(Check_active_player(), work_board);
                        }
                    }
                }
                else if ((x_distance == 1 || x_distance == -1) && y_distance == -1)//przesuniecie pionka lub damy do przodu
                {
                    work_board[destination.Y, destination.X] = work_board[origin.Y, origin.X];
                    work_board[origin.Y, origin.X] = null;
                    Set_board(Check_active_player(), work_board);
                    if (current_piece.Type == Type.Man && destination.Y == 0)//jesli ruszymy pionek o 1 i dociera on do bandy to na pewno zostanie zamieniony na dame
                    {
                        work_board[destination.Y, destination.X] = new Checkers_piece(Check_active_player(), Type.King);
                        Set_board(Check_active_player(), work_board);
                    }
                }
                else if ((x_distance == y_distance) && current_piece.Type == Type.King)//przesuniecie damy w dowolnym kierunku ukosnym
                {
                    //trzeba sprawdzic czy po drodze nie ma pionka przeciwnika ktory mozna zbic
                    //jesli na drodze jest nasz pionek to ruch nie moze zostac wykonany
                    //todo to do
                    work_board[destination.Y, destination.X] = work_board[origin.Y, origin.X];
                    work_board[origin.Y, origin.X] = null;
                    Set_board(Check_active_player(), work_board);
                }
                //czy jest bicie
                else
                { throw new Exception("Move " + origin.ToString() + "->" + destination.ToString() + " not allowed!"); }
            }
            Switch_player_turn_key();//zmien aktywnego gracza na drugiego gracza jesli nie bylo bicia albo bylo to juz ostatnie mozliwe bicie
        }
        private int find_next_capture_for_this_piece(Checkers_piece[,] work_board, Coordinates origin, ref List<Coordinates> destinations)//todo
        {//jesli wykonano juz jedno bicie, to kolejne musi byc wykonane tym samym pionkiem
            try
            {
                if (work_board[origin.Y, origin.X].Color == Check_active_player())
                {
                    int number_of_captured_pieces_1 = 0;
                    int number_of_captured_pieces_2 = 0;
                    int number_of_captured_pieces_3 = 0;
                    int number_of_captured_pieces_4 = 0;
                    Coordinates dest1 = new Coordinates(origin.X - 2, origin.Y - 2);
                    Coordinates dest2 = new Coordinates(origin.X + 2, origin.Y - 2);
                    Coordinates dest3 = new Coordinates(origin.X + 2, origin.Y + 2);
                    Coordinates dest4 = new Coordinates(origin.X - 2, origin.Y + 2);

                    try
                    {
                        if ((work_board[origin.Y - 1, origin.X - 1].Color == Check_oponent_player()) && (work_board[origin.Y - 2, origin.X - 2] == null))
                        {
                            var copy_of_board = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];
                            copy_of_board = work_board.Clone() as Checkers_piece[,];
                            single_capturing_by_piece(ref copy_of_board, origin, dest1);//trzeba wykonac to bicie na kopii planszy
                            List<Coordinates> unused_list = new List<Coordinates>();
                            number_of_captured_pieces_1 = (1 + find_next_capture_for_this_piece(copy_of_board, dest1, ref unused_list));
                        }
                    }
                    catch (Exception e)
                    { }

                    try
                    {
                        if ((work_board[origin.Y - 1, origin.X + 1].Color == Check_oponent_player()) && (work_board[origin.Y - 2, origin.X + 2] == null))
                        {
                            var copy_of_board = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];
                            copy_of_board = work_board.Clone() as Checkers_piece[,];
                            single_capturing_by_piece(ref copy_of_board, origin, dest2);//trzeba wykonac to bicie na kopii planszy
                            List<Coordinates> unused_list = new List<Coordinates>();
                            number_of_captured_pieces_2 = (1 + find_next_capture_for_this_piece(copy_of_board, dest2, ref unused_list));
                        }
                    }
                    catch (Exception e)
                    { }

                    try
                    {
                        if ((work_board[origin.Y + 1, origin.X + 1].Color == Check_oponent_player()) && (work_board[origin.Y + 2, origin.X + 2] == null))
                        {
                            var copy_of_board = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];
                            copy_of_board = work_board.Clone() as Checkers_piece[,];
                            single_capturing_by_piece(ref copy_of_board, origin, dest3);//trzeba wykonac to bicie na kopii planszy
                            List<Coordinates> unused_list = new List<Coordinates>();
                            number_of_captured_pieces_3 = (1 + find_next_capture_for_this_piece(copy_of_board, dest3, ref unused_list));
                        }
                    }
                    catch (Exception e)
                    { }

                    try
                    {
                        if ((work_board[origin.Y + 1, origin.X - 1].Color == Check_oponent_player()) && (work_board[origin.Y + 2, origin.X - 2] == null))
                        {
                            var copy_of_board = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];
                            copy_of_board = work_board.Clone() as Checkers_piece[,];
                            single_capturing_by_piece(ref copy_of_board, origin, dest4);//trzeba wykonac to bicie na kopii planszy
                            List<Coordinates> unused_list = new List<Coordinates>();
                            number_of_captured_pieces_4 = (1 + find_next_capture_for_this_piece(copy_of_board, dest4, ref unused_list));
                        }
                    }
                    catch (Exception e)
                    { }
                    int max = Math.Max(number_of_captured_pieces_1, (Math.Max(number_of_captured_pieces_2, Math.Max(number_of_captured_pieces_3, number_of_captured_pieces_4))));

                    if (number_of_captured_pieces_1 == max)
                    { destinations.Add(dest1); }
                    if (number_of_captured_pieces_2 == max)
                    { destinations.Add(dest2); }
                    if (number_of_captured_pieces_3 == max)
                    { destinations.Add(dest3); }
                    if (number_of_captured_pieces_4 == max)
                    { destinations.Add(dest4); }
                    //todo
                    //trzeba sprawdzic, czy dwie drogi nie sa rownie dlugie. Jesli tak to oba pierwsze ruchy musza być dozwolone
                    return max;
                }
                else
                { throw new Exception("Something went wrong!"); }
            }
            catch (Exception e)
            { }
            return 0;
        }
        //tode
        private void single_capturing_by_piece(ref Checkers_piece[,] work_board, Coordinates origin, Coordinates destination)
        {
            var current_piece = work_board[origin.Y, origin.X];
            //odleglosc wraz ze znakiem zwrotu/kierunku
            var x_distance = destination.X - origin.X;
            var y_distance = destination.Y - origin.Y;

            if ((x_distance == 2 || x_distance == -2) && (y_distance == 2 || y_distance == -2))//bicie pionkiem w dowolnym kierunku
            {
                work_board[destination.Y, destination.X] = work_board[origin.Y, origin.X];
                work_board[origin.Y, origin.X] = null;
                var oponent_piece_coords = new Coordinates((destination.X - x_distance / 2), (destination.Y - y_distance / 2));
                var oponent_piece = work_board[oponent_piece_coords.Y, oponent_piece_coords.X];
                if (Check_oponent_player() == oponent_piece.Color)
                { }
                else
                { throw new Exception("Your are trying to jump over your own piece!"); }

                work_board[oponent_piece_coords.Y, oponent_piece_coords.X] = null;
                Set_board(Check_active_player(), work_board);
                if (current_piece.Type == Type.Man && destination.Y == 0)
                { work_board[destination.Y, destination.X] = new Checkers_piece(Check_active_player(), Type.King); }//to bedzie trzeba zmienic bo jesli jest dalsze bicie to tego nie ma!!!!!!!!!!!!
                //!!!!!!!!!!!!
                //todo
                //jezeli sa mozliwe kolejne bicia to ten gracz ma kolejny ruch
                //!!!!!!!!!!!!!!!!!!
            }
            else
            { throw new Exception("Capturing is not allowed right now!"); }
        }
        //private void Display_board_helper(Checkers_piece[,] board, Color color)
        //{
        //    Console.Write("\n---");
        //    for (int i = 0; i < _number_of_fields_in_row; i++)
        //    { Console.Write(i + " "); }

        //    for (int i = 0; i < _number_of_fields_in_row; i++)//i is row
        //    {
        //        Console.Write("\n" + i + ". ");
        //        for (int j = 0; j < _number_of_fields_in_row; j++)//j is column
        //        {
        //            if (board[i, j] == null)
        //            { Console.Write("= "); }
        //            //{ Console.Write("# "); }
        //            else
        //            { Console.Write(board[i, j].ToString() + " "); }
        //        }
        //    }
        //    Console.Write("\n---");
        //    for (int i = 0; i < _number_of_fields_in_row; i++)
        //    { Console.Write(i + " "); }
        //    Console.Write(color + "\n");
        //}
    }
}
