
namespace _3_Self_Referenced_Table
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var db = new SelfReferencedDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var department = new Department { Name = "One" };           

            department.Employees.Add(new Employee { Name = "Pesho", Manager = new Employee { Name = "King Stojan", Department = department }});
            department.Employees.Add(new Employee { Name = "Gosho", Manager = new Employee { Name = "King Stojan", Department = department } });

            db.Departments.Add(department);
            db.SaveChanges();

            var depart = db
                    .Departments
                    .Where(d => d.Id == 1)
                    .Select(d => new
                    {
                        d.Name,                       
                        Managers = d.Employees.Where(m => m.Manager.Name != null).Select(f => new
                        {
                            Manager =  f.Manager.Name ,
                            EmployeeSlaves = f.Name
                        })
                    });

            foreach (var x in depart)
            {
                Console.WriteLine($"Department: {x.Name}");

                foreach (var manager in x.Managers)
                {
                    Console.WriteLine($"ManagerName: {manager.Manager} -- EmployeeNames: {manager.EmployeeSlaves}");                                   
                }           
            };
        }
    }
}
