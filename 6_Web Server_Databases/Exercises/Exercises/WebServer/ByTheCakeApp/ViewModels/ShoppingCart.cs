
namespace WebServer.ByTheCakeApp.ViewModels
{
    using System.Collections.Generic;
    using WebServer.ByTheCakeApp.Data.Models;

    public class ShoppingCart
    {
        public const string SessionKey = "Shopping_Cart_Key";

        public List<Product> Orders { get; private set; } = new List<Product>();
    }
}
