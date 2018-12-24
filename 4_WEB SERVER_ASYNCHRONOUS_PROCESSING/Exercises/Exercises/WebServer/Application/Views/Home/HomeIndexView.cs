
namespace WebServer.Application.Views
{
    using Server.Contracts;

    class HomeIndexView : IView
    {
        public string View()
        {
            return @"<head><link rel = ""icon"" href = ""data:,""></head><h1>Welcome<h1>";           
        }
    }
}
