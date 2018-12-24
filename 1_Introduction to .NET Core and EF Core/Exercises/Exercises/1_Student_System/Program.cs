
namespace _1_Student_System
{
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StudentSystemContext())
            {
                RestartDb(db);
                Seed(db);
                WorkingWithDatabase1(db);
                Console.WriteLine("#########################################################");
                WorkingWithDatabase2(db);
                Console.WriteLine("#########################################################");
                WorkingWithDatabase3(db);
            }
        }

        private static void WorkingWithDatabase3(StudentSystemContext db)
        {
            var dictCourseCount = new Dictionary<string, int>();
            var dictPrice = new Dictionary<string, decimal>();

            var students = db.StudentCourses
                .Include(s => s.Student)
                .Include(c => c.Course)
                .ToArray();

            foreach (var x in students)
            {
                if (!dictCourseCount.ContainsKey(x.Student.Name))
                {
                    dictCourseCount[x.Student.Name] = 0;
                }
                dictCourseCount[x.Student.Name]++;

                if (!dictPrice.ContainsKey(x.Student.Name))
                {
                    dictPrice[x.Student.Name] = 0;
                }
                dictPrice[x.Student.Name] += x.Course.Price;
            }

            dictPrice = dictPrice
                .OrderByDescending(p => p.Value)
                .ThenBy(p =>p.Key)
                .ToDictionary(x =>x.Key, d => d.Value);

            foreach (var y in dictPrice.OrderByDescending(p => p.Value))
            {
                foreach (var x in dictCourseCount)
                {
                    if (x.Key == y.Key)
                    {
                        Console.WriteLine($"Name: {x.Key} - CourseCount: {x.Value} - CoursesTotalPrice: {y.Value:f2} - CoursesAverigePrice: {y.Value / x.Value:f2}");
                    }
                }
            }
        }
     
        private static void WorkingWithDatabase2(StudentSystemContext db)
        {
            var courses = db.Courses
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    c.EndDate,
                    Recourse = c.Resources.Select(r => new
                    {
                        r.Name,
                        r.ResourceType,
                        r.Url
                    })
                })
                .OrderBy(cn => cn.Name)
                .ThenByDescending(cn => cn.EndDate)
                .ToList();

            foreach (var x in courses)
            {
                Console.WriteLine($"CourseName: {x.Name}");
                Console.WriteLine($"CourseDescription: {x.Description}");
                Console.WriteLine();

                foreach (var y in x.Recourse)
                {
                    Console.WriteLine($"RecourseName: {y.Name}");
                    Console.WriteLine($"RecourseType: {y.ResourceType}");
                    Console.WriteLine($"RecourseUrl: {y.Url}");
                }
                Console.WriteLine("====================================");
            }
        }

        private static void WorkingWithDatabase1(StudentSystemContext db)
        {
            var students = db.Students
                .Select(s => new
                {
                    s.Name,
                    HomeworkSubmissions = s.HomeworkSubmissions
                    .Select(h => new
                    {
                        h.Content,
                        h.ContentType
                    })
                })
                .ToList();

            foreach (var x in students)
            {
                Console.WriteLine($"Name: {x.Name}");

                foreach (var y in x.HomeworkSubmissions)
                {
                    Console.WriteLine($"HomeworkContent: {y.Content}");
                    Console.WriteLine($"HomeworkContentTypeType: {y.ContentType}");
                }
            }
        }

        private static void Seed(StudentSystemContext db)
        {
            var students = new[]
            {
                new Student
                {
                    Name = "Asan Mehmedov",
                    RegisteredOn = new DateTime(2010, 02, 05),
                    Birthday = new DateTime(2001, 05, 24),
                    PhoneNumber = "0845698715"
                },
                new Student
                {
                    Name = "Ivan Ivanov",
                    RegisteredOn = new DateTime(2014, 01, 18),
                    Birthday = new DateTime(2000, 10, 20),
                    PhoneNumber = "0257893451"
                },
                new Student
                {
                    Name = "Gosho Goshov",
                    RegisteredOn = new DateTime(2016, 02, 19),
                    Birthday = new DateTime(1995, 04, 15),
                    PhoneNumber = "0698742365"
                },
                new Student
                {
                    Name = "Pesho Peshov",
                    RegisteredOn = new DateTime(2013, 02, 25),
                    Birthday = new DateTime(1971, 05, 24),
                    PhoneNumber = "0123456789"
                },
                new Student
                {
                    Name = "Jhon Kingsly",
                    RegisteredOn = new DateTime(2009, 10, 11),
                    Birthday = new DateTime(1985, 02, 04),
                    PhoneNumber = "0888888888"
                }
            };
            db.Students.AddRange(students);

            var courses = new[]
            {
                new Course
                {
                    Name = "Mathe",
                    StartDate = new DateTime(2018, 06, 04),
                    EndDate = new DateTime(2018, 12, 16),
                    Price = 1000.00M,
                    Description = "Mnoo slojno"
                },
                new Course
                {
                    Name = "Biology",
                    StartDate = new DateTime(2018, 07, 14),
                    EndDate = new DateTime(2018, 12, 29),
                    Price = 1200.00M,
                    Description = "Mnoo tupo"
                },
                new Course
                {
                    Name = "Geography",
                    StartDate = new DateTime(2018, 06, 04),
                    EndDate = new DateTime(2018, 12, 16),
                    Price = 800.00M,
                    Description = "Mnoo bezinteresno"
                },
                new Course
                {
                    Name = "Chemistry",
                    StartDate = new DateTime(2018, 06, 04),
                    EndDate = new DateTime(2018, 12, 16),
                    Price = 1500.00M,
                    Description = "Mnoo skapo"
                },
            };
            db.Courses.AddRange(courses);

            var homeWork = new[]
            {
                new Homework
                {
                    Course = courses[0],
                    Content = "resjkmgfdl,fj",
                    ContentType = ContentType.Zip,
                    SubmissionTime = new DateTime(2018, 10, 28),
                    Student = students[1]
                },
                new Homework
                {
                    Course = courses[1],
                    Content = "srtuyikms",
                    ContentType = ContentType.Pdf,
                    SubmissionTime = new DateTime(2018, 11, 08),
                    Student = students[0]
                },
                new Homework
                {
                    Course = courses[0],
                    Content = "iy['u",
                    ContentType = ContentType.Application,
                    SubmissionTime = new DateTime(2018, 09, 28),
                    Student = students[1]
                },
                new Homework
                {
                    Course = courses[2],
                    Content = "resjkmgfdl,fj",
                    ContentType = ContentType.Zip,
                    SubmissionTime = new DateTime(2018, 10, 28),
                    Student = students[4]
                },
                new Homework
                {
                    Course = courses[3],
                    Content = "resjkmgfdl,fj",
                    ContentType = ContentType.Zip,
                    SubmissionTime = new DateTime(2018, 10, 28),
                    Student = students[2]
                },
                 new Homework
                {
                    Course = courses[3],
                    Content = "resjkmgfdl,fj",
                    ContentType = ContentType.Zip,
                    SubmissionTime = new DateTime(2018, 10, 28),
                    Student = students[3]
                }
            };
            db.HomeworkSubmissions.AddRange(homeWork);

            var recourses = new[]
            {
                new Resource
                {
                    Name = "sehjyjr6",
                    Course = courses[2],
                    ResourceType = ResourceType.Document,
                    Url = "xfg/dytj/yut/rttyu/sd"
                },
                new Resource
                {
                    Name = "sehjyjr6",
                    Course = courses[0],
                    ResourceType = ResourceType.Document,
                    Url = "xfg/dytj/yut/rttyu/sd"
                },
                new Resource
                {
                    Name = "gio",
                    Course = courses[2],
                    ResourceType = ResourceType.Other,
                    Url = "ffhj/dytj/yut/rttyu/hj"
                },
                new Resource
                {
                    Name = "gig",
                    Course = courses[3],
                    ResourceType = ResourceType.Presentation,
                    Url = "nkl/ho/h/hiioh/hlk"
                },
            };
            db.Resources.AddRange(recourses);

            var studentCourses = new[]
            {
                new StudentCourse
                {
                    Student = students[2], Course = courses[0]
                },
                new StudentCourse
                {
                    Student = students[2], Course = courses[1]
                },
                new StudentCourse
                {
                    Student = students[2], Course = courses[3]
                },
                new StudentCourse
                {
                    Student = students[0], Course = courses[0]
                },
                new StudentCourse
                {
                    Student = students[0], Course = courses[2]
                },
                new StudentCourse
                {
                    Student = students[1], Course = courses[3]
                },
                new StudentCourse
                {
                    Student = students[1], Course = courses[1]
                },
                new StudentCourse
                {
                    Student = students[3], Course = courses[2]
                },
                new StudentCourse
                {
                    Student = students[3], Course = courses[1]
                },
                new StudentCourse
                {
                    Student = students[4], Course = courses[0]
                },
                new StudentCourse
                {
                    Student = students[4], Course = courses[2]
                }
            };
            db.StudentCourses.AddRange(studentCourses);

            db.SaveChanges();
        }

        private static void RestartDb(StudentSystemContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
