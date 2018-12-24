
namespace _2_One_to_Many_Relation
{
    using Microsoft.EntityFrameworkCore;

    public class OneToManyDbContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=ALEN\\SQLEXPRESS01;Database= OneToMany;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
              .HasOne<Department>(emp => emp.Department)
              .WithMany(d => d.Employees)
              .HasForeignKey(emp => emp.DepartmentId);
        }
    }
}
