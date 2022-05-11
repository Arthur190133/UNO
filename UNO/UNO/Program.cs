using System;
using System.Collections.Generic;
using System.Linq;
using static UNO.Player;
using static UNO.Card;

namespace UNO
{
    // Classe du programme principale
    public class Program
    {

        // enumération des types de joueurs
        public enum player_type
        {
            none,
            AI,
            JOUEUR
        }

        // enumération des types de cartes
        public enum card_type
        {
            none,
            BASIC,
            PASSER,
            INVERSION,
            PLUS2,
            JOKER,
            PLUS4
        }

        // enumération des couleurs de cartes
        public enum card_color
        {
            BLEU,
            VERT,
            ROUGE,
            JAUNE,
            MULTICOLORE
        }

        // enumération des couleurs de la console
        public enum Console_color
        {
            Blue,
            Green,
            DarkRed,
            DarkYellow,
            DarkMagenta
        }

        // enumération des sens du jeu
        public enum Direction
        {
            Droite,
            Gauche
        }

        public struct Player_action
        {
            public int Player_index { get; set; }
            public string Player_action_text { get; set; }

            public Player_action(int _)
            {
                Player_index = -1;
                Player_action_text = "";
            }
        }

        static void Main()
        {

            string title = @"
                                            ▒▒▒▒▒▒▒║
                                       ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄▄▄▄▄▄▄▄
                                   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄██▀▀⌠▄▄▄╓└▀▀█▄,
                                ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▄█▀;▄▓▓▓▓╢╢▓▓▓▓▄└▀█
                             ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄█▀,▄▓╢▓▓▓▓▓▓▓▓▓▓╢▓▄└█▄
                          ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▄██▌▒░█▀ ▄▓▓▓▓▓▓╢▓▓▓▓╢▓▓▓▓╢▓┐▀█
                        ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▄██▀░▄ █▄█▀ ▐╣▓▓▓╢▓▀└    ▀▓╣▓▓▓▓▓▄▐▌
                      ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░█▀└▄▄▓▓╢▌▐█┘ ]▓▓▓▓╫▀         ╙▓▓▓▓▓▓▐▌
                    ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄█   ▐ ▓▓▓▓   ▓▓▓▓╫▌            ▓▓▓▓▓▓ █
                   ▒▒▒▒▒▒▒▒▒░▄▄███░▒▒▐█     ▓▓▓▓╢▌   ▓▓▓▓▓▓▐█▀▀██      ▐▓▓▓▓▌▐▌
                 ▒▒▒▒▒▒▒▒░███▀░▄▄╙██▄▒█     ▐▓▓▓▓▓C j▓▓▓▓▓ █▒▒▒▒▒█▌     ▓▓▓▓▓▌▌
                ▒▒▒▒▒░▄█▀▀.▄▓▓▓▓▓▄└▀███     ▓▓▓▓▓▓  ▌▓▓▓▓U▐▌▒▒▒▒▒█▌    ▐▓▓▓▓▓ █
              ▒▒▒▒▄█▀▀└▄▄▓  ▓▓▓▓▓▓╢▓▄ ▀█      ▓▓▓▓╫▌ ▓▓▓▓╢▌└█▒▒▒▒▒▒█    ▐▓▓▓▓▓ ▌
             ▒▒▒░█▀  █▓╢▓╢▌ ╙▓▓▓▓▓▓▓▓▓▓▄       ▐╣▓▓▓▓ ▐╣▓▓▓▓▄╙█▒▒▒▒▒█    ▓▓▓▓▓▌▐▌
          ▄▄░▒▒▒█    ▐╣▓▓▓▓⌐ ▓╣▓▓▓▓▓▓▓▓╣▓▄     █▓▓▓╢▌ █▓▓▓▓▓▄╙█▄▒▒▒█   ▓╢▓▓▓▓█
      ▄█▀▀ ▐▌▒▒▓▌     █▓▓▓▓▓  █▓▓▓▓█▓▓▓▓▓╣▓▓▄  ▐▓▓▓▓▓U└█▓▓▓▓╢▓▄╙▀▀▀ ▄▓╢▓▓▓╫▌▌
   █▀▀ ▄▄▓█ █▒▒▒█      ▌▓▓▓╫▌ ▐╣▓▓▓▓▓▀▓╢▓▓▓▓▓▓▄ █╣▓▓▓█ █╢▓▓▓▓▒▓▓▓▓▓▒╢▓▓▓▓╫▀█
 ▄█   █╢▓▓╢▌└█▒▒▐█     █╣▓▓▓▓  █▓▓▓▓█  ▀█▓▓▓▓▓╢▓█▌▓▓▓╫▌  ▀▓▓▓▓▓▓▓▓▓▓▓▓▓▓╢█▀█
█▀    ▐╣▓▓▓▓⌐▀▌▒▒█▌     █▓▓▓╢▌ ▐▓▓▓▓╫▌   ╙▓▓▓▓▓▓▓╣▓▓▓▓▓   '▀█▒▓▓▓▓▓▓▓╢▒█▀ █
▐▌     █▓▓▓▓█ █▒▒░█     ▐╣▓▓▓▓⌐ █╣▓▓▓█      ▀▓╢▓▓▓▓▓▓▓╢▌     └▀▀███▀▀▀   ▄▀
 █     ▐▓▓▓▓╫▄▐█▒▒▓▌     ▓▓▓▓▓█  ▌▓▓▓╢▌       ▀▓╣▓▓▓▓▓▓▓U              ▄█▀
  █     ▓╣▓▓▓▓ █▒▒▒█      ▓▓▓▓   ▐╣▓▓▓▓         ╙▀▓▓▓▓▓▓█           ▄▄█▀
  ▓▌     ▓▓▓▓╢▌└█▒▒╠█     ▓▓▓▓▓▓  █▓▓▓▓▓ ▄         ▀▓▓▓▀▀ ▐█▄▄▄▄▄███▀
   █     ▐╣▓▓▓▓⌐▐▌▒▒█▌    ]▓▓▓▓▓  ╙▓▓▓▓▓ ▐██╓            ▄█▒▒▒▒▒▒▒
   ▐▌     ▓▓▓▓╣▓ █▒▒▒█    ]▓▓▓▓▓   ▓╣▓▓▓▓ █▒▀█▄        ▄█▒▒▒▒▒▒▒▒
    █     ╙▓▓▓▓╫▌╙█▒▒█▌   ▓╢▓▓▓▓    ▓▓▓▓   █▒▒▀█▄,,▄▄██▀▒▒▒▒▒▒▒▒
     █     ▓╣▓▓▓╢▄'▀▀█▀ ,▓╣▓▓▓▓▓    ▐▓▓▀▀  █▒▒▒▒▒▀▀▀▒▒▒▒▒▒▒▒▒▒
     █▌     ▓╢▓▓▓╢▓▓▄▄@▓╢▓▓▓▓▓▓          ▄█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
      █      ▓╣▓▓▓▓▓▓▓▓▓▓▓▓▓╫▓ █C      ▄█▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
      ▐█      ▀▓╢▓▓▓▓▓▓▓▓╢▓▀└╓█▀█  ▄██▀▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
       ▀█       ▀▀▓▓▓▓▓▓▀╙  ▄█▒▒▀█▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
        ▀█                ▄█▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
          █▄            ▄█▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
            ▀█▄▄▄ ▄▄▄██▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
                ▀▀▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
                   ▒▒▒▒▒▒▒▒▒▒▒▒▒▒ ";



            string texte = "";
            Console.Title = "UNO";
            // Logo du Uno

            // Afficher le logo du Uno
            int Title_index = 0;
            for (int i = 0; i < title.Length; i++)
            {
                do
                {
                    Title_index++;
                    texte = texte + title.ElementAt(Title_index - 1);

                } while (title.ElementAt(Title_index - 1) == title.ElementAt(Title_index));

                switch (title.ElementAt(Title_index - 1).ToString())
                {
                    case "▒": case "║": case "╜": 
                        Show_colored_message(card_color.ROUGE, texte);
                        break;
                    case " ":
                        Console.Write(texte);
                        break;
                    case "▓":
                        Show_colored_message(card_color.JAUNE, texte);
                        break;

                    default:
                        Console.Write(texte);
                        break;
                }
                i = Title_index;
                texte = "";
            }
            Console.WriteLine();

            int[] index_playable_card = new int[120];// index des cartes jouables des joueurs
            int Start_line;
            int End_line;

            byte players_numbers = 0;// nombre de joueurs
            byte current_player = 0;// valeur du joueur actuelle 

            card_color current_color;// couleur de la derniere carte jouée
            Direction Current_direction = Direction.Droite;// Sens du jeu           

            string[] players_names = new string[4];// nom des joueurs 

            player_type[] players_types = new player_type[4];// types des joueurs 

            Player[] Players = new Player[4];// joueurs

            List<Card> deck_card = new List<Card>();// liste des cartes non jouées
            List<Card> deck_card_used = new List<Card>();// cartes jouées

            Player_action[] Players_actions = new Player_action[4];// dernières actions des joueurs

            // Assigner les valeurs par défauts
                for (int i = 0; i < Players_actions.Length ; i++)
                {
                    Players_actions[i] = new Player_action(0);
                }

            // Recuperer le nombre de joueur
            Get_players_numbers(ref players_numbers);
            // Recuperer le nom des joueurs
            Get_players_names(ref players_numbers, ref players_names);
            // Recuperer le type des joueurs
            Get_player_type(players_numbers, ref players_types);
            // créer les joueurs
            create_players(players_names, players_types, deck_card, Players);

            // Recuperer le plus grand nom des joueurs
            int longest_length = 0;
            for (int i = 0; i < players_numbers; i++)
            {
                if (Players[i].player_name.Length > longest_length)
                {
                    longest_length = Players[i].player_name.Length;
                }
            }


           // Loading("Génération des cartes en cours");
            // Ajouter les cartes du Uno dans le deck
            add_cards(ref deck_card);
            //Loading("Mélange des cartes en cours");
            // Melanger les cartes 
            shuffle_cards(ref deck_card);
            //Loading("Distribution des cartes en cours");
            // Distribuer les cartes aux joueurs
            deal_cards(ref Players, players_numbers, ref deck_card);
            // Ajouter la première carte au deck de cartes jouées
            deck_card_used.Add(Get_card_from_deck(out Card _, ref deck_card));
            // Changer la couleur du jeu
            current_color = deck_card_used.Last().color;

            // Si la première couleur de carte est MULTICOLORE, choisir une couleur aléatoirement
            if (current_color == card_color.MULTICOLORE)
            {
                Random_color(ref current_color);
            }
            // Effacer la derniere carte jouée
            Start_line = 36;
            End_line = Console.CursorTop; ;
            for (int i = 0; i < End_line - Start_line; i++)
            {
                Console.SetCursorPosition(Console.CursorLeft, End_line - i);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(Console.CursorLeft, Start_line);
            }
            play_round(players_numbers,ref Current_direction, ref current_player, ref Players, deck_card_used, ref index_playable_card, ref current_color, ref deck_card, ref Start_line, ref End_line, ref  Players_actions, longest_length);

            Console.Read();

            
        }

        // chargement des cartes
        static void Loading(string value)
        {
            var random = new Random();// variable random
            string loading_text;// texte du chargment
            int loading_value = 0;// valeur du chargment (0-100)

            Console.WriteLine("\r\n");
            // tant que la valeur du chargment est < 100
            do
            {
                System.Threading.Thread.Sleep(random.Next(300, 500));// attendre 
                loading_value = loading_value + random.Next(1, 20);// ajouter entre 1 et 20 à la valeur du chargement
                loading_value = Math.Min(Math.Max(loading_value, 0), 100);
                loading_text = $"{value} {loading_value}%";
                // remplacer la ligne du chargement
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine(loading_text);
            } while (loading_value < 100);
        }

        // récupérer le nombre de joueur
        static void Get_players_numbers(ref byte players_numbers)
        {
            string Current_players_numbers;

            //tant que current_players_numbers est un nombre, qu'il est plus grand ou egal à 2 et plus petit ou egal à 4
            do
            {
                Console.WriteLine("à combiens de personne voulez-vous jouer ? (IA compris, de 2 à 4 joueurs)");
                Current_players_numbers = Console.ReadLine();
            } while (!byte.TryParse(Current_players_numbers, out players_numbers) || players_numbers < 2 || players_numbers > 4);
        }
        // récupérer le nom des joueurs
        static void Get_players_names(ref byte players_numbers, ref string[] players_names)
        {

            for (byte i = 0; i < players_numbers; i++)
            {
                bool Wrong_name;
                // tant que le nom est vide ou que le nom est déjà pris
                do
                {
                    Wrong_name = false;
                    Console.WriteLine($"Entrez le nom du joueur numéro : {i + 1}");
                    players_names[i] = Console.ReadLine().Replace(" ", string.Empty);
                    for(byte y = 0; y < i; y++)
                    {
                        if(players_names[i] == players_names[y])
                        {
                            Wrong_name = true;
                        }
                    }
                } while ((players_names[i] == string.Empty) || players_names[i].Length > 15 || Wrong_name);
            }
        }

        // récupérer le type des joueurs
        static void Get_player_type(byte players_numbers, ref player_type[] players_types)
        {
            string Current_player_type;
            for (byte i = 0; i < players_numbers; i++)
            {
                // tant que current_player_type est un nombre et qu'il n'est pas un type de joueur
                do
                {
                    Console.WriteLine($"Quel est le type du joueur numéro : { i + 1 } ?");
                    Current_player_type = Console.ReadLine().ToUpper();
                } while (byte.TryParse(Current_player_type, out byte _) || (!Enum.TryParse(Current_player_type, out players_types[i])));
            }
        }

        // créer les joueurs
        static void create_players(string[] players_names, player_type[] players_types, List<Card> card_deck, Player[] players)
        {
            for (byte i = 0; i < 4; i++)
            {
                players[i] = new Player(players_types[i], players_names[i]);
            }
        }

        // ajout de toutes les cartes dans le deck
        static void add_cards(ref List<Card> card_deck)
        {
            card_color color = card_color.BLEU;// couleur de carte

            // ajout de toutes les cartes basiques
            for (byte i = 0; i <= 1; i++)
            {
                // ajout des cartes pour chaque couleurs
                for (byte a = 0; a < 4; a++)
                {
                    color = (card_color)a; // couleur de la carte
                    // nombre de carte
                    for (int  b = 0 + i; b <= 9; b++)
                    {
                        card_deck.Add(new Card() { types = card_type.BASIC, color = color, number = b });
                    }
                }
            }

            for (byte d = 0; d <= 1; d++)
            {
                for (byte c = 0; c < 4; c++)
                {
                    color = (card_color)c;

                    // Ajout de toutes les cartes "passe ton tour"
                    card_deck.Add(new Card() { types = card_type.INVERSION, color = color, number = -1 });

                    // Ajout de toutes les cartes "changement de sens"
                    card_deck.Add(new Card() { types = card_type.PASSER, color = color, number = -1 });

                    // Ajout de toutes les cartes "+2"
                    card_deck.Add(new Card() { types = card_type.PLUS2, color = color, number = -1 });
                }
            }
            for (byte c = 0; c <= 3; c++)
            {
                // Ajout des cartes spéciales "Changer de couleurs" et "Changer de couleurs + 4"
                card_deck.Add(new Card() { types = card_type.JOKER, color = card_color.MULTICOLORE, number = -1 });
                card_deck.Add(new Card() { types = card_type.PLUS4, color = card_color.MULTICOLORE, number = -1 });
            }
        }

        // Mélanger les cartes
        static void shuffle_cards(ref List<Card> card_deck)
        {
            var random = new Random();
            card_deck = card_deck.OrderBy(random_card => random.Next()).ToList();
        }

        // Distribuer les cartes
        static void deal_cards(ref Player[] Players, byte players_numbers, ref List<Card> card_deck)
        {
            for (byte i = 0; i < 8; i++)
            {
                for (byte p = 0; p < players_numbers; p++)
                {
                    Players[p].player_cards.Add(Get_card_from_deck(out _, ref card_deck));
                }
            }
        }

        // Trier les cartes
        static void Sort_cards(ref Player[] Players, byte players_numbers, byte current_player)
        {
            Players[current_player].player_cards = Players[current_player].player_cards.OrderBy(color => color.color).ToList();
        }

        // Récuperer une carte du deck du Uno
        static Card Get_card_from_deck(out Card card, ref List<Card> card_deck)
        {
            card = new Card
            {
                color = card_deck.First().color,
                types = card_deck.First().types,
                number = card_deck.First().number
            };
            card_deck.RemoveAt(0);

            return card;
        }

        // Commencer une manche
        static void play_round(byte players_numbers,ref Direction direction, ref byte current_player, ref Player[] Players, List<Card> card_deck_used, ref int[] index_playable_card, ref card_color current_color, ref List<Card> card_deck,ref int Start_line, ref int End_line, ref Player_action[] Players_actions, int longest_length)
        {
            Start_line = Console.CursorTop;
            Show_board_game(players_numbers, Players, direction);
            Console.WriteLine($" {Players[current_player].player_name}, c'est à vous de jouez ! \n\r\n\r");
            Show_players_actions(Players_actions, Players);// afficher les dernières actions des joueurs
            Show_last_card_played(card_deck_used.Last(), current_color);// afficher la dernière carte jouée
            Sort_cards(ref Players, players_numbers, current_player);// trier les cartes du joueur
            Can_play(current_player, Players, card_deck_used, ref index_playable_card, current_color);// récuperer les cartes jouable du joueur
            if (Players[current_player].player_type == player_type.JOUEUR)
            {
                show_player_cards(current_player, Players, index_playable_card);// afficher les cartes du joueurs qui joue, seulement si c'est un JOUEUR
            }

            if(Choose_card(current_player, ref Players, card_deck_used, index_playable_card, ref card_deck, players_numbers, current_color, direction, ref Players_actions))// choisir une carte à jouer
            {
                
                Play_card(players_numbers,ref direction, ref current_player, ref Players, card_deck_used, ref index_playable_card, ref current_color, ref card_deck, ref Players_actions);// jouer la carte que le joueur a choisis
               
            }
            else
            {
                current_player = Get_next_player(direction, current_player, players_numbers);
            }

            End_line = Console.CursorTop;
            end_round(players_numbers, direction, ref current_player, ref Players, card_deck_used, ref index_playable_card, ref current_color, ref card_deck, ref Start_line, ref End_line,  ref  Players_actions, longest_length);// fin de la manche


        }

        // Fin de manche
        static void end_round(byte players_numbers, Direction direction, ref byte current_player, ref Player[] Players, List<Card> card_deck_used, ref int[] index_playable_card, ref card_color current_color, ref List<Card> card_deck, ref int Start_line, ref int End_line, ref Player_action[] Players_actions, int longest_length)
        {

            
            for (int i = 0; i < End_line - Start_line; i++)
            {
                Console.SetCursorPosition(Console.CursorLeft, End_line - i);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(Console.CursorLeft, Start_line);

            }

            
            for (int i = 0; i < players_numbers; i++)
            {
                if (Players[i].player_cards.Count() == 0)
                {
                    Console.WriteLine($"\n\r\n\r\n\r{Players[i].player_name} à gagné");
                    Get_players_points(Players, players_numbers);
                    Show_players_points(players_numbers, Players, longest_length);
                    Console.ReadLine();
                    Environment.Exit(0);
                    
                }
            }




            play_round(players_numbers,ref direction, ref current_player, ref Players, card_deck_used, ref index_playable_card, ref current_color, ref card_deck, ref Start_line, ref End_line, ref Players_actions, longest_length);
        }

        static void Show_board_game(byte players_numbers, Player[] Players, Direction direction)
        {
            string Board_game;

            // turning arrow

            

            switch(direction)
            {
               case Direction.Droite:
                    Console.WriteLine(@"        
              ▄▄
          ▄▄▄████▄
       ▄█████████▀
      ████▀   █▀
     ████
    ▐███               
 ");
                    break;

                case Direction.Gauche:
                    Console.WriteLine(@"
         ▄▄▄██
      ▄███████
     ████▀
    ███▀
   ████
 ▀██████▀
   ▀██▀
");
                    break;
            
            }
            

            Board_game = string.Format(@"
{0, 25}" + Players[0].player_name + @"
{0, 25}" + Players[0].player_cards.Count() + @" carte(s)", " ");
            
            if(players_numbers == 2)
            {
                Board_game = Board_game + string.Format( @"


{0, 25}" + Players[1].player_name + @"
{0, 25}" + Players[1].player_cards.Count() + @" carte(s)", " " );
            }
            else
            {
                if(players_numbers == 4)
                {
                    Board_game = Board_game + string.Format(@"


    " + Players[1].player_name + @"{0,45 }" + Players[3].player_name + @"
    " + Players[1].player_cards.Count() + @" carte(s){0, 36}" + Players[3].player_cards.Count() + @" carte(s)", " ") ;
                }
                else
                {
                    Board_game = Board_game + @"


    " + Players[1].player_name + @"
    " + Players[1].player_cards.Count() + @" carte(s)";
                }
                Board_game = Board_game + string.Format(@"


{0, 25}" + Players[2].player_name + @"
{0, 25}" + Players[2].player_cards.Count() + @" carte(s)", " ");


            }

            


            Console.WriteLine(Board_game);
            //   


            switch (direction)
            {
                case Direction.Droite:
                    Console.WriteLine(string.Format(@"               
    {0, 52}         ▄▄▄▄
    {0, 52}         ███▌
    {0, 52}   ▄    ▄███▀
    {0, 52} ▄██▄▄█████▀
    {0, 52}████████▀
    {0, 52}'██", " "));
                    break;

                case Direction.Gauche:
                    Console.WriteLine(string.Format(@"               
    {0, 52}          ▄█
    {0, 52}       ▄█████▄
    {0, 52}        ████▀ 
    {0, 52}        ███▌
    {0, 52}      ▄████
    {0, 52} ▄▄██████▀
    {0, 52} ████▀▀`", " "));
                    break;
            }


            // reverse turning arrow
 
                
        }




        static void Get_players_points(Player[] Players, byte players_numbers)
        {
            int current_player_point = 0;

            for(int i = 0; i < players_numbers; i++)
            {
                for(int o = 0; o < Players[i].player_cards.Count(); o++)
                {
                    switch (Players[i].player_cards.ElementAt(o).types)
                    {
                        case card_type.BASIC:
                            current_player_point = current_player_point + Players[i].player_cards.ElementAt(o).number;
                            break;
                        case card_type.PASSER: case card_type.INVERSION :case card_type.PLUS2:
                            current_player_point = current_player_point + 20;
                            break;
                        case card_type.JOKER: case card_type.PLUS4:
                            current_player_point = current_player_point + 50;
                            break;

                    }
                }
                Players[i].player_points = current_player_point;
                current_player_point = 0;
            }
        }

        static void Show_players_points(byte players_numbers, Player[] Players, int longest_length)
        {
            string player_points = "";
            Array.Sort(Players, delegate(Player point_x, Player point_y) { return point_x.player_points.CompareTo(point_y.player_points); });


            Console.WriteLine("         Voici vos points\n\r");
            for(int i = 0; i < players_numbers; i++)
            {
                player_points = String.Format("         n°{0,-1} |{1,-" + longest_length +"} | {2,-4}", i + 1 ,Players[i].player_name , Players[i].player_points);
                Console.WriteLine(player_points);
            }
        }

        static void Show_last_card_played(Card Last_card, card_color current_color)
        {
            string card = Last_card.types + " | " + Last_card.color;

            Console.Write("\n\r                    Dernière carte jouée ");

            if (Last_card.types == card_type.BASIC)
            {
                card = card + " | " + Last_card.number;
            }
            Show_colored_message(current_color, $"{card} \n\r\n\r\n\r");

        }
       
        static bool Choose_card(byte current_player, ref Player[] Players, List<Card> card_deck_used, int[] index_playable_card, ref List<Card> card_deck, byte players_numbers, card_color current_color, Direction direction, ref Player_action[] Players_actions)
        {
            var random = new Random();// variable random

            string card_choice_type;
            string card_choice_color = null;
            string card_choice_number = null;

            Card Drew_card;

            bool return_value = true;

            switch (Players[current_player].player_type)
            {
                case player_type.AI:
                    Console.WriteLine($"\n\r{Players[current_player].player_name} est entrain de réflichir...");
                    //System.Threading.Thread.Sleep(random.Next(3000, 4000));
                    System.Threading.Thread.Sleep(random.Next(300,350));
                    AI_choose_random_card(index_playable_card, Players, current_player, current_color, players_numbers, direction,ref card_deck_used, card_deck, ref Players_actions);

                    break;

                case player_type.JOUEUR:
                    do
                    {
                        do
                        {
                            Console.WriteLine("Choisissez un type de carte à jouer ou piochez une carte ");
                            card_choice_type = Console.ReadLine().ToUpper();

                        } while (byte.TryParse(card_choice_type, out byte _) || (!Enum.TryParse(card_choice_type, out card_type _) && (card_choice_type != "PIOCHER")));

                        if (card_choice_type != "PIOCHER")
                        {
                            do
                            {
                                Console.WriteLine("Choisissez une couleur de carte à jouer ");
                                card_choice_color = Console.ReadLine().ToUpper();
                            } while (byte.TryParse(card_choice_color, out byte _) || (!Enum.TryParse(card_choice_color, out card_color _)));

                            if (card_choice_type == "BASIC")
                            {
                                do
                                {
                                    Console.WriteLine("Choisissez le numéro de la carte à jouer ");
                                    card_choice_number = Console.ReadLine();

                                } while (!byte.TryParse(card_choice_number, out byte _));
                            }
                        }


                    } while (!card_exist(card_choice_type, card_choice_color, card_choice_number, Players, current_player, index_playable_card));

                    if (card_choice_type != "PIOCHER")
                    {
                        if (card_choice_type == "BASIC")
                        {
                            card_deck_used.Add(new Card() { types = (card_type)Enum.Parse(typeof(card_type), card_choice_type), color = (card_color)Enum.Parse(typeof(card_color), card_choice_color), number = int.Parse(card_choice_number) });
                        }
                        else
                        {
                            card_deck_used.Add(new Card() { types = (card_type)Enum.Parse(typeof(card_type), card_choice_type), color = (card_color)Enum.Parse(typeof(card_color), card_choice_color) });
                        }
                    }
                    else
                    {
                        Draw_card(Players, card_deck, card_deck_used, current_player, ref Players_actions, new Card() { types = card_type.BASIC } );
                        Drew_card = Players[current_player].player_cards.Last();
                        if(play_drew_card(Drew_card, card_deck_used, current_color, Players[current_player].player_type))
                        {
                            card_deck_used.Add(Drew_card);
                            Players[current_player].player_cards.Remove(Drew_card);
                        }
                        else
                        {
                            return_value = false;
                        }

                    }
                    break;
            }
            return return_value;
        }

        static bool card_exist(string card_choice_type, string card_choice_color, string card_choice_number, Player[] Players, byte current_player, int[] index_playable_card)
        {
            byte index_card = 0;

            Card Card = new Card() { types = card_type.none, color = card_color.BLEU, number = -2 };

            if (card_choice_type != "PIOCHER")
            {
                if (card_choice_type == "BASIC")
                {
                    Card = Players[current_player].player_cards.Find(card => card.types == (card_type)Enum.Parse(typeof(card_type), card_choice_type) && card.color == (card_color)Enum.Parse(typeof(card_color), card_choice_color) && card.number == int.Parse(card_choice_number));
                }
                else
                {
                    Card = Players[current_player].player_cards.Find(card => card.types == (card_type)Enum.Parse(typeof(card_type), card_choice_type) && card.color == (card_color)Enum.Parse(typeof(card_color), card_choice_color));
                }

                if (Card != null)
                {
                    index_card = (byte)Players[current_player].player_cards.IndexOf(Card);
                    if (index_playable_card.Contains(index_card))
                    {
                        Players[current_player].player_cards.RemoveAt(index_card);
                        return true;
                    }

                    Console.WriteLine("Vous ne pouvez pas jouer cette carte !\n\r\n\r");
                    return false;

                }
            }
            else
            {
                return true;
            }
            Console.WriteLine("La carte n'existe pas  !\n\r\n\r");
            return false;
        }

        static void Play_card(byte players_numbers,ref Direction direction, ref byte current_player, ref Player[] Players, List<Card> card_deck_used, ref int[] index_playable_card, ref card_color current_color, ref List<Card> card_deck, ref Player_action[] Players_actions)
        {


            switch (card_deck_used.Last().types)
            {

                case card_type.INVERSION:
                    Switch_direction(ref direction);

                    break;

                case card_type.PASSER:
                    Console.WriteLine($"{ Players[Get_next_player(direction, current_player, players_numbers)].player_name}, vous ne pouvez pas jouer ce tour !");
                    current_player = Get_next_player(direction, current_player, players_numbers);

                    break;

                case card_type.PLUS2:
                    Draw_card(Players, card_deck, card_deck_used,  Get_next_player(direction, current_player, players_numbers),ref Players_actions, card_deck_used.Last());
                    Console.WriteLine($"{ Players[Get_next_player(direction, current_player, players_numbers)].player_name}, vous ne pouvez pas jouer ce tour !");
                    current_player = Get_next_player(direction, current_player, players_numbers);

                    break;

                case card_type.PLUS4:
                    Draw_card(Players, card_deck,  card_deck_used, Get_next_player(direction, current_player, players_numbers), ref Players_actions, card_deck_used.Last());
                    change_color(ref current_color, current_player, Players[current_player].player_type, Players, ref Players_actions);
                    Console.WriteLine($"{ Players[Get_next_player(direction, current_player, players_numbers)].player_name}, vous ne pouvez pas jouer ce tour !");
                    current_player = Get_next_player(direction, current_player, players_numbers);

                    break;

                case card_type.JOKER:
                    change_color(ref current_color, current_player, Players[current_player].player_type, Players, ref  Players_actions);

                    break;


            }

            if (card_deck_used.Last().color != card_color.MULTICOLORE)
            {
                current_color = card_deck_used.Last().color;
            }
            string Player_action = $"#player_name# a joué  &{card_deck_used.Last().color}& £{card_deck_used.Last().types} | {card_deck_used.Last().color}";
            if (card_deck_used.Last().types == card_type.BASIC)
            {
                Player_action = Player_action + $" | {card_deck_used.Last().number}";
            }
            Player_action = Player_action + "£";
            Update_players_actions_list(ref Players_actions, Player_action, current_player);
            current_player = Get_next_player(direction, current_player, players_numbers);

        }


        static void show_player_cards(byte current_player, Player[] Players, int[] index_playable_card)
        {
            string card;
            byte index_card = 0;

            Console.WriteLine("\r\n Voici vos cartes ");
            // récupèrer le plus grand length des types de cartes du joueur
            int longest_length = Players[current_player].player_cards.Max(c => c == null ? 0 : c.types.ToString().Length);


            for (byte i = 0; i < Players[current_player].player_cards.Count; i++)
            {
                card = String.Format("              {0,-" + longest_length + "} | {1,-12}", Players[current_player].player_cards.ElementAt(i).types, Players[current_player].player_cards.ElementAt(i).color);
                if ((Players[current_player].player_cards.ElementAt(i).types == card_type.BASIC))
                {
                    card = card + String.Format("|{0}", Players[current_player].player_cards.ElementAt(i).number);
                }
                if (i == index_playable_card[index_card])
                {
                    index_card++;
                    Show_colored_message(Players[current_player].player_cards.ElementAt(i).color, card);
                    Console.WriteLine("\n\r");
                }
                else
                {
                    Console.WriteLine($"{card}\n\r");
                }
            }
        }

        static bool Can_play(byte player, Player[] Players, List<Card> card_deck_used, ref int[] index_playable_card, card_color current_color)
        {
            int index_card = 0;
            // mettre -1 comme valeur par defaut pour index_playable_card
            for (int i = 0; i < index_playable_card.Length; i++)
            {
                index_playable_card[i] = -1;
            }

            for (byte i = 0; i < Players[player].player_cards.Count; i++)
            {
                if (card_deck_used.Last().types == card_type.BASIC)
                {
                    if (((Players[player].player_cards.ElementAt(i).color == current_color) || (Players[player].player_cards.ElementAt(i).number == card_deck_used.Last().number)) || Players[player].player_cards.ElementAt(i).color == card_color.MULTICOLORE)
                    {

                        index_playable_card[index_card] = i;
                        index_card++;
                    }
                }
                else
                {
                    if (((Players[player].player_cards.ElementAt(i).types == card_deck_used.Last().types)) || ((Players[player].player_cards.ElementAt(i).color == current_color)) || (Players[player].player_cards.ElementAt(i).color == card_color.MULTICOLORE))
                    {
                        index_playable_card[index_card] = i;
                        index_card++;
                    }
                }
            }
            if (index_card == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static void Random_color(ref card_color current_color)
        {
            var random = new Random();

            Console.Write("La couleur est maitenant du ");
            current_color = (card_color)random.Next(0, 4);
            Show_colored_message(current_color, $"{current_color} \n\r");
        }

        static void Show_colored_message(card_color color, string message)
        {
            Console_color ConsoleColor = (Console_color)color;
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ConsoleColor.ToString());
            Console.Write(message);
            Console.ResetColor();
        }

        // changer le sens du jeu
        static void Switch_direction(ref Direction current_direction)
        {

            switch (current_direction)
            {
                // changer la direction pour la gauche
                case Direction.Droite:
                    current_direction = Direction.Gauche;
                    break;

                // changer la direction pour la droite
                case Direction.Gauche:
                    current_direction = Direction.Droite;
                    break;

            }
        }

        // Changer la couleur du jeu
        static void change_color(ref card_color current_color, byte current_player, player_type Player_type, Player[] Players, ref Player_action[] Players_actions)
        {
            string color;

            byte current_number_color;
            byte max_number_color = 0;

            card_color card_color = 0 ;
            card_color max_color = 0;

            switch (Player_type)
            {
                case player_type.JOUEUR:
                    // tant que color est un nombre, que color n'est pas une couleur de carte et que color est MULTICOLORE et 
                    do
                    {
                        Console.WriteLine("Entrez une nouvelle couleur ! ");
                        color = Console.ReadLine().ToUpper();
                    } while (byte.TryParse(color, out byte value) || !Enum.TryParse(color, out current_color) || color == card_color.MULTICOLORE.ToString());
                    break;

                case player_type.AI:
                    for(int i = 0; i < 4; i++)
                    {
                        current_number_color = 0;
                        card_color = card_color + i;
                        for(int o = 0; o < Players[current_player].player_cards.Count(); o++)
                        {
                            if (Players[current_player].player_cards.ElementAt(0).color == card_color)
                            {
                                current_number_color++;
                            }
                        }
                        if(current_number_color > max_number_color)
                        {
                            max_number_color = current_number_color;
                            max_color = card_color;
                        }
                    }
                    current_color = max_color;
                    break;
            }
            // afficher la nouvelle couleur
            Console.Write("La couleur est maitenant du ");
            Show_colored_message(current_color, $"{current_color} \n\r");
            Update_players_actions_list(ref Players_actions, $"#player_name# a changé la couleur en &{current_color}& £{current_color}£", current_player);

        }

        // récupérer le prochain joueur
        static byte Get_next_player(Direction direction, byte current_player, byte players_numbers)
        {
            switch (direction)
            {
                case Direction.Droite:
                    if (current_player == players_numbers - 1)
                    {
                        current_player = 0;
                    }
                    else
                    {
                        current_player++;
                    }
                    break;

                case Direction.Gauche:
                    if (current_player == 0)
                    {
                        current_player = (byte)(players_numbers - 1);
                    }
                    else
                    {
                        current_player--;
                    }

                    break;
            }
            return current_player;
        }

        // distribuer des cartes
        static void Draw_card(Player[] Players, List<Card> card_deck, List<Card> card_deck_used, byte index_player_to_draw, ref Player_action[] player_actions, Card card)
        {
            string card_value;

            byte current_player = index_player_to_draw;

            if (card_deck.Count == 0 && card_deck_used.Count == 0)
            {
                Console.WriteLine("Il n'y a plus de carte disponible, vous devez jouer !");
                Console.ReadLine();
            }
            else
            {
                void draw(int draw_number)
                {
                    // remplire le deck si il est vide
                    if (card_deck.Count() < draw_number)
                    {
                        do
                        {
                            card_deck.Add(card_deck_used.ElementAt(0));
                            card_deck_used.RemoveAt(0);
                        } while (card_deck_used.Count() > 1);

                        shuffle_cards(ref card_deck);
                    }
                    Console.WriteLine($"{Players[current_player].player_name} à pioché : ");
                    for (byte i = 0; i < draw_number; i++)
                    {
                        Players[current_player].player_cards.Add(Get_card_from_deck(out Card _, ref card_deck));
                        card_value = "\r\n        " + Players[current_player].player_cards.Last().types + " / " + Players[current_player].player_cards.Last().color;
                        if (Players[current_player].player_cards.Last().types == card_type.BASIC)
                        {
                            card_value = card_value + " / " + Players[current_player].player_cards.Last().number;
                        }
                        Show_colored_message(Players[current_player].player_cards.Last().color, $"{card_value}\r\n");
                    }                
                }
                int card_draw_number;
                // Choisir le nombre de carte à récuperer depuis le deck du Uno
                switch (card.types)
                {
                    case card_type.PLUS2:
                        draw(2);
                        card_draw_number = 2;
                        if(Players[current_player].player_type == player_type.JOUEUR)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        
                        break;

                    case card_type.PLUS4:
                        draw(4);
                        card_draw_number = 4;
                        if (Players[current_player].player_type == player_type.JOUEUR)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                        break;

                    default:
                        draw(1);
                        card_draw_number = 1;
                        break;
                }
                string player_action = $"#player_name# a pioché {card_draw_number} carte(s)";
                Update_players_actions_list(ref player_actions, player_action, current_player); // Mettre à jour la liste des actions des joueurs
            }
            
        }
        
        // Jouer la carte que le joueur viens de piocher
        static bool play_drew_card(Card Drew_card, List<Card> card_deck_used, card_color current_color, player_type Player_type)
        {
            bool can_play = false;

            string play_card;

            if (card_deck_used.Last().types == card_type.BASIC)
            {
                if (((Drew_card.color == current_color) || (Drew_card.number == card_deck_used.Last().number)) || Drew_card.color == card_color.MULTICOLORE)
                {
                    can_play = true;
                }
            }
            else
            {
                if (((Drew_card.types == card_deck_used.Last().types)) || ((Drew_card.color == current_color)) || (Drew_card.color == card_color.MULTICOLORE))
                {
                    can_play = true;
                }
            }

            if (can_play)
            {
                switch (Player_type)
                {
                    case player_type.JOUEUR:  
                        do
                        {
                            Console.WriteLine("Voulez vous jouer la carte que vous venez de piocher ?");
                            play_card = Console.ReadLine().ToUpper();
                        } while ((play_card != "OUI") && (play_card != "NON"));

                        if(play_card == "OUI")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case player_type.AI:
                        return true;
                }
            }
            else if (Player_type == player_type.JOUEUR)
            {
                Console.WriteLine("Vous ne pouvez pas jouer la carte que vous venez de piocher");
                return false;
            }
            return false;
        }

        // Mettre à jour la liste des actions des joueurs
        static void Update_players_actions_list(ref Player_action[] Players_actions, string Message, int Player_index)
        {
            int Last_action_index = -1;

            do
            {
                Last_action_index++;

            } while (Players_actions[Last_action_index ].Player_index != -1 && Last_action_index != 3);

            if (Players_actions[Players_actions.Length - 1].Player_index != -1)
            {
                for (int i = 0; i <= 2; i++)
                {
                    Players_actions[i] = Players_actions[i + 1];
                }
                Players_actions[3].Player_index = Player_index;
                Players_actions[3].Player_action_text = Message;
            }
            else
            {
                Players_actions[Last_action_index].Player_index = Player_index;
                Players_actions[Last_action_index].Player_action_text = Message;
            }

        }

        // Afficher les actions des joueurs
        static void Show_players_actions(Player_action[] Players_actions, Player[] Players)
        {
            int Last_action_index = -1;

            do
            {
                Last_action_index++;


            } while (Last_action_index != 4 && Players_actions[Last_action_index].Player_index != -1);

            
            if (Players_actions[0].Player_index != -1)
            {
                Console.WriteLine("                   Dernières actions des joueurs \n");
                for (int i = 1; i <= Last_action_index; i++)
                {
                    string Message = Players_actions[i - 1].Player_action_text;
                    string Color_message = Get_substring_from_string(Message, "£");
                    string String_color = Get_substring_from_string(Message, "&");
                    Message = Message.Replace("£" + Color_message + "£", "");
                    Message = Message.Replace("&" + String_color + "&", "");
                    Message = Message.Replace("#player_name#", Players[Players_actions[i - 1].Player_index].player_name);
                    Console.Write($"                    {Message}");
                    if (Color_message != " ")
                    {
                        card_color Color = (card_color)Enum.Parse(typeof(card_color), String_color);
                        Show_colored_message(Color, Color_message);
                    }
                    Console.WriteLine();

                }
            }


        }

        // Récuperer un morceau d'une chaine de caractère 
        static string Get_substring_from_string(string Message, string Start)
        {
            string result = " ";
            if (Message.Contains(Start))
            {
                int Start_index = Message.IndexOf(Start, 0) + Start.Length;
                int End_index = Message.IndexOf(Start, Start_index);
                result = Message.Substring(Start_index, End_index - Start_index);
            }
            return result;
        }

        // Choisir la carte de l'IA
        static void AI_choose_random_card(int[] index_playable_cards, Player[] Players, byte current_player, card_color current_color, byte players_numbers, Direction direction,ref List<Card> card_deck_used, List<Card> card_deck, ref Player_action[] Players_actions)
        {
            card_color Card_color = card_color.BLEU;
            List<card_color> Max_color = new List<card_color>();

            List<Card> Card_to_test = new List<Card>();
            Card Drew_card = new Card();

            byte[,] Cards_color_numbers = new byte[4, 2] { { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 } }; // Matrice contentant la couleur et son nombre de cartes dans le deck du joueur (1: coleur, 2: nombre de cartes)
            byte current_type_card = 0;// Nombre de couleur de carte actuelle 
            byte index_card = 0;
            byte card_number = 0;
            byte max_card_number = 0;
            byte max = 0;
            byte AI_number_cards = (byte)Players[current_player].player_cards.Count();// Nombre de carte de l'AI
            byte Next_player_number_card = (byte)Players[Get_next_player(direction, current_player, players_numbers)].player_cards.Count();// Nombre de carte du prochain joueur

            int[] index_playable_cards_prediction = new int[120]; // Tableau d'index contenant les cartes jouables des prediction

            float Pourcentage = 0;// Pourcentage de joueur une carte spéciale 

            var random = new Random(); // Random

            // Calculer le pourcentage de jouer une carte spéciale 
            Pourcentage = random.Next(Math.Max(Math.Min((Next_player_number_card - AI_number_cards) * -10, 100), 0), 100);

            if (Next_player_number_card < 5 && Pourcentage == 100 && Players[current_player].player_cards.Any(card => card.color == card_color.MULTICOLORE))
            {

                Card card_AI  = (Players[current_player].player_cards.Find(card => card.color == card_color.MULTICOLORE)); // trouver la carte que l'IA va jouer
                card_deck_used.Add(card_AI); // ajouter la carte que l'IA va jouer au deck des cartes jouées
                Players[current_player].player_cards.Find(card => card == card_AI); // retirer la carte du deck du joueur   
            }
            else
            {
                // compter le nombre de carte par couleur 
                for (int i = 0; i < 4; i++)
                {
                    for (int g = 0; g < Players[current_player].player_cards.Count(); g++)
                    {
                        if (index_playable_cards.Contains(g))
                        {
                            if (Players[current_player].player_cards.ElementAt(g).color == Card_color)
                            {
                                current_type_card++;
                            }
                        }
                    }
                    Cards_color_numbers[i, 0] = (byte)Card_color; // ajouter la couleur
                    Cards_color_numbers[i, 1] = current_type_card; // ajouter le nombre de carte par couleur
                    Card_color++; // passer à la couleur suivante
                    current_type_card = 0; // remettre à 0 le nombre de carte 
                }

                // Prendre la/les couleur(s) qui est/sont qui revient/reviennent le plus souvent
                for (int i = 0; i < 4; i++)
                {
                    if (Cards_color_numbers[i, 1] > max)
                    {
                        max = Cards_color_numbers[i, 1];
                    }
                }

                // Ajouter les couleurs de cartes de l'AI qui peut jouée
                for (int i = 0; i < 4; i++)
                {
                    if (Cards_color_numbers[i, 1] == max && Cards_color_numbers[i, 1] != 0)
                    {
                        Max_color.Add((card_color)Cards_color_numbers[i, 0]);
                    }
                }

                // Si l'IA peut jouer plus d'une carte, predire toutes les cartes jouables et prendre celle avec le plus de possibilité
                if (Max_color.Count >= 1)
                {
                    for (byte i = 0; i < Players[current_player].player_cards.Count; i++)
                    {
                        if (index_playable_cards.Contains(i) && Players[current_player].player_cards.ElementAt(i).color != card_color.MULTICOLORE)
                        {
                            card_number = AI_card_prediction(i, Players, current_player, index_playable_cards_prediction);
                            if(card_number > max_card_number)
                            {
                                max_card_number = card_number;
                                index_card = i;
                            }
                        }
                    }
                    card_deck_used.Add(Players[current_player].player_cards.ElementAt(index_card)); // ajouter la carte que l'IA va jouer au deck des cartes jouées
                    Players[current_player].player_cards.RemoveAt(index_card); // retirer la carte du deck du joueur                 
                }
                // Regarder si l'IA possède une carte MULTICOLORE
                else if (Players[current_player].player_cards.Any(card => card.color == card_color.MULTICOLORE))
                {
                    index_card = (byte)Players[current_player].player_cards.FindIndex(card => card.color == card_color.MULTICOLORE);
                    card_deck_used.Add(Players[current_player].player_cards.ElementAt(index_card));
                    Players[current_player].player_cards.RemoveAt(index_card);
                }
                // piocher une carte
                else
                {
                   Draw_card(Players, card_deck, card_deck_used, current_player ,ref Players_actions, Players[current_player].player_cards.Last());
                   Drew_card = Players[current_player].player_cards.Last();
                    // jouer la carte si elle peut etre jouée
                    if (play_drew_card(Drew_card, card_deck_used, current_color, Players[current_player].player_type))
                    {
                        card_deck_used.Add(Drew_card);
                        Players[current_player].player_cards.Remove(Drew_card);
                    }
                    else
                    {
                        current_player = Get_next_player(direction, current_player, players_numbers);
                    }

                }
            }
            
        }

        // Prediction d'une carte
        static byte AI_card_prediction(byte value, Player[] Players, byte current_player, int[] index_playable_predictions_cards)
        {
            List<Card> Prediction_card = new List<Card>();

            byte numbers_cards = 0; // nombre de carte pouvant être jouée après la prédiction

            // Tester la carte et récuperer le nombre de possibilité de rejouabilité
            Prediction_card.Add(Players[current_player].player_cards.ElementAt(value));
            Can_play(current_player, Players, Prediction_card, ref index_playable_predictions_cards, Prediction_card.First().color);
            Prediction_card.Clear();
            for(byte i = 0; index_playable_predictions_cards[i] != -1; i++)
            {
                numbers_cards++;
            }
            return numbers_cards;        
        }

    }
}

