
namespace _5_Shop_Hierarchy
{
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int SalsmanId { get; set; }

        public Salesman Salesman { get; set; }
    }
}
