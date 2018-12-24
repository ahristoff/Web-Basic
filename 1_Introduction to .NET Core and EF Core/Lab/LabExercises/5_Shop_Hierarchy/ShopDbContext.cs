
namespace _5_Shop_Hierarchy
{
    using Microsoft.EntityFrameworkCore;

    public class ShopDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=ALEN\\SQLEXPRESS01;Database= ShopDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Customer>()
                .HasOne(c => c.Salesman)
                .WithMany(c => c.Customers)
                .HasForeignKey(c => c.SalsmanId);
        }
    }
}
