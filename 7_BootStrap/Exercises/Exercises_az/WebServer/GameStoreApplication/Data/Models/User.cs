

namespace WebServer.GameStoreApplication.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WebServer.GameStoreApplication.Common;

    public class User
    {
        public int Id { get; set; }

        [MinLength(ValidationConstants.Account.NameMinLength)]   //[MinLength(2)]     
        [MaxLength(ValidationConstants.Account.NameMaxLength)]   //[MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ValidationConstants.Account.EmailMaxLength)] //[MaxLength(30)]
        public string Email { get; set; }

        [Required]
        [MinLength(ValidationConstants.Account.PasswordMinLength)]  //[MinLength(6)]   
        [MaxLength(ValidationConstants.Account.PasswordMaxLength)]  //[MaxLength(50)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<UserGame> Games { get; set; } = new List<UserGame>();
    }
}
