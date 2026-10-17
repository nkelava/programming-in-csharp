

namespace TicTacToe.Entities
{
    public class Player
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint Score { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }

        public Player(uint id, string name, string icon, string color)
        {
            Id = id;
            Name = name;
            Score = 0;
            Icon = icon;
            Color = color;
        }
    }
}
