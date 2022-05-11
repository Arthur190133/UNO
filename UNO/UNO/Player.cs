using System;
using System.Collections.Generic;
using System.Linq;
using static UNO.Program;
using static UNO.Card;

namespace UNO
{
    internal class Player
    {
        public player_type player_type { get; set; }// type de joueur
        public string player_name { get; set; }// nom du joueur
        public List<Card> player_cards { get; set; }// liste des cartes du joueur
        public int player_points { get; set; }// points du joueur

        // initialiser les variables du joueur
        public Player(player_type current_player_type, string current_player_name)
        {
            player_type = current_player_type;
            player_name = current_player_name;
            player_cards = new List<Card>();
            player_points = int.MaxValue;
        }
    }
}
