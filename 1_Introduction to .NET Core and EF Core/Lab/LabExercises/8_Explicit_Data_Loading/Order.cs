
namespace _8_Explicit_Data_Loading
{
    using System.Collections.Generic;

    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<ItemOrder> ItemOrders { get; set; } = new List<ItemOrder>();
    }
}
