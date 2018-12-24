
namespace _2_One_to_Many_Relation
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new OneToManyDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var department = new Department { Name = "One" };
              
                department.Employees.Add(new Employee { Name = "Pesho", });
                department.Employees.Add(new Employee { Name = "Gosho", });
                db.Departments.Add(department);

                db.SaveChanges();

                var res = db
                    .Departments
                    .Where(d => d.Id == 1)
                    .Select(d => new
                    {
                        d.Name,
                        Employeess = d.Employees.Select(f => new { f.Name })
                    });

                foreach (var x in res)
                {
                    Console.WriteLine($"Department: {x.Name}");
                    Console.WriteLine("EmployeeNames: ");
                    foreach (var y in x.Employeess)
                    {
                        Console.WriteLine($"---{y.Name}");
                    }
                };
            }
        }
    }
}
