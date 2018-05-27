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
        private int _number_of_pieces_per_player;
        private Random _rand = new Random();

        public Draughts_checkers(int number_of_fields_in_row_, int number_of_pieces_per_player)
        {
            _number_of_pieces_per_player = number_of_pieces_per_player;
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
        public int Number_of_pieces_per_player { get => _number_of_pieces_per_player; }

        public Checkers_piece[,] Get_copy_of_board(Color color)
        {
            if (color == Color.Black)
            {
                var copy_of_board_black = board_black.Clone() as Checkers_piece[,];
                return copy_of_board_black;
            }
            else
            { return Rotate_board(board_black); }
        }
        public void Set_board(Color active_player_color, Checkers_piece[,] board)
        {
            if(Check_active_player() != active_player_color)
            { Switch_player_turn(); }

            if (active_player_color == Color.Black)
            {
                var copy_of_board_black = board.Clone() as Checkers_piece[,];
                board_black = copy_of_board_black;
            }
            else
            { board_black = Rotate_board(board); }
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

        public int Generate_player_key(Color color)
        {
            if (_player_white_secret_key == 0 && color == Color.White)
            {
                _player_turn_key = _player_white_secret_key = _rand.Next(10000, 32000);
                return _player_white_secret_key;
            }
            else if (_player_black_secret_key == 0 && color == Color.Black)
            {
                return _player_black_secret_key = _rand.Next(10000, 32000);
            }
            else
            {
                throw new Exception("Player " + color + " already exists!");
            }

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
        public Color Check_player_color(int player_secret_key)
        {
            if (player_secret_key == _player_black_secret_key)
            { return Color.Black; }

            else if (player_secret_key == _player_white_secret_key)
            { return Color.White; }
            else
            { throw new Exception("The player did not join yet!"); }
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
        private void Switch_player_turn()
        {
            if (_player_turn_key == _player_black_secret_key)
            { _player_turn_key = _player_white_secret_key; }

            else if (_player_turn_key == _player_white_secret_key)
            { _player_turn_key = _player_black_secret_key; }
            else
            { throw new Exception("Unexpected Switch_player_turn_key exception!"); };
        }

        public void Make_move(int player_secret_key, Coordinates origin, Coordinates destination)//int is error code, to be implemented
        {
            if (Check_active_player(player_secret_key) == false)
            { throw new Exception("Wrong player is trying to make a move. It's Not your turn, " + Check_oponent_player() + "!"); }

            if (origin == destination)
            { throw new Exception("Origin and destination is the same field!"); }

            var work_board = Get_copy_of_board(Check_active_player());
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
                //szukanie najdluzszych bic dla pionkow ktore to bicie oferuja
                int length_of_capturing = 0;
                var all_the_longest_possible_ways = Get_the_longest_capturings(work_board, ref length_of_capturing);

                //Console.WriteLine("\nZnaleziono " + length_of_capturing + " bic z rzedu.");
                //Console.WriteLine("Najdluzsze bicia mozna wykonac sciezkami: ");
                //foreach (var way in all_the_longest_possible_ways)
                //{
                //    foreach (var step in way)
                //    {
                //        Console.Write(" -> " + step.ToString());
                //    }
                //    Console.Write("\n");
                //}

                //odleglosc wraz ze znakiem zwrotu/kierunku
                var x_distance = destination.X - origin.X;
                var y_distance = destination.Y - origin.Y;
                if (length_of_capturing > 0)//bicie jest obowiazkowe
                {
                    List<Coordinates> chosen_way = new List<Coordinates>();//szukanie sciezki wybranej przez gracza
                    foreach (var way in all_the_longest_possible_ways)
                    {
                        if ((way[0] == origin) && (way[1] == destination))
                        {
                            chosen_way = way;
                        }
                    }
                    if (chosen_way.Count() == 0)//exception
                    {
                        string s = string.Empty;
                        foreach (var way in all_the_longest_possible_ways)
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
                        Single_capturing_by_piece(work_board, origin, destination);
                        Set_board(Check_active_player(), work_board);

                        if (length_of_capturing > 1)
                        {
                            Switch_player_turn();//zmiana aktywnego gracza. Na koncu sprawdzania zasad zawsze jest zamiana,
                            //wiec poprzez podwojna zamiane, tura wroci na bijacego gracza i bedzie on miec dodatkowy ruch na bicie
                        }
                        else if (length_of_capturing == 1 && current_piece.Type == Type.Man && destination.Y == 0)//jesli ruszymy pionek o 1 i dociera on do bandy to na pewno zostanie zamieniony na dame
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
                    work_board[destination.Y, destination.X] = work_board[origin.Y, origin.X];
                    work_board[origin.Y, origin.X] = null;
                    Set_board(Check_active_player(), work_board);
                }
                else
                { throw new Exception("Move " + origin.ToString() + "->" + destination.ToString() + " not allowed!"); }
            }
            Switch_player_turn();//zmien aktywnego gracza na drugiego gracza jesli nie bylo bicia albo bylo to juz ostatnie mozliwe bicie
        }

        private List<List<Coordinates>> Find_the_longest_capturings_for_this_piece_and_oponent_and_destination(Checkers_piece[,] work_board, Coordinates origin, Coordinates oponent, Coordinates dest)
        {
            var possible_ways = new List<List<Coordinates>>();
            try
            {
                if ((work_board[oponent.Y, oponent.X].Color == Check_oponent_player()) && (work_board[dest.Y, dest.X] == null))
                {
                    var copy_of_board = new Checkers_piece[_number_of_fields_in_row, _number_of_fields_in_row];
                    copy_of_board = work_board.Clone() as Checkers_piece[,];
                    Single_capturing_by_piece(copy_of_board, origin, dest);//trzeba wykonac to bicie na kopii planszy
                    var local_possible_ways = Find_the_longest_capturings_for_this_piece(copy_of_board, dest);
                    if (local_possible_ways.Count() == 0)
                    {
                        var new_list = new List<Coordinates>();
                        new_list.Add(new Coordinates(dest.X, dest.Y));
                        local_possible_ways.Add(new_list);
                    }
                    else
                    {
                        foreach (var way in local_possible_ways)
                        {
                            way.Reverse();
                            way.Add(new Coordinates(dest.X, dest.Y));
                            way.Reverse();
                        }
                    }
                    possible_ways = possible_ways.Concat(local_possible_ways).ToList();
                }
            }
            catch (Exception e)
            { }
            return possible_ways;
        }

        private List<List<Coordinates>> Find_the_longest_capturings_for_this_piece(Checkers_piece[,] work_board, Coordinates origin)
        {
            string name_of_function = "Get_the_longest_capturings_for_this_piece";
            try
            {
                if (work_board[origin.Y, origin.X].Color == Check_active_player())
                {
                    var possible_ways = new List<List<Coordinates>>();

                    if (work_board[origin.Y, origin.X].Type == Type.Man)
                    {
                        List<Coordinates> oponents = new List<Coordinates>();
                        oponents.Add(new Coordinates(origin.X - 1, origin.Y - 1));
                        oponents.Add(new Coordinates(origin.X + 1, origin.Y - 1));
                        oponents.Add(new Coordinates(origin.X + 1, origin.Y + 1));
                        oponents.Add(new Coordinates(origin.X - 1, origin.Y + 1));

                        List<Coordinates> dests = new List<Coordinates>();
                        dests.Add(new Coordinates(origin.X - 2, origin.Y - 2));
                        dests.Add(new Coordinates(origin.X + 2, origin.Y - 2));
                        dests.Add(new Coordinates(origin.X + 2, origin.Y + 2));
                        dests.Add(new Coordinates(origin.X - 2, origin.Y + 2));

                        for (int i = 0; i < dests.Count(); i++)
                        {
                            var single_iteration_possible_ways = Find_the_longest_capturings_for_this_piece_and_oponent_and_destination(work_board, origin, oponents[i], dests[i]);
                            possible_ways = possible_ways.Concat(single_iteration_possible_ways).ToList();
                        }
                    }
                    else if (work_board[origin.Y, origin.X].Type == Type.King)
                    {//dama moze bic po skosie i zatrzymac sie na dowlonym polu za pionkiem. Nie musi to byc pole bezposrednio za pionkiem.
                        for (int i = 1; i < Number_of_fields_in_row; i++)//i to odleglosc pomiedzy nasza dama a zbijanym pionkiem/dama przeciwnika
                        {
                            //Console.WriteLine("i = " + i);
                            for (int j = i + 1; j < Number_of_fields_in_row; j++)//j to odleglosc pomiedzy polem naszej damy, a wolnym polem za zbijanym pionkiem/dama
                            {
                                List<Coordinates> oponents = new List<Coordinates>();
                                oponents.Add(new Coordinates(origin.X - i, origin.Y - i));
                                oponents.Add(new Coordinates(origin.X + i, origin.Y - i));
                                oponents.Add(new Coordinates(origin.X + i, origin.Y + i));
                                oponents.Add(new Coordinates(origin.X - i, origin.Y + i));

                                List<Coordinates> dests = new List<Coordinates>();
                                dests.Add(new Coordinates(origin.X - j, origin.Y - j));
                                dests.Add(new Coordinates(origin.X + j, origin.Y - j));
                                dests.Add(new Coordinates(origin.X + j, origin.Y + j));
                                dests.Add(new Coordinates(origin.X - j, origin.Y + j));

                                //trzeba sprawdzic czy pomiedzy dama a pionkiem przeciwnika wszystkie pola sa wolne
                                //trzeba sprawdzic czy pomiedzy pionkiem przeciwnika a miejscem odlozenia damy tez wszystkie pola sa wolne
                                //ponizsze zmienne okreslaja czy od damy do zbijanego pionka, oraz czy od zbijanego pionka do pola destination wszystkie inne pola sa puste
                                //jesli nie, czyli po drodze jest jakis przeszkadzajacy pionek, to nie rozwazamy takiej sciezki bicia
                                bool empty_fields_to_oponent1 = true;
                                bool empty_fields_to_oponent2 = true;
                                bool empty_fields_to_oponent3 = true;
                                bool empty_fields_to_oponent4 = true;
                                List<bool> empty_fields_to_oponents = new List<bool>();
                                empty_fields_to_oponents.Add(empty_fields_to_oponent1);
                                empty_fields_to_oponents.Add(empty_fields_to_oponent2);
                                empty_fields_to_oponents.Add(empty_fields_to_oponent3);
                                empty_fields_to_oponents.Add(empty_fields_to_oponent4);

                                for (var d = 1; d < i; d++)//d is distandce_from_king_to_empty_field // d = 1 bo zaczynamy 1 pole dalej niz pole origin
                                {
                                    try
                                    {
                                        if (work_board[origin.Y - d, origin.X - d] != null)
                                        { empty_fields_to_oponent1 = false; }
                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent1 = false; }

                                    try
                                    {
                                        if (work_board[origin.Y - d, origin.X + d] != null)
                                        { empty_fields_to_oponent2 = false; }
                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent2 = false; }

                                    try
                                    {
                                        if (work_board[origin.Y + d, origin.X + d] != null)
                                        { empty_fields_to_oponent3 = false; }
                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent3 = false; }

                                    try
                                    {
                                        if (work_board[origin.Y + d, origin.X - d] != null)
                                        { empty_fields_to_oponent4 = false; }
                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent4 = false; }
                                }
                                for (var d = i + 1; d < j; d++)//d is distandce_from oponent piece_to_empty_field // d = i + 1 bo zaczynamy jedno pole dalej niz stoi przeciwnik
                                {
                                    try
                                    {
                                        if (work_board[origin.Y - d, origin.X - d] != null)
                                        { empty_fields_to_oponent1 = false; }
                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent1 = false; }

                                    try
                                    {
                                        if (work_board[origin.Y - d, origin.X + d] != null)
                                        { empty_fields_to_oponent2 = false; }
                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent2 = false; }

                                    try
                                    {
                                        if (work_board[origin.Y + d, origin.X + d] != null)
                                        { empty_fields_to_oponent3 = false; }
                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent3 = false; }

                                    try
                                    {
                                        if (work_board[origin.Y + d, origin.X - d] != null)
                                        { empty_fields_to_oponent4 = false; }

                                    }
                                    catch (Exception)
                                    { empty_fields_to_oponent4 = false; }
                                }

                                for (int dest_ID = 0; dest_ID < dests.Count(); dest_ID++)
                                {
                                    if (empty_fields_to_oponents[dest_ID] == true)
                                    {
                                        var single_iteration_possible_ways = Find_the_longest_capturings_for_this_piece_and_oponent_and_destination(work_board, origin, oponents[dest_ID], dests[dest_ID]);
                                        possible_ways = possible_ways.Concat(single_iteration_possible_ways).ToList();
                                    }
                                }
                            }
                        }
                    }
                    else
                    { throw new Exception("Something went wrong with piece type in " + name_of_function + "!"); }
                    var final_possible_ways = new List<List<Coordinates>>();
                    int final_length = 0;

                    foreach (var way in possible_ways)
                    {
                        if (way.Count() == final_length)
                        {
                            final_possible_ways.Add(way);
                        }
                        else if (way.Count() > final_length)
                        {
                            final_possible_ways = new List<List<Coordinates>>();
                            final_possible_ways.Add(way);
                            final_length = way.Count();//tu dodalam te linijke
                        }
                    }
                    return final_possible_ways;
                }
                else
                { throw new Exception("Something went wrong in " + name_of_function + "!"); }
            }
            catch (Exception)
            { }
            return new List<List<Coordinates>>();
        }
        private List<List<Coordinates>> Get_the_longest_capturings(Checkers_piece[,] work_board, ref int length_of_capturing)
        {
            List<List<Coordinates>> all_the_longest_possible_ways = new List<List<Coordinates>>();
            length_of_capturing = 0;
            for (int i = 0; i < _number_of_fields_in_row; i++)//row
            {
                for (int j = 0; j < _number_of_fields_in_row; j++)//column
                {
                    var possible_ways_for_this_piece = Find_the_longest_capturings_for_this_piece(work_board, new Coordinates(j, i));
                    if (possible_ways_for_this_piece.Count() > 0)
                    {
                        foreach (var way in possible_ways_for_this_piece)
                        {//dlugosc bicia jest o 1 krotsza od dlugosci sciezki, dlatego najpierw sprawdzamy a potem dodajemy poczatkowe wspolrzedne
                            if (length_of_capturing > 0 && way.Count == length_of_capturing)
                            {
                                way.Reverse();
                                way.Add(new Coordinates(j, i));
                                way.Reverse();
                                all_the_longest_possible_ways.Add(way);
                            }
                            else if (way.Count() > length_of_capturing)
                            {
                                length_of_capturing = way.Count();
                                all_the_longest_possible_ways = new List<List<Coordinates>>();
                                way.Reverse();
                                way.Add(new Coordinates(j, i));
                                way.Reverse();
                                all_the_longest_possible_ways.Add(way);
                            }
                        }
                    }
                }
            }
            return all_the_longest_possible_ways;
        }

        private void Single_capturing_by_piece(Checkers_piece[,] board, Coordinates origin, Coordinates destination)//changes a board!
        {
            string name_of_function = "Single_capturing_by_piece";
            if (board[origin.Y, origin.X].Type == Type.Man)
            {
                var current_piece = board[origin.Y, origin.X];
                //odleglosc wraz ze znakiem zwrotu/kierunku
                var x_distance = destination.X - origin.X;
                var y_distance = destination.Y - origin.Y;

                if ((x_distance == 2 || x_distance == -2) && (y_distance == 2 || y_distance == -2))//bicie pionkiem w dowolnym kierunku
                {
                    board[destination.Y, destination.X] = board[origin.Y, origin.X];
                    board[origin.Y, origin.X] = null;
                    var oponent_piece_coords = new Coordinates((destination.X - x_distance / 2), (destination.Y - y_distance / 2));
                    var oponent_piece = board[oponent_piece_coords.Y, oponent_piece_coords.X];
                    if (Check_oponent_player() != oponent_piece.Color)
                    { throw new Exception("Your are trying to jump over your own piece!"); }

                    board[oponent_piece_coords.Y, oponent_piece_coords.X] = null;

                    var possible_ways = Find_the_longest_capturings_for_this_piece(board, destination);

                    if (current_piece.Type == Type.Man && destination.Y == 0 && (possible_ways.Count() == 0))//jesli nie ma dalszego bicia to pionek sie zmienia na dame
                    { board[destination.Y, destination.X] = new Checkers_piece(Check_active_player(), Type.King); }
                }
                else
                { throw new Exception("Capturing is not allowed right now!"); }
            }
            else if (board[origin.Y, origin.X].Type == Type.King)
            {
                //Console.WriteLine("Przed zbiciem");
                //Display_board_helper(work_board, Color.White);
                board[destination.Y, destination.X] = board[origin.Y, origin.X];
                board[origin.Y, origin.X] = null;

                var x_distance = destination.X - origin.X;
                if (x_distance < 0)
                { x_distance *= -1; }

                //wynulowanie zbijanego pionka
                //wczesniej bylo sprawdzone czy po drodze nie ma zadnych innych pionkow
                //wiec mozna nie szukac zbijanego pionka tylko po prostu wpisac null na kazde pole
                //Console.WriteLine("x_distance = " + x_distance);
                for (int i = 1; i < x_distance; i++)
                {
                    if (destination.X > origin.X && destination.Y > origin.Y)
                    {
                        board[origin.Y + i, origin.X + i] = null;
                        //Console.WriteLine("Null na " + (origin.X + i) + "," + (origin.Y + i));
                    }
                    else if (destination.X < origin.X && destination.Y > origin.Y)
                    {
                        board[origin.Y + i, origin.X - i] = null;
                        //Console.WriteLine("Null na " + (origin.X - i) + "," + (origin.Y + i));
                    }
                    else if (destination.X > origin.X && destination.Y < origin.Y)
                    {
                        board[origin.Y - i, origin.X + i] = null;
                        //Console.WriteLine("Null na " + (origin.X + i) + "," + (origin.Y - i));
                    }
                    else if (destination.X < origin.X && destination.Y < origin.Y)
                    {
                        board[origin.Y - i, origin.X - i] = null;
                        //Console.WriteLine("Null na " + (origin.X - i) + "," + (origin.Y - i));
                    }
                }

                //var possible_ways = Get_the_longest_capturings_for_this_piece(work_board, destination);

                //Console.WriteLine("Po zbiciu");
                //Display_board_helper(work_board, Color.White);
            }
            else
            { throw new Exception("Something went wrong with piece type in " + name_of_function + "!"); }

        }

        private void Display_board_helper(Checkers_piece[,] board, Color color)
        {
            Console.WriteLine("Display_board_helper");
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
            Console.WriteLine("Display_board_helper END!");
        }
    }
}
