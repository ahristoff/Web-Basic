
namespace _6_Shop_Hierarchy_Extended
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
                PrintCastomerWithOrderAndReviewsCount(db);
            }
        }

        private static void PrintCastomerWithOrderAndReviewsCount(ShopDbContext db)
        {
            var customersData = db
                .Customers
                .Select(c => new
                {
                    c.Name,
                    ReviewsCount = c.Reviews.Count,
                    OrdersCount = c.Orders.Count
                })
                .OrderByDescending(c => c.OrdersCount)
                .ThenByDescending(c => c.ReviewsCount)
                .ToList();

            foreach (var x in customersData)
            {
                Console.WriteLine(x.Name);
                Console.WriteLine($"Orders: {x.OrdersCount}");
                Console.WriteLine($"Reviews: {x.ReviewsCount}");
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
                    case "order":
                        SaveOrder(db, arguments);
                        break;
                    case "review":
                        SaveReview(db, arguments);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void SaveReview(ShopDbContext db, string arguments)
        {
            var customerId = int.Parse(arguments);
            db.Add(new Review { CustomerId = customerId });

            db.SaveChanges();
        }

        private static void SaveOrder(ShopDbContext db, string arguments)
        {
            var customerId = int.Parse(arguments);
            db.Add(new Order { CustomerId = customerId });

            db.SaveChanges();
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
