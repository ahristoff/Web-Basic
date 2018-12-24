
namespace WebServer.ByTheCakeApp.Services
{
    using System.Collections.Generic;
    using WebServer.ByTheCakeApp.Data.Models;
    using WebServer.ByTheCakeApp.ViewModels.Orders;

    public interface IShoppingService
    {
        void CreateOrder(int userId, IEnumerable<Product> orders);

        List<OrdersViewModel> GetOrdersById(string username);

        OrderDetailsViewModel FindOrder(int id);
    }
}
