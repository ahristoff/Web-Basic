﻿
namespace _7_Shop_Hierarchy_Complex_Query
{
    public class Review
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ItemId { get; set; }

        public Item item { get; set; }
    }
}
