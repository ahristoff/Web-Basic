using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.ByTheCakeApp.ViewModels.Account
{
    using System;
    //4
    public class ProfileViewModel
    {
        public string Username { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int TotalOrders { get; set; }
    }
}
