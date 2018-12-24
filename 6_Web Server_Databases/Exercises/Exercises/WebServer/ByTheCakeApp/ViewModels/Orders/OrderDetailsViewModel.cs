
namespace WebServer.ByTheCakeApp.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using WebServer.ByTheCakeApp.Data.Models;

    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }

        public DateTime CreationDate { get; set; }

        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    }
}
