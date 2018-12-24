
namespace _7_Shop_Hierarchy_Complex_Query
{
    using Microsoft.EntityFrameworkCore;

    public class ShopDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmen { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

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

            //-----------------------------------------many-many
            builder
                .Entity<ItemOrder>()
                .HasKey(c => new { c.ItemId, c.OrderId });

            builder
                .Entity<ItemOrder>()
                .HasOne(i => i.Item)
                .WithMany(i => i.ItemOrders)
                .HasForeignKey(io => io.ItemId);

            builder
               .Entity<ItemOrder>()
               .HasOne(i => i.Order)
               .WithMany(i => i.ItemOrders)
               .HasForeignKey(io => io.OrderId);

            builder
                .Entity<Item>()
                .HasMany(i => i.Reviews)
                .WithOne(i => i.item)
                .HasForeignKey(i => i.ItemId);

        }
    }
}
