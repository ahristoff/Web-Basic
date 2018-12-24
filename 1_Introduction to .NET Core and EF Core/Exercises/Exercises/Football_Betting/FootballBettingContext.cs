
namespace Football_Betting
{
    using Football_Betting.Models;
    using Microsoft.EntityFrameworkCore;

    public class FootballBettingContext : DbContext
    {
        public FootballBettingContext()
        {
        }
        public FootballBettingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=ALEN\\SQLEXPRESS01;Database= Football;Integrated Security=True");
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //-------------------------------------------------------------------------Property ot tabl.
            builder.Entity<Team>()
                .HasOne(e => e.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(e => e.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict); //1

            builder.Entity<Team>()
                .HasOne(e => e.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(e => e.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict); //2
            //-------------------------------------------------------------------------Property ot tabl.
            builder.Entity<Town>()
                .HasMany(tm => tm.Teams)
                .WithOne(tw => tw.Town)
                .HasForeignKey(tm => tm.TownId);    //3,4

            //-------------------------------------------------------------------------Property ot tabl.
            builder.Entity<Game>()
                .HasOne(t => t.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(t => t.AwayTeamId) //5
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(g => g.HomeTeamId) //5
                .OnDelete(DeleteBehavior.Restrict);
            //----------------------------------------------------------------------------property v tabl.
            builder.Entity<Country>()
                .HasMany(tm => tm.Towns)
                .WithOne(tw => tw.Country)
                .HasForeignKey(tm => tm.CountryId);  //6

            builder.Entity<Team>()
                .HasMany(tm => tm.Players)
                .WithOne(tw => tw.Team)
                .HasForeignKey(tm => tm.TeamId);  //7


            builder.Entity<Position>()
                .HasMany(tm => tm.Players)
                .WithOne(tw => tw.Position)
                .HasForeignKey(tm => tm.PositionId);  //8

            //-------------------------------------------------------9
            builder.Entity<PlayerStatistic>()
                .HasKey(ps => new { ps.PlayerId, ps.GameId });

            builder.Entity<PlayerStatistic>()
               .HasOne(tm => tm.Player)
               .WithMany(tw => tw.PlayerStatistics)
               .HasForeignKey(tm => tm.PlayerId);

            builder.Entity<PlayerStatistic>()
               .HasOne(tm => tm.Game)
               .WithMany(tw => tw.PlayerStatistics)
               .HasForeignKey(tm => tm.GameId);
            //------------------------------------------------------9

            builder.Entity<Game>()
               .HasMany(tm => tm.Bets)
               .WithOne(tw => tw.Game)
               .HasForeignKey(tm => tm.GameId);    //10



            builder.Entity<User>()
               .HasMany(tm => tm.Bets)
               .WithOne(tw => tw.User)
               .HasForeignKey(tm => tm.UserId);   //12

        }
    }
}
