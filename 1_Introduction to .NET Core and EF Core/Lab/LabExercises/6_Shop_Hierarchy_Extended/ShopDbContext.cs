
namespace _6_Shop_Hierarchy_Extended
{
    using Microsoft.EntityFrameworkCore;

    public class ShopDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmen { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

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

            builder
                .Entity<Order>()
                .HasOne(c => c.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(c => c.CustomerId);

            builder
                .Entity<Review>()
                .HasOne(c => c.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(c => c.CustomerId);
        }
    }
}
