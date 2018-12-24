using SimpleMvc.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.App.Models
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public List<Note> Notes { get; set; }
    }
}
