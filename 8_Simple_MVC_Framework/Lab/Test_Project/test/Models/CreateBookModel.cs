
namespace test.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateBookModel
    {
        [Required(ErrorMessage ="попълни Title")]
        public string Title { get; set; }
        
        public int Year { get; set; }

        [Required]
        public string Author { get; set; }
    }
}
