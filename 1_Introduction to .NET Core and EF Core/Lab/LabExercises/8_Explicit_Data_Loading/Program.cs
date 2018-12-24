
namespace _8_Explicit_Data_Loading
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
                SaveItems(db);
                ProcessComandDb(db);
                PrintCastomerData(db);
            }
        }

        private static void SaveItems(ShopDbContext db)
        {
            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var parts = line.Split(';');
                var itemName = parts[0];
                var itemPrice = decimal.Parse(parts[1]);

                db.Add(new Item
                {
                    Name = itemName,
                    Price = itemPrice
                });
            }

            db.SaveChanges();
        }

        private static void PrintCastomerData(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());

            var customerData = db.Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,                   
                    ReviewsCount = c.Reviews.Count,
                    SalsmanName = c.Salesman.Name
                })
                .FirstOrDefault();
          
            Console.WriteLine($"Customer: {customerData.Name}");
            Console.WriteLine($"Orders count:{customerData.OrdersCount}");
            Console.WriteLine($"Reviews count: {customerData.ReviewsCount}");
            Console.WriteLine($"Salesman: {customerData.SalsmanName}");
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
            var parts = arguments.Split(';');
            var customerId = int.Parse(parts[0]);
            var itemId = int.Parse(parts[1]);

            db.Add(new Review
            {
                CustomerId = customerId,
                ItemId = itemId
            });

            db.SaveChanges();
        }

        private static void SaveOrder(ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(';');
            var customerId = int.Parse(parts[0]);

            var order = new Order { CustomerId = customerId };

            for (int i = 1; i < parts.Length; i++)
            {
                var itemId = int.Parse(parts[i]);

                order.ItemOrders.Add(new ItemOrder
                {
                    ItemId = itemId,
                    OrderId = order.Id
                });
            }

            db.Add(order);

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
