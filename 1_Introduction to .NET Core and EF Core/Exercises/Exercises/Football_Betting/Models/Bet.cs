
namespace Football_Betting.Models
{
    using System;

    public class Bet
    {
        public int BetId { get; set; }

        public decimal Amount { get; set; }

        public int Prediction { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } //12

        public int GameId { get; set; }

        public Game Game { get; set; } //10
    }
}
