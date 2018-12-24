
namespace WebServer.Application.Views.Home
{
    using System;
    using WebServer.Server.Contracts;

    public class UserViewAndId : IView
    {
        private string name;
        private int id;

        public UserViewAndId(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public string View()
        {
            return $"<h1>Hello you are {this.name} with id {this.id}</h1>";
        }
    }
}
