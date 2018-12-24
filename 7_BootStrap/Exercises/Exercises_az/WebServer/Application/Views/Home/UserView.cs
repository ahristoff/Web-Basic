
namespace WebServer.Application.Views.Home
{
    using Server.Contracts;

    public class UserView : IView
    {
        private string name;

        public UserView(string name)
        {
            this.name = name;
        }
        public string View()
        {
            return $"<h1>Hello {name}<h1>";
        }
    }
}
