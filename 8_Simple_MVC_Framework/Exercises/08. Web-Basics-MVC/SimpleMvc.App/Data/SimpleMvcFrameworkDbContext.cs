
namespace SimpleMvc.App.Data
{
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.App.Data.Models;

    public class SimpleMvcFrameworkDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(@"Server=ALEN\SQLEXPRESS01;Database=SimpleMvcFramework;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(n => n.Notes)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
