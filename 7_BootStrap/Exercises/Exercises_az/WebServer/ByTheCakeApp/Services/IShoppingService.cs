using System.Collections.Generic;
using WebServer.ByTheCakeApp.Data.Models;
using WebServer.ByTheCakeApp.ViewModels.Orders;

namespace WebServer.ByTheCakeApp.Services
{
    public interface IShoppingService
    {
        // void CreateOrder(int userId, IEnumerable<int> productIds);
        void CreateOrder(int userId, IEnumerable<Product> orders);

        List<OrdersViewModel> GetOrdersById(string username);

        OrderDetailsViewModel FindOrder(int id);
    }
}
