
namespace _4_Many_to_Many_Relation
{
    using System;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            var db = new ManyToManyDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var studentNames = new[]
            {
                new Student { Name = "Pesho" },
                new Student { Name = "Gosho" }
            };
            db.Students.AddRange(studentNames);

            var courseNames = new[]
            {
                new Course{ Name = "Biologie"},
                new Course{ Name = "Chemistry"},
            };
            db.Courses.AddRange(courseNames);

            var studentCourses = new[]
            {
                new StudentsCourse{ Student = studentNames[0], Course = courseNames[0]},
                new StudentsCourse{ Student = studentNames[0], Course = courseNames[1]},
                new StudentsCourse{ Student = studentNames[1], Course = courseNames[1]},
            };
            db.StudentsCourses.AddRange(studentCourses);

            db.SaveChanges();

            var students = db.Students
                .Select(s => new
                {
                    s.Name,
                    StudentCourses = s.StudentsCourses.Select(sc => new
                    {
                        sc.Course
                    })
                    .ToArray()
                })
                .ToArray();

            foreach (var student in students)
            {
                Console.WriteLine($"StudentName: {student.Name}");
                Console.WriteLine("CourseNames: ");

                foreach (var course in student.StudentCourses)
                {
                    Console.WriteLine($"--{course.Course.Name}");
                }
            }
        }
    }
}
