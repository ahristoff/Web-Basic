
namespace WebServer.ByTheCakeApp.ViewModels.Orders
{
    using System;

    public class OrdersViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreationDate { get; set; }
    
        public decimal Sum { get; set; }
    }
}
