using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.ByTheCakeApp.ViewModels.Orders
{
    public class OrdersViewModel
    {
        public DateTime CreationDate { get; set; }

        public int OrderId { get; set; }

        public decimal Sum { get; set; }
    }
}
