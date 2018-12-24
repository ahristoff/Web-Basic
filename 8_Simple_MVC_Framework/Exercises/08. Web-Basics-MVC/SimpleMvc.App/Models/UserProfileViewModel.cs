
namespace SimpleMvc.App.Models
{
    using SimpleMvc.App.Data.Models;
    using System.Collections.Generic;

    public class UserProfileViewModel
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public List<Note> Notes { get; set; }
    }
}
