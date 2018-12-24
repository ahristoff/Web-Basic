
namespace Football_Betting
{
    using System;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using (var context = new FootballBettingContext())
            {
                RestartDb(context);
            }
        }

        private static void RestartDb(FootballBettingContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
