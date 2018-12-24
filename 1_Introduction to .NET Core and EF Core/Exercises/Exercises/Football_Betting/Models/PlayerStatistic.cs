
namespace Football_Betting.Models
{
    public class PlayerStatistic
    {
        public int GameId { get; set; }

        public Game Game { get; set; }  //9

        public int PlayerId { get; set; }

        public Player Player { get; set; } //9

        public int ScoredGoals { get; set; }

        public int Assists { get; set; }

        public int MinutesPlayed { get; set; }
    }
}
