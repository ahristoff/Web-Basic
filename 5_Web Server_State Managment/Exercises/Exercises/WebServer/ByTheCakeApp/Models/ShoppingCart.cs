
namespace WebServer.ByTheCakeApp.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public const string SessionKey = "Shopping_Cart_Key";

        public List<Cake> Orders { get; private set; } = new List<Cake>();
    }
}
