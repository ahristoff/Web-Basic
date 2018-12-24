
namespace Football_Betting.Models
{
    using System.Collections.Generic;

    public class Country
    {
        public int CountryId { get; set; }

        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; } = new List<Town>();  //6
    }
}
