
namespace Football_Betting.Models
{
    using System.Collections.Generic;

    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public string Initials { get; set; }

        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }

        public Color PrimaryKitColor { get; set; }  //1,2

        public int SecondaryKitColorId { get; set; }

        public Color SecondaryKitColor { get; set; } //1,2

        public int TownId { get; set; }

        public Town Town { get; set; }  //3,4

        public ICollection<Game> HomeGames { get; set; } = new List<Game>(); //5

        public ICollection<Game> AwayGames { get; set; } = new List<Game>(); //5

        public ICollection<Player> Players { get; set; } = new List<Player>();  //7

    }
}
