
namespace _1_Student_System
{
    using _1_Student_System.Models;
    using Microsoft.EntityFrameworkCore;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {
        }
        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            //----------Relations-------------------------------

            //----------------------------------------Many to Many
            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.CourseEnrollments)
                .HasForeignKey(c => c.StudentId);

            builder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsEnrolled)
                .HasForeignKey(c => c.CourseId);
            //------------------------------------------one to many


            builder.Entity<Student>()           //2
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId);

            builder.Entity<Course>()            //4
                .HasMany(s => s.Resources)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            builder.Entity<Course>()            //5
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId);

            //----------Validations------------------------------
            //---------Student-----------------------------------
            builder.Entity<Student>()
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder.Entity<Student>()
               .Property(s => s.PhoneNumber)
               .HasColumnType("char(10)")
               .IsUnicode(false);

            builder.Entity<Student>()
                .Property(s => s.Birthday)
                .IsRequired(false);
            //-----------Course----------------------------------
            builder.Entity<Course>()
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsUnicode();

            builder.Entity<Course>()
               .Property(s => s.Description)
               .IsUnicode()
               .IsRequired(false);
            //-----------Resources-------------------------------
            builder.Entity<Resource>()
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Entity<Resource>()
               .Property(r => r.Url)
               .IsUnicode(false);
            //-----------HomeWork---------------------------------
            builder.Entity<Homework>()
                .Property(r => r.Content)
                .IsUnicode(false);

        }
    }
}
