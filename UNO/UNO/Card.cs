using static UNO.Program;

namespace UNO
{
    internal class Card
    {
        public card_type types { get; set; }// type de la carte
        public card_color color { get; set; }// couleur de la carte
        public int number { get; set; }// numéro de la carte
    }
}
