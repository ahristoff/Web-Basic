
namespace WebServer.ByTheCakeApp.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebServer.ByTheCakeApp.ViewModels.Orders;

    public class ShoppingService : IShoppingService
    {
        public void CreateOrder(int userId, IEnumerable<Product> orders)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var productIds = orders.Select(o => o.Id).ToList();

                var order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Products = productIds
                        .Select(id => new OrderProduct
                        {
                            ProductId = id
                        })
                        .ToList()
                };
                
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
        
        public List<OrdersViewModel> GetOrdersById(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var user = db.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                var orders = db.Orders
                    .Where(o => o.UserId == user.Id)
                    .Select(u => new OrdersViewModel
                    {
                        OrderId = u.Id,
                        CreationDate = u.CreationDate,
                        Sum = u.Products.Sum(p => p.Product.Price)
                    })
                    .ToList();
                return orders;
            }
        }

        
        public OrderDetailsViewModel FindOrder(int id)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var order = db.Orders
                     .Where(pr => pr.Id == id)
                     .Select(pr => new OrderDetailsViewModel
                     {
                         OrderId = id,
                         CreationDate = pr.CreationDate,
                         Products = pr.Products
                         .Select(p => new OrderProduct
                         {
                             ProductId = p.ProductId,
                             OrderId = id,
                             Product = p.Product
                         })
                         .ToList()
                     })
                     .FirstOrDefault();

                return order;
            }
        }
    }
}
