
namespace _4_Many_to_Many_Relation
{
    using Microsoft.EntityFrameworkCore;

    public class ManyToManyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentsCourse> StudentsCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=ALEN\\SQLEXPRESS01;Database= ManyToMany;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentsCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<StudentsCourse>()
                .HasOne<Student>(sc => sc.Student)
                .WithMany(st => st.StudentsCourses)
                .HasForeignKey(sc => sc.StudentId);

            builder.Entity<StudentsCourse>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(st => st.StudentsCourses)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
