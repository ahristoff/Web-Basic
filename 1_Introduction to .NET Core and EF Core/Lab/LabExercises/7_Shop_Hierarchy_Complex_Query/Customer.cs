
namespace _7_Shop_Hierarchy_Complex_Query
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int SalsmanId { get; set; }

        public Salesman Salesman { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
