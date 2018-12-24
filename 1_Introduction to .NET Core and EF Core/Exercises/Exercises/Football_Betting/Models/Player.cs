
namespace Football_Betting.Models
{
    using System.Collections.Generic;

    public class Player
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public int SquadNumber { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }  //7

        public int PositionId { get; set; }

        public Position Position { get; set; }  //8

        public bool IsInjured { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>(); //9
    }
}
