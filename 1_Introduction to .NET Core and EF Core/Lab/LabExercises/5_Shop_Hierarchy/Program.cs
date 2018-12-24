
namespace _5_Shop_Hierarchy
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShopDbContext())
            {
                PrepareDatabase(db);
                FillSalesman(db);
                ProcessComandDb(db);
                PrintSalesmenWithCustomersCount(db);
            }
        }

        private static void PrintSalesmenWithCustomersCount(ShopDbContext db)
        {
            var salesmanData = db.Salesmen
                .Select(c => new
                {
                    c.Name,
                    Customers = c.Customers.Count
                })
                .OrderByDescending(c => c.Customers)
                .ThenBy(c => c.Name);

            foreach (var x in salesmanData)
            {
                Console.WriteLine($"{x.Name} - {x.Customers} customers");
            }           
        }

        private static void ProcessComandDb(ShopDbContext db)
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var parts = line.Split('-');
                var command = parts[0];
                var arguments = parts[1];

                switch (command)
                {
                    case "register":
                        RegisterCustomer(db, arguments);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void RegisterCustomer(ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(';');
            var customerName = parts[0];
            var salesmanId = int.Parse(parts[1]);

            db.Add(new Customer
            {
                Name = customerName,
                SalsmanId = salesmanId

            });

            db.SaveChanges();
        }

        private static void FillSalesman(ShopDbContext db)
        {
            var salesman = Console.ReadLine().Split(';');

            foreach (var x in salesman)
            {
                db.Add(new Salesman { Name = x });
            }

            db.SaveChanges();
        }

        private static void PrepareDatabase(ShopDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
