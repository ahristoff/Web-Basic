
namespace Football_Betting.Models
{
    using System.Collections.Generic;

    public class Color
    {
        public int ColorId { get; set; }
        public string Name { get; set; }

        public ICollection<Team> PrimaryKitTeams { get; set; } //1,2
        public ICollection<Team> SecondaryKitTeams { get; set; } //1,2
    }
}
