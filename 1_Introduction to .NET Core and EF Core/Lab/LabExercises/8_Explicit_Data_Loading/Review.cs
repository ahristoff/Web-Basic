
namespace _8_Explicit_Data_Loading
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
